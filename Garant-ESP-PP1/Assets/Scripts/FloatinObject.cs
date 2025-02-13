using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatinObject : MonoBehaviour
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float depthBeforeSubmerged;
    [SerializeField] float displacementAmount = 1f;
    [SerializeField] int floaterCount = 1;
    [SerializeField] float waterDrag = 0.99f;
    [SerializeField] float waterAngularDrag;

    [SerializeField] WaveGenerator waveGenerator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity * rb.mass / floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = waveGenerator.GetWaveHeightAtPosition(transform.position, Time.time);
        if(transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position ,ForceMode.Acceleration);
            rb.AddForce(displacementMultiplier * -rb.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * -rb.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}
