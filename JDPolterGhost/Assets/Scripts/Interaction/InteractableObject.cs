using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for interactible objects, will handle detecting player interaction and graphical effects to indicate interaction
/// </summary>
/// 
[RequireComponent(typeof(Collider))]
public class InteractableObject : MonoBehaviour
{
    //is the player using the object?
    public bool haunted;

    //Is the object in a state that should scare people?
    public bool spookey;

    //influence taken from the player to interact with this object
    public int influenceCost = 1;

    //minimum influence the player must have to interact with this object
    public int influenceGate = 0;

    //Minimum time in seconds between activations
    //TODO: Find a way to tie this to animations?
    public float timeout = 1f;

    public float hauntingCameraDistance = 2f;

    private float lastActivationTime;

    void Start()
    {
        lastActivationTime = Time.time;
    }

	public virtual void Highlight()
    {
        Debug.Log(gameObject.name + " is the closest interactible!");
        //TODO: Apply some graphical effect to show the object is being interacted with
    }

    public virtual void Activate()
    {
        if(Time.time - lastActivationTime > timeout)
        {
            Debug.Log("Actibated the base class interaction! Maybe you forgot to implement an override for your behavior?");
            lastActivationTime = Time.time;
        }
    }
}
