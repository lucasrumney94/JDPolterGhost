using UnityEngine;
using System.Collections;

public class InteractRattle : InteractableObject
{
    public Vector3 rattleRange;

    public Vector3 initialPosition;

    public ParticleSystem movementParticles; //Please setup the particle system's duration and emission to fit the animation duration; There is not a good scriptable way to do this
    public AudioSource movementAudio;

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
            lastActivationTime = Time.time;
            return true;
        }
        return false;
    }

    private IEnumerator Rattle()
    {
        spookey = true;
        moving = true;
        SetTargetPosition();
        EmitParticles();
        EmitSound();

        yield return new WaitForSeconds(timeout);

        //ResetPosition();
        spookey = false;
        ClearFrightenedAgents();
    }

    private void EmitParticles()
    {
        if(movementParticles != null)
        {
            movementParticles.Play();
        }
    }

    private void EmitSound()
    {
        if(movementAudio != null)
        {
            movementAudio.Play();
        }
    }

    private void SetTargetPosition()
    {
        Vector3 targetOffset = new Vector3(Random.Range(-rattleRange.x, rattleRange.x), Random.Range(-rattleRange.y, rattleRange.y), Random.Range(-rattleRange.z, rattleRange.z));
        targetPosition = targetOffset + initialPosition;
    }

    private void LerpToPosition()
    {
        float progressTime = Mathf.Clamp((Time.time - lastActivationTime) / timeout, 0f, 1f);
        transform.position = Vector3.Lerp(transform.position, targetPosition, progressTime);
    }

    private void ResetPosition()
    {
        transform.position = initialPosition;
    }
}
