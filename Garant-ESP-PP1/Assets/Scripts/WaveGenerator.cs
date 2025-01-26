using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{
    [SerializeField] WindGenerator windController;
    [SerializeField] Material ocean;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (windController.onHighSea)
        {
            SetPrimaryWave(windController.activeWindDirection, windController.activeWindSpeed);
        }
    }

    void SetPrimaryWave(Vector3 activeWindDirection, float activeWindSpeed)
    {

        Vector4 primaryWave = new Vector4(activeWindDirection.x, activeWindDirection.z, 0.05f * activeWindSpeed, 50);
        ocean.SetVector("_PrimaryWave", primaryWave);
    }
}
