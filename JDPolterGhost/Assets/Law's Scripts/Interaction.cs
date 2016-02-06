using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// To be attached to the player main GameObject, casts a sphere from the center of the camera to detect and activate the closest InteractibleObject
/// </summary>
public class Interaction : MonoBehaviour
{
    public float influence = 0f;
    public LayerMask interactMask = -1;

    public InteractableObject targetedInteractible;
    public bool hauntingObject;

    private PlayerCamera playerCamera;
    private Collider playerCollider;

    private MeshRenderer playerMesh;
    private PlayerMovement playerMovement;

    public float sphereCastRadius;

	void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag(Tags.camera).GetComponent<PlayerCamera>();
        playerCollider = GetComponent<Collider>();
        playerMesh = GetComponent<MeshRenderer>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //InteractSphereCast();
    }

    public void InteractWithTarget()
    {
        if(targetedInteractible != null)
        {
            if (hauntingObject == false)
            {
                if (influence >= targetedInteractible.influenceGate)
                {
                    Debug.Log(targetedInteractible.gameObject.name + " was interacted with!");
                    hauntingObject = true;
                    targetedInteractible.haunted = true;
                    playerMesh.enabled = false;
                    transform.position = targetedInteractible.transform.position; 
                }
            }
            else if(hauntingObject == true)
            {
                hauntingObject = false;
                targetedInteractible.haunted = false;
                playerMesh.enabled = true;
            }
        }
    }

    public void ActivateTarget()
    {
        if(hauntingObject == true)
        {
            if(influence > targetedInteractible.influenceCost)
            {
                targetedInteractible.Activate();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == Tags.interactable)
        {
            targetedInteractible = other.GetComponent<InteractableObject>();
            //TODO: Trigger a graphical effect for the highlighted object
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == Tags.interactable)
        {
            targetedInteractible = null;
        }
    }
}
