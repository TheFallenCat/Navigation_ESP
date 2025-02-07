using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailSpeed : MonoBehaviour
{
    [SerializeField] Rigidbody rb; // The boat's Rigidbody
    [SerializeField] Transform sail; // The sail Transform
    [SerializeField] WindGenerator windGenerator;

    [SerializeField] float MaxSpeed = 10f;

    public float forceOnSail;
    public float sailEfficiency = 50f;

    void FixedUpdate()
    {
        

        // Get sail direction
        Vector3 sailDirection = sail.forward;


        // Compute final force applied to the boat
        float force = forceOnSail = sailEfficiency * windGenerator.activeWindSpeed  / Vector3.Distance(sail.forward.normalized, windGenerator.activeWindDirection.normalized);

        // Apply force to the boat
        ApplyForceToReachVelocity(rb, Vector3.forward * MaxSpeed, force);
    }

    void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
    {
        if (force == 0 || velocity.magnitude == 0)
            return;

        velocity = velocity + velocity.normalized * 0.2f * rigidbody.drag;

        //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
        force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

        //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude https://www.youtube.com/watch?v=h0NJK4mEIJU
        if (rigidbody.velocity.magnitude == 0)
        {
            rigidbody.AddForce(velocity * force, mode);
        }
        else
        {
            var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.velocity) / velocity.magnitude);
            rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
        }
    }
}

