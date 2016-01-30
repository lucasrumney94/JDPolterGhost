using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// To be attached to the player main GameObject, casts a sphere from the center of the camera to detect and activate the closest InteractibleObject
/// </summary>
public class Interaction : MonoBehaviour
{
    public Camera mainCamera;
    public InteractableObject targetedInteractible;

    public float sphereCastRadius;

	void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag(Tags.camera).GetComponent<Camera>();
    }

    void Update()
    {
        InteractSphereCast();
    }

    public void InteractWithTarget()
    {
        if(targetedInteractible != null)
        {
            Debug.Log(targetedInteractible.gameObject.name + " was interacted with!");
            //Determine the kind of interactable, then use a switch statement to call the appropriate function
        }
    }

    private void InteractSphereCast()
    {
        RaycastHit hit = new RaycastHit();

        Vector3 cameraCenter = mainCamera.transform.position;
        Vector3 cameraNormal = mainCamera.transform.forward;
        Ray interactRay = new Ray(cameraCenter, cameraNormal);
        
        Physics.SphereCast(interactRay, sphereCastRadius, out hit);

        if(targetedInteractible = hit.transform.gameObject.GetComponent<InteractableObject>())
        {
            targetedInteractible.Highlight();
        }
    }
}
