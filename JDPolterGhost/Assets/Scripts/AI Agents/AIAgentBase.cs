using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all NPCs; allows you to set a schedule 
/// All floats in #Schedule should strictly increase going down the list
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AIAgentBase : MonoBehaviour
{
    public bool spooked = false; //Has the agent seen anything strange in the last 2 seconds?

    public int fearLevel = 0;
    public int maxFearLevel = 1000;

    public int panicLevel = 0; //Similar to fear, panic increases whenever fear does, and decreases as long as the agent is 'panicing'
    public int MaxPanicLevel = 50; //Once panic reaches this breakpoint the agent will flee to a safe distance (need help with determining this behavior exactly, for now just runs to bed)

    public float viewConeAngle = 90f;

    public ScheduleItem sleep; //Location of bed and sleeping hours, unlike other ScheduleItems endTime should be BEFORE startTime

    public ScheduleItem[] schedule; //List of objects and times for the agent to visit through the day

    public GameObject[] miscLocations; //List of objects for the agent to visit at random between schedule items

    private bool selectedFreeTimeLocation; //Has the agent determined what to do during its schedule gap?

    private NavMeshAgent agent;

    void Start()
    {
        //Check to see if times are in the correct range and order
        agent = GetComponent<NavMeshAgent>();
        AssertTimeOrder();
    }

    void Update()
    {
        DetermineActivityByTime();
    }

    /// <summary>
    /// Makes sure that the schedule is ordered correctly
    /// All start times in schedule should be less than the following end time, and no ranges should overlap
    /// Sleep times are the exception to this, sleep.startTime should be the latest schedule time in the day
    /// and sleep.endTime should be the earliest
    /// 
    /// These actions will always take precedence over inherited class actions, so their ordering is not importaint
    /// </summary>
    private void AssertTimeOrder()
    {
        float previousEndTime = sleep.endTime;
        for(int i = 0; i < schedule.Length; i++)
        {
            Debug.Assert(previousEndTime < schedule[i].startTime, "Check that your schedule times don't overlap!", gameObject);
            Debug.Assert(schedule[i].startTime < schedule[i].endTime, "Check that your schedule doesn't have any negative duration items!", gameObject);
        }
        Debug.Assert(schedule[schedule.Length - 1].endTime < sleep.startTime, "Check that sleep start time is the latest time in your schedule!");
    }

    private void DetermineActivityByTime()
    {
        float time = GameTime.TimeOfDay();

        if(panicLevel >= MaxPanicLevel)
        {
            Panic();
        }
        else if (spooked)
        {
            StartCoroutine(Investigate());
        }
        else if (spooked == false)
        {
            if ((sleep.startTime < time) || (time < sleep.endTime))
            {
                GoToBed();
                return;
            }
            for (int i = 0; i < schedule.Length; i++)
            {
                if (schedule[i].startTime < time && time < schedule[i].endTime)
                {
                    selectedFreeTimeLocation = false;
                    agent.SetDestination(schedule[i].location.transform.position);
                    return;
                }
            }
            if (selectedFreeTimeLocation == false)
            {
                if (miscLocations.Length > 0)
                {
                    selectedFreeTimeLocation = true;
                    int location = Random.Range(0, miscLocations.Length);
                    Debug.Log("Heading to misc location " + location);
                    agent.SetDestination(miscLocations[location].transform.position);
                }
            }
        }
    }

    private IEnumerator Investigate()
    {
        agent.Stop();
        Debug.Log("Agent stopped to look at a haunted object!");
        yield return new WaitForSeconds(2f);

        spooked = false;
        agent.Resume();
        Debug.Log("Agent resumed moving!");
    }

    private void Panic()
    {
        agent.Resume();
        GoToBed();
        if(IsInvoking("ReducePanicLevel") == false)
        {
            InvokeRepeating("ReducePanicLevel", 5f, 1f);
        }
    }

    private void ReducePanicLevel()
    {
        panicLevel--;
        if(panicLevel <= 0)
        {
            CancelInvoke();
        }
    }

    private void GoToBed()
    {
        agent.SetDestination(sleep.location.transform.position);
        //TODO: Implement animations and variable changes for this AI state
    }

    void OnCollisionStay(Collision other)
    {
        CheckHearing(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckSight(other);
    }

    /// <summary>
    /// Checks an object around the npc to see if it is an interactable, and if it is within the npc's viewcone
    /// </summary>
    private void CheckSight(Collider other)
    {
        InteractableObject interactable;
        if(interactable = other.GetComponent<InteractableObject>())
        {
            if (interactable.spookey)
            {
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(transform.forward, direction);
                if (angle < viewConeAngle / 2f)
                {
                    Debug.DrawRay(transform.position, direction, Color.red);

                    RaycastHit hit;
                    Ray toObject = new Ray(transform.position, direction);
                    Physics.Raycast(toObject, out hit);
                    if (hit.transform.tag == Tags.interactable)
                    {
                        ScareAgent(interactable);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Check if the player is within hearing range of any possessed object
    /// </summary>
    private void CheckHearing(Collision other)
    {

    }

    private void ScareAgent(InteractableObject hauntedObject)
    {
        int addedFear = hauntedObject.ScareNPC(this);
        fearLevel += addedFear;
        panicLevel += addedFear;
        spooked = true;
        transform.LookAt(hauntedObject.transform);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
    }
}
