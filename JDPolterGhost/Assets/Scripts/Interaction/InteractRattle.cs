using UnityEngine;
using System.Collections;

public class InteractRattle : InteractableObject
{
    public float rattleIntensity = 0.1f;

    public Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    public override void Activate()
    {
        if (spookey == false)
        {
            StartCoroutine(Rattle());
            lastActivationTime = Time.time; //Depreciated for this behavior; left in for testing
        }
    }

    private IEnumerator Rattle()
    {
        spookey = true;
        Vector3 offset = new Vector3(Random.Range(-rattleIntensity, rattleIntensity), Random.Range(-rattleIntensity, rattleIntensity), Random.Range(-rattleIntensity, rattleIntensity));
        transform.position = Vector3.Lerp(transform.position, initialPosition + offset, 0.1f);

        yield return new WaitForSeconds(timeout);

        ResetPosition();
        spookey = false;
        ClearFrightenedAgents();
    }

    private void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
