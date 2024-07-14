using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float thrustMultiplier = 1f;
    [SerializeField] float rotationMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
