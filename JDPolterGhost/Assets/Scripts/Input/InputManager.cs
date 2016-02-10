using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public bool debugInputs;

    public GameObject player;
    public PlayerCamera playerCamera;
    public PlayerMovement playerMovement;
    public Interaction playerInteraction;

    public string axisLeftRight = "LeftRight";
    public string axisForwardBack = "ForwardBack";
    public string axisUpDown = "UpDown";

    public bool invertCamera;
    [Range(0.1f, 10f)]
    public float camHorizontalSensitivity = 1f;
    [Range(0.1f, 10f)]
    public float camVerticalSensitivity = 1f;
    public string camLeftRight = "CamLeftRight";
    public string camUpDown = "CamUpDown";

    public string interact1 = "Interact1";

    public bool interact1Held = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        playerCamera = GameObject.FindGameObjectWithTag(Tags.camera).GetComponent<PlayerCamera>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerInteraction = player.GetComponent<Interaction>();
    }

    void Update()
    {
        Moveplayer();
        MoveCamera();
        Interact();
    }

    private void Moveplayer()
    {
        Vector3 movementVector = Vector3.zero;
        movementVector.x = Input.GetAxis(axisLeftRight);
        movementVector.y = Input.GetAxis(axisUpDown);
        movementVector.z = Input.GetAxis(axisForwardBack);

        playerMovement.MovePlayer(movementVector);

        if (debugInputs)
        {
            Debug.Log(movementVector);
        }
    }

    private void MoveCamera()
    {
        Vector3 movementVector = Vector3.zero;
        movementVector.x = Input.GetAxis(camLeftRight) * camHorizontalSensitivity;
        movementVector.y = Input.GetAxis(camUpDown) * camVerticalSensitivity * (invertCamera ? 1f : -1f);

        //TODO: Pass vertical movement to camera, and horizontal movement to player
        //playerMovement.rotatePlayer(movementVector.x);
        playerCamera.MoveCamera(movementVector);

        if (debugInputs)
        {
            Debug.Log(movementVector);
        }
    }

    private void Interact()
    {
        float interactValue = Input.GetAxis(interact1);

        if (interact1Held == false && interactValue > 0.1f)
        {
            interact1Held = true;
            //TODO: Pass interact to player
            playerInteraction.InteractWithTarget();

            if (debugInputs)
            {
                Debug.Log("Pressed interact button!");
            }
        }

        if(interactValue < 0.1f)
        {
            interact1Held = false;
        }
    }
}
