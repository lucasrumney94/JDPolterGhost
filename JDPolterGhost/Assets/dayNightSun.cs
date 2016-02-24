using UnityEngine;
using System.Collections;

public class dayNightSun : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.RotateAround(Vector3.zero, Vector3.right, 5.0f*Time.deltaTime);
	}
}
