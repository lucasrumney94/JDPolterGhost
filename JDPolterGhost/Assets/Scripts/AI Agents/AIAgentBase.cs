using UnityEngine;
using System.Collections;
/// <summary>
/// Base class for all NPCs; handles setting destinations for agents based on time of day, and contains functions for actions common to all NPCs
/// (eating, sleeping, etc.)
/// All floats in #Schedule should strictly increase going down the list
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AIAgentBase : MonoBehaviour
{
    public bool spooked = false;

    #region Schedule

    public float wakeuptime;

    public float breakfastStartTime;
    public float breakfastEndTime;

    public float lunchStartTime;
    public float lunchEndTime;

    public float dinnerStartTime;
    public float dinnerEndTime;

    public float bedtime;

    #endregion

    #region Locations

    public GameObject bed;

    public GameObject diningChair;

    public GameObject defaultObject;

    #endregion

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
    /// Makes sure that the given times are ordered correctly
    /// These things should always be true:
    /// -bedtime is the latest time in the list
    /// -wakeuptime is the earliest time in the list
    /// -all meals are ordered correctly
    /// -all mealStartTimes come before their respective mealEndTimes
    /// 
    /// These actions will always take precedence over inherited class actions, so their ordering is not importaint
    /// </summary>
    private void AssertTimeOrder()
    {
        Debug.Assert(wakeuptime < breakfastStartTime, gameObject);
        Debug.Assert(breakfastStartTime < breakfastEndTime, gameObject);
        Debug.Assert(breakfastEndTime < lunchStartTime, gameObject);
        Debug.Assert(lunchStartTime < lunchEndTime, gameObject);
        Debug.Assert(lunchEndTime < dinnerStartTime, gameObject);
        Debug.Assert(dinnerStartTime < dinnerEndTime, gameObject);
        Debug.Assert(dinnerEndTime < bedtime, gameObject);
    }

    private void DetermineActivityByTime()
    {
        //TODO: Switch statement for finding agent destination
        float time = GameTime.TimeOfDay();

        if (spooked == false)
        {
            if (bedtime < time || time < wakeuptime)
            {
                GoToBed();
            }
            else if (breakfastStartTime < time && time < breakfastEndTime)
            {
                GoToEat();
            }
            else if (lunchStartTime < time && time < lunchEndTime)
            {
                GoToEat();
            }
            else if (dinnerStartTime < time && time < dinnerEndTime)
            {
                GoToEat();
            }
            else
            {
                GoToDefault();
            }
        }
    }

    private void GoToBed()
    {
        agent.SetDestination(bed.transform.position);
    }

    private void GoToEat()
    {
        agent.SetDestination(diningChair.transform.position);
    }

    private void GoToDefault()
    {
        agent.SetDestination(defaultObject.transform.position);
    }
}
