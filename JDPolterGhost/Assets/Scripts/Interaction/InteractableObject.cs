using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for interactible objects, will handle detecting player interaction and graphical effects to indicate interaction
/// </summary>
/// 
[RequireComponent(typeof(Collider))]
public class InteractableObject : MonoBehaviour
{
    //influence taken from the player to interact with this object
    public int influenceCost = 1;

    //minimum influence the player must have to interact with this object
    public int influenceGate = 0;

	public virtual void Highlight()
    {
        Debug.Log(gameObject.name + " is the closest interactible!");
        //TODO: Apply some graphical effect to show the object is being interacted with
    }
}
