using UnityEngine;
using System.Collections;

public class dayNightSun : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        transform.eulerAngles = Vector3.right * TimeToRotation();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.eulerAngles = Vector3.right * TimeToRotation();
    }

    private float TimeToRotation()
    {
        return GameTime.TimeOfDay() * (360f / 24f) - 90f;
    }
}
