using UnityEngine;
using System.Collections;
/// <summary>
/// Base class for all NPCs; allows you to set a schedule 
/// All floats in #Schedule should strictly increase going down the list
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AIAgentBase : MonoBehaviour
{
    public bool spooked = false;

    //Location of bed and sleeping hours, unlike other ScheduleItems endTime should be BEFORE startTime
    public ScheduleItem sleep;

    //List of objects and times for the agent to visit through the day
    public ScheduleItem[] schedule;

    //List of objects for the agent to visit at random between schedule items
    public GameObject[] miscLocations;

    //Has the agent determined what to do during its schedule gap?
    private bool selectedFreeTimeLocation;

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

        if (spooked == false)
        {
            if((sleep.startTime < time) || (time < sleep.endTime))
            {
                GoToBed();
                return;
            }
            for(int i = 0; i < schedule.Length; i++)
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

    private void GoToBed()
    {
        agent.SetDestination(sleep.location.transform.position);
        //TODO: Implement animations and variable changes for this AI state
    }
}
