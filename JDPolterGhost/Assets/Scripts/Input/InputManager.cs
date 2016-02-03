using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour
{
    public bool debugInputs;

    public GameObject player;
    public cameraController mainCamera;
    public PlayerMovement playerMovement;

    public string axisLeftRight = "LeftRight";
    public string axisForwardBack = "ForwardBack";
    public string axisUpDown = "UpDown";

    public string camLeftRight = "CamLeftRight";
    public string camUpDown = "CamUpDown";

    public string interact1 = "Interact1";

    public bool interact1Held = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
        mainCamera = GameObject.FindGameObjectWithTag(Tags.camera).GetComponent<cameraController>();
        playerMovement = player.GetComponent<PlayerMovement>();
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
        movementVector.x = Input.GetAxis(camLeftRight);
        movementVector.y = Input.GetAxis(camUpDown);

        //TODO: Pass vertical movement to camera, and horizontal movement to player
        playerMovement.rotatePlayer(movementVector.x);
        mainCamera.changeElevation(movementVector.y);

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
