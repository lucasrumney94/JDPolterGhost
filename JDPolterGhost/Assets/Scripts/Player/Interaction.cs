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

    // private MeshRenderer playerMesh;            //disabled because player is now a particle system
    private ParticleSystem playerParticleSystem;
    private PlayerMovement playerMovement;
    private NavMeshObstacle playerObstacle;

    public float sphereCastRadius;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag(Tags.camera).GetComponent<PlayerCamera>();
        playerCollider = GetComponent<Collider>();
        playerParticleSystem = GetComponent<ParticleSystem>();
        //playerMesh = GetComponent<MeshRenderer>();            //disabled because player is now a particle system
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        //InteractSphereCast();
    }

    public void InteractWithTarget()
    {
        if (targetedInteractible != null)
        {
            if (hauntingObject == false)
            {
                if (influence >= targetedInteractible.influenceGate)
                {
                    Debug.Log(targetedInteractible.gameObject.name + " was interacted with!");
                    hauntingObject = true;
                    targetedInteractible.haunted = true;
                    playerParticleSystem.enableEmission = false;
                    //playerMesh.enabled = false;            //disabled because player is now a particle system
                    transform.position = targetedInteractible.transform.position;
                }
            }
            else if (hauntingObject == true)
            {
                hauntingObject = false;
                targetedInteractible.haunted = false;
                playerParticleSystem.enableEmission = true;
                //playerMesh.enabled = true;            //disabled because player is now a particle system
            }
        }
    }

    public void ActivateTarget()
    {
        if (hauntingObject == true)
        {
            if (influence > targetedInteractible.influenceCost)
            {
                if (targetedInteractible.Activate())
                {
                    influence -= targetedInteractible.influenceCost;
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.isTrigger == false)
        {
            if (other.tag == Tags.interactable)
            {
                targetedInteractible = other.GetComponent<InteractableObject>();
                //TODO: Trigger a graphical effect for the highlighted object
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (hauntingObject == false)
        {
            if (other.tag == Tags.interactable)
            {
                targetedInteractible = null;
            }
        }
    }
}
