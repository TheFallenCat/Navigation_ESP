using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class FogFreeCamera : MonoBehaviour
{
    [SerializeField] bool enableFog = false;

    bool previousFogState;

    void OnPreRender()
    {
        previousFogState = RenderSettings.fog;
        RenderSettings.fog = enableFog;
    }

    void OnPostRender()
    {
        RenderSettings.fog = previousFogState;
    }
}
