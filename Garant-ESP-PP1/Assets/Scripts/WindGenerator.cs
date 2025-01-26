using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WindGenerator : MonoBehaviour
{
    [SerializeField] Vector3[] windTypes;
    [SerializeField] Vector3 activeWind;

    [SerializeField] int windSwitchTimer = 300;
    [SerializeField] int windSwitchDuration = 10;
    public bool onHighSea;

    [SerializeField] Material ocean;

    [SerializeField] float angleTest;

    // Start is called before the first frame update
    void Start()
    {
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
                Vector3 newWindType = windTypes[Random.Range(0, windTypes.Length)];
                StartCoroutine(WindTransitionTo(new Vector3(newWindType.x, 0, newWindType.y), newWindType.z));
            }
            yield return new WaitForSeconds(windSwitchTimer);
        }
    }
    /// <summary>
    /// Co-routine to seemlesly transition from one wind type to another.
    /// </summary>
    /// <param name="windDirection">The direction of the wind to switch to.</param>
    /// <param name="windSpeed"></param>
    public IEnumerator WindTransitionTo(Vector3 windDirection, float windSpeed)
    {
        float angle = angleTest = Vector3.Angle(windDirection, new Vector3(activeWind.x, 0, activeWind.y));
        float windSpeedDelta;
        int iterations = (int)(1 / Time.fixedDeltaTime) * windSwitchDuration;
        float anglePerIteration = angle / iterations;
        Vector3 rotatedDirection = windDirection;
        for (int i = 0; i < iterations; i++)
        {
            rotatedDirection = RotateVector(anglePerIteration, rotatedDirection);
            activeWind = new Vector3(rotatedDirection.x, rotatedDirection.z, windSpeed);
            SetPrimaryWave();
            yield return new WaitForFixedUpdate();
        }
    }

    void SetPrimaryWave()
    {
        
        Vector4 primaryWave = new Vector4(activeWind.x, activeWind.y, 0.1f * activeWind.z, 50);
        ocean.SetVector("_PrimaryWave", primaryWave);
    }

    Vector3 RotateVector(float angle, Vector3 vector)
    {
        return Quaternion.AngleAxis(angle, Vector3.up) * vector;
    }
}
