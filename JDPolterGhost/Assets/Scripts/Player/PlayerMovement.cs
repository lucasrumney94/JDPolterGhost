using UnityEngine;
using System.Collections;

/// <summary>
/// This script should be attached to the player, and handles all the forces applied.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public static GameObject player;
    public GameObject playerCamera;

    public bool forceMovement = false;
    public float speed = 5.0f;
    [Range(0f, 1f)]
    public float rotationSpeed = 0.1f;

    private Rigidbody playerRigidbody;

    void Start()
    {
        player = this.gameObject;
        playerCamera = GameObject.FindGameObjectWithTag(Tags.camera);
        playerRigidbody = GetComponent<Rigidbody>();
    }


    /// <summary>
    /// Moves the player smoothly in the direction of movementVector
    /// </summary>
    /// <param name="movementVector">Direction of movement</param>
    public void MovePlayer(Vector3 movementVector)
    {
        if (forceMovement) MoveForce(movementVector);
        else MoveVelocity(movementVector);
        if(movementVector.magnitude > 0.01f)
        {
            RotateToCamera();
        }
    }

    private void MoveForce(Vector3 movementVector)
    {
        playerRigidbody.AddRelativeForce(movementVector);
    }

    private void MoveVelocity(Vector3 movementVector)
    {
        playerRigidbody.velocity = transform.TransformVector(movementVector).normalized * speed;
    }

    private void RotateToCamera()
    {
        player.transform.rotation = Quaternion.Slerp(transform.rotation, playerCamera.transform.rotation, rotationSpeed);
    }

    /// <summary>
    /// Rotates the Player around the Y-Axis
    /// </summary>
    /// <param name="yRotation">[-1,1] what EulerAngle rotation to apply</param>
    public void rotatePlayer(float yRotation)
    {
        player.GetComponent<Rigidbody>().AddTorque(new Vector3(0.0f,yRotation,0.0f));
    }
}
