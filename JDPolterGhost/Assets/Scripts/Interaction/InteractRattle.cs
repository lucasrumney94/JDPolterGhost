using UnityEngine;
using System.Collections;

public class InteractRattle : InteractableObject
{
    public float rattleSpeed;
    public Vector3 rattleRange;

    public Vector3 initialPosition;

    private bool moving = false;
    private Vector3 targetPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (moving)
        {
            LerpToPosition();
        }
    }

    public override bool Activate()
    {
        if (spookey == false)
        {
            StartCoroutine(Rattle());
            lastActivationTime = Time.time; //Depreciated for this behavior; left in for testing
            return true;
        }
        return false;
    }

    private IEnumerator Rattle()
    {
        spookey = true;
        moving = true;
        SetTargetPosition();

        yield return new WaitForSeconds(timeout);

        ResetPosition();
        spookey = false;
        ClearFrightenedAgents();
    }

    private void SetTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(-rattleRange.x, rattleRange.x), Random.Range(-rattleRange.y, rattleRange.y), Random.Range(-rattleRange.z, rattleRange.z));
    }

    private void LerpToPosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * rattleSpeed);
    }

    private void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
