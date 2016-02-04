using UnityEngine;
using System.Collections;
using System;

public class PlayerCamera : MonoBehaviour
{
    public float cameraDistance; //Radius of camera movement sphere

    public float currentLongitude = 0;
    private float targetLongitude;

    public float currentLatitude = 0;
    private float targetLatitude;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }

	public void MoveCamera(Vector3 cameraDelta)
    {
        OrbitCamera(Mathf.Deg2Rad * cameraDelta.x, Mathf.Deg2Rad * cameraDelta.y);
        LookAtPlayer();
    }

    /// <summary>
    /// Change Latitude of the camera, bound to within +/-85 degrees
    /// </summary>
    /// <param name="angle">Camera delta in radians</param>
    private void OrbitCamera(float angleX, float angleY)
    {
        targetLatitude = Mathf.Clamp(currentLatitude + angleY, Mathf.Deg2Rad * -85f, Mathf.Deg2Rad * 85f);
        targetLongitude = currentLongitude + angleX;

        float newY = Mathf.Sin(targetLatitude) * cameraDistance;

        //Adjust x,z based on y position
        float newX = -Mathf.Sin(targetLongitude) * Mathf.Sqrt((cameraDistance * cameraDistance) - (newY * newY));
        float newZ = -Mathf.Cos(targetLongitude) * Mathf.Sqrt((cameraDistance * cameraDistance) - (newY * newY));

        Vector3 newPosition = new Vector3(newX, newY, newZ) + player.transform.position;
        transform.position = newPosition;

        currentLatitude = targetLatitude;
        currentLongitude = targetLongitude;
    }

    /// <summary>
    /// Turn the camera to face the player
    /// </summary>
    private void LookAtPlayer()
    {
        transform.LookAt(player.transform);
    }
}
