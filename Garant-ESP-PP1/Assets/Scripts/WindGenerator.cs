using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindGenerator : MonoBehaviour
{
    
    public Vector3 activeWindDirection;
    public float activeWindSpeed;
    
    [System.Serializable]
    struct WindSpeedBounds
    {
        public int min;
        public int max;
    }

    [SerializeField] int windSwitchTimer = 300;
    [SerializeField] int windSwitchDuration = 10;
    [SerializeField] WindSpeedBounds windSpeedBounds;
    public bool onHighSea;

    // Start is called before the first frame update
    void Start()
    {
        activeWindDirection = new Vector3(1, 0, 0);
        onHighSea = true;
        StartCoroutine(SwitchWindType());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    /// <summary>
    /// Continuous co-routine which switches the active wind type when the player is on the high seas.
    /// </summary>
    IEnumerator SwitchWindType()
    {
        while (true)
        {
            if (onHighSea)
            {
                float windDirectionX = Random.Range(-1.0f, 1.0f);
                float windDirectionZ = Random.Range(-1.0f, 1.0f);
                float windSpeed = Random.Range(windSpeedBounds.min, windSpeedBounds.max + 1);
                Vector3 newWindDirection = new Vector3(windDirectionX, 0, windDirectionZ); 
                StartCoroutine(WindTransitionTo(newWindDirection, windSpeed));
            }
            yield return new WaitForSeconds(windSwitchTimer);
        }
    }
    /// <summary>
    /// Co-routine to seemlesly transition from one wind type to another.
    /// </summary>
    /// <param name="windDirection">The direction of the wind to switch to.</param>
    /// <param name="windSpeed">The speed of the wind to switch to</param>
    public IEnumerator WindTransitionTo(Vector3 windDirection, float windSpeed)
    {
        Quaternion temp = Quaternion.identity;
        temp.SetFromToRotation(activeWindDirection, windDirection);
        float angle;//Vector3.Angle(windDirection, activeWindDirection);
        Vector3 temp1 = Vector3.zero;
        temp.ToAngleAxis(out angle, out temp1);
        if (angle > 180)
        {
            angle = (angle - 180) * -1;
        }
        float windSpeedDelta = windSpeed - activeWindSpeed;
        int iterations = (int)(1 / Time.fixedDeltaTime) * windSwitchDuration;
        float anglePerIteration = angle / iterations;
        float windSpeedDeltaPerIteration = windSpeedDelta / iterations;
        Vector3 rotatedDirection;
        for (int i = 0; i < iterations; i++)
        {
            rotatedDirection = RotateVector(anglePerIteration, activeWindDirection);
            activeWindDirection = rotatedDirection;
            activeWindSpeed += windSpeedDeltaPerIteration;
            yield return new WaitForFixedUpdate();
        }
    }

    Vector3 RotateVector(float angle, Vector3 vector)
    {
        return Quaternion.AngleAxis(angle, Vector3.up) * vector;
    }
    
}
