﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for all NPCs; allows you to set a schedule 
/// All floats in #Schedule should strictly increase going down the list
/// </summary>
[RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
public class AIAgentBase : MonoBehaviour
{
    public bool spooked = false; //Has the agent been scared this frame?

    public int fearLevel = 0;
    public int maxFearLevel = 1000;

    public int panicLevel = 0; //Similar to fear, panic increases whenever fear does, and decreases as long as the agent is 'panicing'
    public int MaxPanicLevel = 50; //Once panic reaches this breakpoint the agent will flee to a safe distance (need help with determining this behavior exactly, for now just runs to bed)

    public float viewConeAngle = 90f;
    public float eyeHeight;

    public ScheduleItem sleep; //Location of bed and sleeping hours, unlike other ScheduleItems endTime should be BEFORE startTime

    public ScheduleItem[] schedule; //List of objects and times for the agent to visit through the day

    public GameObject[] miscLocations; //List of objects for the agent to visit at random between schedule items

    public GameObject[] safeLocations; //List of objects for the agent to flee to when their panic maxes out; will pick the object which is farthest from the player

    public GameObject exitLocation; //GameObject for the agent to travel to once their fear has capped out

    private bool selectedFreeTimeLocation; //Has the agent determined what to do during its schedule gap?

    private NavMeshAgent agent;
    private GameObject player;

    void Start()
    {
        //Check to see if times are in the correct range and order
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag(Tags.player);
        AssertTimeOrder();
    }

    void Update()
    {
        DetermineActivityByTime();
    }

    void LateUpdate()
    {
        spooked = false;
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
            Debug.Assert(previousEndTime <= schedule[i].startTime, "Check that your schedule times don't overlap!", gameObject);
            Debug.Assert(schedule[i].startTime <= schedule[i].endTime, "Check that your schedule doesn't have any negative duration items!", gameObject);
        }
        Debug.Assert(schedule[schedule.Length - 1].endTime <= sleep.startTime, "Check that sleep start time is the latest time in your schedule!");
    }

    private void DetermineActivityByTime()
    {
        float time = GameTime.TimeOfDay();

        if(fearLevel >= maxFearLevel)
        {
            LeaveHouse();
        }
        else if(panicLevel >= MaxPanicLevel)
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

    private void LeaveHouse()
    {
        agent.SetDestination(exitLocation.transform.position);
    }

    private IEnumerator Investigate()
    {
        agent.Stop();
        Debug.Log("Agent stopped to look at a haunted object!");
        yield return new WaitForSeconds(2f);
        
        if(spooked == false)
        {
            agent.Resume();
            Debug.Log("Agent resumed moving!");
        }
    }

    private void Panic()
    {
        agent.Resume();

        //Finds the safeLocation which is farthest from the player
        Vector3 safeDestination = Vector3.zero;
        float farthestDistance = 0f;
        for(int i = 0; i < safeLocations.Length; i++)
        {
            float newDistance = Vector3.Distance(player.transform.position, safeLocations[i].transform.position);
            if (newDistance > farthestDistance)
            {
                farthestDistance = newDistance;
                safeDestination = safeLocations[i].transform.position;
            }
        }

        agent.SetDestination(safeDestination);

        //Begin reducing panic after 5 seconds
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
        //CheckHearing(other);
    }

    void OnTriggerStay(Collider other)
    {
        CheckSight(other);
        CheckHearing(other);
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
                Vector3 eyePosition = transform.position + new Vector3(0f, eyeHeight, 0f);
                Vector3 direction = other.transform.position - eyePosition;
                float angle = Vector3.Angle(transform.forward, direction);
                if (angle < viewConeAngle / 2f)
                {
                    RaycastHit hit;
                    Ray toObject = new Ray(eyePosition, direction);
                    Physics.Raycast(toObject, out hit);
                    if (hit.transform.tag == Tags.interactable)
                    {
                        Debug.DrawRay(eyePosition, direction, Color.red);
                        ScareAgent(interactable);
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Check if the player is within hearing range of any possessed object
    /// </summary>
    private void CheckHearing(Collider other)
    {
        InteractableObject interactable;
        if (interactable = other.transform.GetComponent<InteractableObject>())
        {
            if (interactable.spookey)
            {
                Vector3 direction = other.transform.position - transform.position;

                RaycastHit hit;
                Ray toObject = new Ray(transform.position, direction);
                Physics.Raycast(toObject, out hit);
                if (hit.transform.tag == Tags.interactable)
                {
                    Debug.DrawRay(transform.position, direction, Color.green);
                    ScareAgent(interactable);
                    return;
                }
            }
        }
    }

    private void ScareAgent(InteractableObject hauntedObject)
    {
        int addedFear = hauntedObject.ScareNPC(this);
        fearLevel += addedFear;
        panicLevel += addedFear;
        player.GetComponent<Interaction>().influence += addedFear;
        spooked = true;
        if(panicLevel < MaxPanicLevel)
        {
            LookAt(hauntedObject.transform.position);
        }
    }

    /// <summary>
    /// Gradually rotates agent on the y axis to look towards position
    /// </summary>
    private void LookAt(Vector3 position)
    {
        Quaternion oldRotation = transform.rotation;
        transform.LookAt(position);
        Quaternion desiredRot = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f));
        transform.rotation = Quaternion.Slerp(oldRotation, desiredRot, 0.2f);
    }
}
