using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] WindGenerator windGenerator;
    [SerializeField] Material ocean;

    // Struct to make sense of the very unintuitive 4 dimensional vector used in the shader
    public struct GerstnerWave
    {
        public Vector2 direction; // Wave direction (x, y)
        public float steepness;   // Steepness (z)
        public float wavelength;  // Wavelength (w)
    }

    GerstnerWave primaryWave;
    GerstnerWave secondaryWave;
    GerstnerWave choppyWave;
    GerstnerWave turbulenceWave;

    private const float GRAVITY = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        primaryWave = new GerstnerWave
        {
            direction = new Vector2(1f, 0),
            steepness = 0.2f,
            wavelength = 50
        };
        secondaryWave = new GerstnerWave
        {
            direction = new Vector2(0.7f, 0.7f),
            steepness = 0.1f,
            wavelength = 30
        };
        choppyWave = new GerstnerWave
        {
            direction = new Vector2(0.5f, -0.5f),
            steepness = 0.05f,
            wavelength = 15
        };
        turbulenceWave = new GerstnerWave
        {
            direction = new Vector2(-1, 0.3f),
            steepness = 0.03f,
            wavelength = 10
        };
    }

    // Update is called once per frame
    void Update()
    {
        SetPrimaryWave(windGenerator.activeWindDirection, windGenerator.activeWindSpeed);
    }
    /// <summary>
    /// Modify Ocean Material to correspond with active wind
    /// </summary>
    /// <param name="activeWindDirection"></param>
    /// <param name="activeWindSpeed"></param>
    void SetPrimaryWave(Vector3 activeWindDirection, float activeWindSpeed)
    {

        Vector4 newWave = new Vector4(activeWindDirection.x, activeWindDirection.z, 0.05f * activeWindSpeed, 50);
        ocean.SetVector("_PrimaryWave", newWave);
        primaryWave.direction = new Vector2(newWave.x, newWave.y);
        primaryWave.steepness = newWave.z;
        
    }
    /// <summary>
    /// Recreate Shader Calculation
    /// </summary>
    /// <param name="position"></param>
    /// <param name="wave"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    private Vector3 CalculateGerstnerWave(Vector3 position, GerstnerWave wave, float time)
    {
        // Wave parameters
        float k = 2 * Mathf.PI / wave.wavelength;    // Wave number
        float c = Mathf.Sqrt(GRAVITY / k);           // Wave speed
        Vector2 d = wave.direction.normalized;       // Normalized direction
        float a = wave.steepness / k;                // Amplitude

        // Wave function
        float f = k * (Vector2.Dot(d, new Vector2(position.x, position.z)) - c * time * 0.7f);

        // Apply the function
        float dx = d.x * (a * Mathf.Cos(f));
        float dz = d.y * (a * Mathf.Cos(f));
        float dy = a * Mathf.Sin(f);

        return new Vector3(dx, dy, dz);
    }

    public float GetWaveHeightAtPosition(Vector3 position, float time)
    {
        Vector3 p = Vector3.zero;
        Vector3 seaLevelPosition = new Vector3(position.x, this.transform.position.y, position.z);

        // Add contributions from all waves
        p += CalculateGerstnerWave(seaLevelPosition, primaryWave, time);
        p += CalculateGerstnerWave(seaLevelPosition, secondaryWave, time);
        p += CalculateGerstnerWave(seaLevelPosition, choppyWave, time);
        p += CalculateGerstnerWave(seaLevelPosition, turbulenceWave, time);

        // Return the water height
        return seaLevelPosition.y + p.y;
    }


}
