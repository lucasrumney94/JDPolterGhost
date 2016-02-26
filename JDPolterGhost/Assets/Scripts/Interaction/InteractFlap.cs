using UnityEngine;
using System.Collections;

public class InteractFlap : InteractableObject
{
    public GameObject arm; //Object to rotate, should be an empty object at the hinge point of what you're rotating
    
    public Vector3 rotationAxis; //Make negative to reverse direction, change magnitude to speed up rotation

    public float angleLimit; //Max angle in degrees

    public Vector3 originalRotation;

    public bool flapping;
    public bool returningToStart = false;

    void Start()
    {
        originalRotation = arm.transform.eulerAngles;
    }

    void Update()
    {
        if (flapping)
        {
            Rotate();
        }
    }

    public override bool Activate()
    {
        if (spookey == false)
        {
            spookey = true;
            flapping = true;
            return true;
        }
        return false;
    }

    private void Rotate()
    {
        if (returningToStart)
        {
            float previousAngleDiff = Vector3.Distance(originalRotation, arm.transform.eulerAngles);
            arm.transform.eulerAngles -= Time.deltaTime * rotationAxis;
            float currentAngleDiff = Vector3.Distance(originalRotation, arm.transform.eulerAngles);
            if(currentAngleDiff > previousAngleDiff) //Has the rotation gone past the original position?
            {
                returningToStart = false;
                flapping = false;
                spookey = false;
                ClearFrightenedAgents();
                arm.transform.eulerAngles = originalRotation;
            }
        }
        else
        {
            arm.transform.eulerAngles += Time.deltaTime * rotationAxis;
            if(Vector3.Distance(originalRotation, arm.transform.eulerAngles) > angleLimit)
            {
                returningToStart = true;
            }
        }
    }
}
