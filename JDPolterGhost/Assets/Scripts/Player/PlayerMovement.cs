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

    void Start()
    {
        player = this.gameObject;

    }


    /// <summary>
    /// Moves the player smoothly in the direction of movementVector
    /// </summary>
    /// <param name="movementVector">Direction of movement</param>
    public void MovePlayer(Vector3 movementVector)
    {
        player.GetComponent<Rigidbody>().AddRelativeForce(movementVector);
    }

    /// <summary>
    /// Rotates the Player around the Y-Axis
    /// </summary>
    /// <param name="yRotation">[-1,1] what EulerAngle rotation to apply</param>
    public void rotatePlayer(float yRotation)
    {
        player.GetComponent<Rigidbody>();
    }
}
