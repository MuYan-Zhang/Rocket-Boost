using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustMultiplier = 1f;
    [SerializeField] float rotationMultiplier = 1f;
    [SerializeField] AudioClip mainBoosterAudio;
    [SerializeField] AudioClip turnBoosterAudio;
    [SerializeField] ParticleSystem mainBoosterParticles;
    [SerializeField] ParticleSystem leftBoosterParticles;
    [SerializeField] ParticleSystem rightBoosterParticles;

    Rigidbody rb;
    AudioSource boosterAudioSource;

    bool isBoosting = false, isRotating = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boosterAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartBoosting();
        }
        else
        {
            StopBoosting();
        }
    }

    void ProcessRotation()
    {
        // Cannot turn if both thrusters are engaged simultaneously
        if (!(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                RotateLeft();
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                RotateRight();
            }
            else
            {
                StopRotation();
            }
        }
    }

    void StartBoosting()
    {
        isBoosting = true;
        rb.AddRelativeForce(Vector3.up * thrustMultiplier * Time.deltaTime);
        if (!boosterAudioSource.isPlaying)
        {
            boosterAudioSource.PlayOneShot(mainBoosterAudio);
        }
        if (!mainBoosterParticles.isPlaying)
        {
            mainBoosterParticles.Play();
        }
    }

    void StopBoosting()
    {
        isBoosting = false;
        if (!isRotating)
        {
            boosterAudioSource.Stop();
        }
        mainBoosterParticles.Stop();
    }

    void RotateLeft()
    {
        isRotating = true;
        ApplyRotation(rotationMultiplier);
        if (!boosterAudioSource.isPlaying)
        {
            boosterAudioSource.PlayOneShot(turnBoosterAudio);
        }
        if (!rightBoosterParticles.isPlaying)
        {
            rightBoosterParticles.Play();
        }
    }

    void RotateRight()
    {
        isRotating = true;
        ApplyRotation(-rotationMultiplier);
        if (!boosterAudioSource.isPlaying)
        {
            boosterAudioSource.PlayOneShot(turnBoosterAudio);
        }
        if (!leftBoosterParticles.isPlaying)
        {
            leftBoosterParticles.Play();
        }
    }

    void StopRotation()
    {
        isRotating = false;
        if (!isBoosting)
        {
            boosterAudioSource.Stop();
        }
        rightBoosterParticles.Stop();
        leftBoosterParticles.Stop();
    }

    void ApplyRotation(float rotationMultiplier)
    {
        rb.freezeRotation = true; // Pause the physics engine so the player can freely rotate
        transform.Rotate(Vector3.forward * rotationMultiplier * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
