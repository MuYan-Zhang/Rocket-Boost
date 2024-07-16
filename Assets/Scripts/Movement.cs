using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustMultiplier = 1f;
    [SerializeField] float rotationMultiplier = 1f;
    [SerializeField] AudioClip mainEngine;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            rb.AddRelativeForce(Vector3.up * thrustMultiplier * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (!(Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                ApplyRotation(rotationMultiplier);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                ApplyRotation(-rotationMultiplier);
            }
        }
    }

    void ApplyRotation(float rotationMultiplier)
    {
        rb.freezeRotation = true; // Pause the physics engine so the player can freely rotate
        transform.Rotate(Vector3.forward * rotationMultiplier * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
