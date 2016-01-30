using UnityEngine;
using System.Collections;

/// <summary>
/// Base class for interactible objects, will handle detecting player interaction and graphical effects to indicate interaction
/// </summary>
public class InteractableObject : MonoBehaviour
{
	public virtual void Highlight()
    {
        //TODO: Apply some graphical effect to show the object is being interacted with
    }
}
