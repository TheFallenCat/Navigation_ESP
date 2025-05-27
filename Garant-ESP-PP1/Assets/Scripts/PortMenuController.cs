using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PortMenuController : MonoBehaviour
{
    [SerializeField] GameObject PortMenuGUI;
    [SerializeField] TextMeshProUGUI SettingText;
    Animator portMenuAnimator;

    // Start is called before the first frame update
    void Start()
    {
        portMenuAnimator = PortMenuGUI.GetComponent<Animator>();
        PortMenuGUI.SetActive(false);
    }

    public void EnablePortMenuGUI()
    {
        PortMenuGUI.SetActive(true);
        portMenuAnimator.SetTrigger("ActivatePortMenu");
    }

    public void DisablePortMenuGUI()
    {
        portMenuAnimator.SetTrigger("ClosePortMenu");
        //SetActive(false) est déclencher par un animationEvent
    }

    public void activatePort(PortAnchor port)
    {
        EnablePortMenuGUI();
    }


}
