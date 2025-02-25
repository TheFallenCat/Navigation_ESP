using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Controller : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook freeLook;
    [SerializeField] PortMenuController portMenuController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LockCameraControl() => freeLook.enabled = false;
    private void UnlockCameraControl() => freeLook.enabled = true;

    public void AnchorAtPort(int portindex)
    {
        LockCameraControl();
        portMenuController.activatePort(portindex);
    }

    public void SetSail()
    {
        UnlockCameraControl();
        portMenuController.DisablePortMenuGUI();
    }
}
