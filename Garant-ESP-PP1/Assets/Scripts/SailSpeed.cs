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
    public float angleEfficiency;
    public float angleDistance;
    public float sailEfficiency = 1f;
    public float atSea = 1;

    void FixedUpdate()
    {


        // Get sail direction
        Vector3 sailDirection = sail.forward;


        // Compute final force applied to the boat
        angleDistance = Vector3.Distance(sail.forward.normalized, windGenerator.activeWindDirection.normalized);
        angleEfficiency = -Mathf.Log(Vector3.Distance(sail.forward.normalized, windGenerator.activeWindDirection.normalized));
        if (angleEfficiency < 0)
            angleEfficiency = 0;
        float force = windGenerator.activeWindSpeed * angleEfficiency * 5;
        if (force < 5f)
        {
            force = 5f;
        }
        else if (force > 30f)
        {
            force = 30f;
        }
        forceOnSail = (sailEfficiency - 0.2f) * force * atSea;
        

        //if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.D))
        //    forceOnSail /= 2;

        // Apply force to the boat
        ApplyForceToReachVelocity(rb, transform.forward * MaxSpeed, forceOnSail);
    }

    public void SwitchAnchor()
    {
        if (atSea == 0)
            atSea = 1f;
        else
            atSea = 0f;

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

