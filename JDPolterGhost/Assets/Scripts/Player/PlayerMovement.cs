using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be attached to the player, and handles all the forces applied.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public static GameObject player;

    public float speed = 5.0f;
<<<<<<< HEAD
=======
    [Range(0f, 1f)]
    public float rotationSpeed = 0.1f;

    private Rigidbody playerRigidbody;
    private Interaction playerInteraction;
>>>>>>> 7ef62bfb219949fe5f7b4fa747d48fb33275fd7a

    void Start()
    {
        player = this.gameObject;
<<<<<<< HEAD

=======
        playerCamera = GameObject.FindGameObjectWithTag(Tags.camera);
        playerRigidbody = GetComponent<Rigidbody>();
        playerInteraction = GetComponent<Interaction>();
>>>>>>> 7ef62bfb219949fe5f7b4fa747d48fb33275fd7a
    }
    
    /// <summary>
    /// Moves the player smoothly in the direction of movementVector
    /// </summary>
    /// <param name="movementVector">Direction of movement</param>
    public void MovePlayer(Vector3 movementVector)
    {
<<<<<<< HEAD
        player.GetComponent<Rigidbody>().AddRelativeForce(movementVector);
=======
        if (playerInteraction.hauntingObject == false)
        {
            if (forceMovement) MoveForce(movementVector);
            else MoveVelocity(movementVector);
            if (movementVector.magnitude > 0.05f)
            {
                RotateToCamera();
            }
        }
        else
        {
            playerRigidbody.velocity = Vector3.zero;
        }
    }

    private void MoveForce(Vector3 movementVector)
    {
        playerRigidbody.AddRelativeForce(movementVector);
    }

    private void MoveVelocity(Vector3 movementVector)
    {
        if (movementVector.magnitude > 1f) movementVector = movementVector.normalized;
        playerRigidbody.velocity = transform.TransformVector(movementVector) * speed;
    }

    private void RotateToCamera()
    {
        player.transform.rotation = Quaternion.Slerp(transform.rotation, playerCamera.transform.rotation, rotationSpeed);
>>>>>>> 7ef62bfb219949fe5f7b4fa747d48fb33275fd7a
    }
}
