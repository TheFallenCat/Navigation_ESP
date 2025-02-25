using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SailSpeed))]
public class ShipController : MonoBehaviour
{
    //visible Properties
    [SerializeField] Transform Rudder;
    [SerializeField] float SteerPower = 500f;
    [SerializeField] float Power = 5f;
    [SerializeField] float Drag = 0.1f;



    //used Components
    Rigidbody Rigidbody;
    SailSpeed SailSpeed;
    Controller Controller;
    public bool isAnchored = false;

    public void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        SailSpeed = GetComponent<SailSpeed>();
        Controller = GameObject.Find("Controller").GetComponent<Controller>();
    }

    public void FixedUpdate()
    {
        //default direction
        var forceDirection = transform.forward;

        //steer
        var steer = 0;
        if (Input.GetKey(KeyCode.A))
            steer = 1;
        if (Input.GetKey(KeyCode.D))
            steer = -1;
        Steering(steer);
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
        if (other.CompareTag("AnchorPoint"))
        {
            int portIndex = other.GetComponent<PortAnchor>().portIndex;
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isAnchored)
                {
                    isAnchored = false;
                    Controller.SetSail();
                }
                else
                {
                    isAnchored = true;
                    Controller.AnchorAtPort(portIndex);
                }
                SailSpeed.SwitchAnchor();
            }
        }
    }
}