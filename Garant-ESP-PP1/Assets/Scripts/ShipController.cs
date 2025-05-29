using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SailSpeed))]
public class ShipController : MonoBehaviour
{
    //visible Properties
    [SerializeField] Transform Rudder;
    [SerializeField] Transform Sail;
    [SerializeField] float SteerPower = 500f;
    [SerializeField] float Drag = 0.1f;

    public const float MIN_SAIL_SIZE = 0.2f;
    public const float MAX_SAIL_SIZE = 1;
    const float SAIL_RAISING_SPEED = 0.2f;
    const float SAIL_LOWERING_SPEED = 0.5f;

    //used Components
    Rigidbody Rigidbody;
    SailSpeed SailSpeed;


    public bool isAnchored = false;
    public PortAnchor portInRange;
    public bool tryAnchor = false;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        SailSpeed = GetComponent<SailSpeed>();
        portInRange = null;
    }

    void RaiseSail()
    {
        if (Sail.localScale.y >= MIN_SAIL_SIZE)
        {
            Sail.localScale -= new Vector3(0, SAIL_RAISING_SPEED * Time.fixedDeltaTime, 0);
            SailSpeed.sailEfficiency -= SAIL_RAISING_SPEED * Time.fixedDeltaTime;
        }
    }

    void LowerSail()
    {
        if (Sail.localScale.y <= MAX_SAIL_SIZE)
        {
            Sail.localScale += new Vector3(0, SAIL_LOWERING_SPEED * Time.fixedDeltaTime, 0);
            SailSpeed.sailEfficiency += SAIL_LOWERING_SPEED * Time.fixedDeltaTime;
        }
    }

    public void SwitchAnchor()
    {
        SailSpeed.SwitchAnchor();
    }

    public void FixedUpdate()
    {
        //steer
        var steer = 0;
        if (Input.GetKey(KeyCode.A))
            steer = 1;
        if (Input.GetKey(KeyCode.D))
            steer = -1;
        Steering(steer);

        if (Input.GetKey(KeyCode.S))
        {
            RaiseSail();
        }

        if (Input.GetKey(KeyCode.W))
        {
            LowerSail();
        }
    }

    void Steering(int steer)
    {
        //Rotational Force
        Rigidbody.AddForceAtPosition(steer * transform.right * SteerPower / 100f, Rudder.position);


        //Motor Animations
        //Rudder.SetPositionAndRotation(Rudder.position, transform.rotation * StartRotation * Quaternion.Euler(0, 30f * steer, 0));

        //moving forward
        var movingForward = Vector3.Cross(transform.forward, Rigidbody.velocity).y < 0;

        //move in direction
        Rigidbody.velocity = Quaternion.AngleAxis(Vector3.SignedAngle(Rigidbody.velocity, (movingForward ? 1f : 0f) * transform.forward, Vector3.up) * Drag, Vector3.up) * Rigidbody.velocity;
    }

    private void OnTriggerStay(Collider other)
    {
        //Check for ports, 
        if (other.CompareTag("AnchorPoint"))
        {
            portInRange = other.GetComponent<PortAnchor>();
        }
        else { portInRange = null; }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check for ports, 
        if (other.CompareTag("AnchorPoint"))
        {
            portInRange = null;
        }
    }
}