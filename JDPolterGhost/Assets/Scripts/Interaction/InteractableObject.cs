using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Base class for interactible objects, will handle detecting player interaction and graphical effects to indicate interaction
/// </summary>
/// 
[RequireComponent(typeof(Collider))]
public class InteractableObject : MonoBehaviour
{
    public bool haunted; //is the player using the object?

    public bool spookey; //Is the object in a state that should scare people?

    public int spookiness; //How much to add to the fear level of an AI each time it consumes influence

    public int influenceCost = 1; //influence taken from the player to interact with this object

    public int influenceGate = 0; //minimum influence the player must have to interact with this object

    //Minimum time in seconds between activations
    //TODO: Find a way to tie this to animations?
    public float timeout = 1f;

    public float hauntingCameraDistance = 2f;

    public float lastActivationTime;

    private List<AIAgentBase> frightenedAgents = new List<AIAgentBase>(); //Agents that have been scared by the current activation of this object; be sure to ClearFrightenedAgents() after each activation!

    void Start()
    {
        lastActivationTime = Time.time;
    }

    public int ScareNPC(AIAgentBase agent)
    {
        if (frightenedAgents.Contains(agent) == false)
        {
            frightenedAgents.Add(agent);
            return spookiness;
        }
        else return 0;
    }

    public void ClearFrightenedAgents()
    {
        frightenedAgents.Clear();
    }

	public virtual void Highlight()
    {
        Debug.Log(gameObject.name + " is the closest interactible!");
        //TODO: Apply some graphical effect to show the object is being interacted with
    }

    /// <summary>
    /// Returns if the object was successfully activated or not
    /// </summary>
    /// <returns></returns>
    public virtual bool Activate()
    {
        if(Time.time - lastActivationTime > timeout)
        {
            Debug.Log("Activated the base class interaction! Maybe you forgot to implement an override for your behavior?");
            lastActivationTime = Time.time;
        }
        return false;
    }
}
