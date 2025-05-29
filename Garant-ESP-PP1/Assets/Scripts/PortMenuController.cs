using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using Cinemachine;

public class PortMenuController : MonoBehaviour
{
    [SerializeField] GameObject portMenuGUI;
    [SerializeField] TextMeshProUGUI settingText;
    [SerializeField] RawImage characterSprite;
    Animator portMenuAnimator;
    [SerializeField] Animator characterAnimator;
    [SerializeField] Animator mapAnimator;


    //Anchor controls
    [SerializeField] CinemachineFreeLook freeLook;
    [SerializeField] ShipController shipController;

    [SerializeField] GameObject[] actions;
    TextMeshProUGUI[] actionsText;

    private Story currentStory;

    DialogueVariables dialogueVariables;
    [SerializeField] TextAsset loadGlobalsJSON;
    //[SerializeField] InkFile globalsInkFile;
    [SerializeField] PortAnchor firstPort;

    


    const string CHARACTER_TAG = "character";


    // Start is called before the first frame update
    void Start()
    {
        portMenuAnimator = portMenuGUI.GetComponent<Animator>();
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        portMenuGUI.SetActive(false);

        actionsText = new TextMeshProUGUI[actions.Length];
        for (int i = 0; i < actions.Length; i++)
        {
            TextMeshProUGUI[] childrenTexts = actions[i].GetComponentsInChildren<TextMeshProUGUI>();
            foreach(TextMeshProUGUI childrenText in childrenTexts)
            {
                if (!childrenText.gameObject.CompareTag("btnText"))
                    actionsText[i] = childrenText;
            }   
        }
        AnchorAtPort(firstPort.inkJson);
    }

    public void EnablePortMenuGUI()
    {
        portMenuGUI.SetActive(true);
        if (mapAnimator != null)
        {
            mapAnimator.Play("SlideOut");
        }
        
        portMenuAnimator.SetTrigger("ActivatePortMenu");
    }

    public void DisablePortMenuGUI()
    {
        if (mapAnimator != null)
        {
            mapAnimator.Play("SlideIn");
        }
        portMenuAnimator.SetTrigger("ClosePortMenu");
        //SetActive(false) est déclencher par un animationEvent
    }

    public void activatePort(TextAsset inkJson)
    {
        currentStory = new Story(inkJson.text);
        EnablePortMenuGUI();
        settingText.text = "";

        dialogueVariables.StartListening(currentStory);

        ContinueStory();
    }

    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            settingText.text += currentStory.Continue();
            settingText.text += "\n";

            if (currentStory.currentChoices.Count < 1)
            {
                ContinueStory();
            }
            else
            {
                DisplayActions();
            }

            HandleTags(currentStory.currentTags);
        }
        else
        {
            dialogueVariables.StopListening(currentStory);
            settingText.text = "";
            RaiseAnchor();
        }
    }

    void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch (tagKey)
            {
                case CHARACTER_TAG:
                    characterAnimator.Play(tagValue);
                    break;
                // Add more case for more tags
            }
        }

        
    }

    void DisplayActions()
    {
        List<Choice> currentActions = currentStory.currentChoices;
        
        for (int i = 0; i < currentActions.Count; i++)
        {
            actions[i].gameObject.SetActive(true);
            actionsText[i].text = currentActions[i].text;
        }

        for (int i = currentActions.Count; i < actions.Length; i++)
        {
            actions[i].gameObject.SetActive(false);
        }
    }

    public void DoAction(int actionIndex)
    {
        Debug.Log(actionIndex);
        currentStory.ChooseChoiceIndex(actionIndex);
        settingText.text = "";
        ContinueStory();
    }

    private void LockCameraControl() => freeLook.enabled = false;
    private void UnlockCameraControl() => freeLook.enabled = true;

    public void AnchorAtPort(TextAsset inkJson)
    {
        shipController.isAnchored = true;
        LockCameraControl();
        activatePort(inkJson);
        shipController.tryAnchor = false;
        shipController.SwitchAnchor();
    }

    public void SetSail()
    {
        UnlockCameraControl();
        DisablePortMenuGUI();
    }

    //Anchor 

    private void Update()
    {
        if (shipController != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && !shipController.isAnchored)
            {
                StartCoroutine(TryAnchor());
            }

            if (shipController.portInRange != null && shipController.tryAnchor)
            {
                AnchorAtPort(shipController.portInRange.inkJson);
            }
        }

    }

    public void RaiseAnchor()
    {
        shipController.isAnchored = false;
        SetSail();
        shipController.SwitchAnchor();
    }

    IEnumerator TryAnchor()
    {
        shipController.tryAnchor = true;
        yield return new WaitForFixedUpdate();
        shipController.tryAnchor = false;
    }


}
