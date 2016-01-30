using UnityEngine;
using System.Collections;
/// <summary>
/// Base class for all NPCs; handles setting destinations for agents based on time of day, and contains functions for actions common to all NPCs
/// (eating, sleeping, etc.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class AIAgentBase : MonoBehaviour
{
    public bool spooked = false;

    #region Schedule

    public int bedtime;
    public int wakeuptime;

    public int breakfastStartTime;
    public int breakfastEndTime;

    public int lunchStartTime;
    public int lunchEndTime;

    public int dinnerStartTime;
    public int dinnerEndTime;

    #endregion

    #region Locations



    #endregion

    private void DetermineActivityByTime()
    {
        //TODO: Switch statement for finding agent destination

    }
}
