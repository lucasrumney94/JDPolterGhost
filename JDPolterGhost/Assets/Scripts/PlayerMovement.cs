using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public static GameObject player;

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
        player.GetComponent<Rigidbody>().AddForce(movementVector);
    }
}
