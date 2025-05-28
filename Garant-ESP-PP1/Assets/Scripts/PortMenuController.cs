using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using Ink.UnityIntegration;
using Cinemachine;

public class PortMenuController : MonoBehaviour
{
    [SerializeField] GameObject PortMenuGUI;
    [SerializeField] TextMeshProUGUI settingText;
    [SerializeField] RawImage CharacterSprite;
    Animator portMenuAnimator;
    [SerializeField ]Animator characterAnimator;
    [SerializeField] CinemachineFreeLook freeLook;

    [SerializeField] GameObject[] actions;
    TextMeshProUGUI[] actionsText;

    private Story currentStory;
    private bool dialogueIsPlaying;
    DialogueVariables dialogueVariables;
    [SerializeField] InkFile globalsInkFile;

    const string CHARACTER_TAG = "character";


    // Start is called before the first frame update
    void Start()
    {
        portMenuAnimator = PortMenuGUI.GetComponent<Animator>();
        dialogueVariables = new DialogueVariables(globalsInkFile.filePath);
        PortMenuGUI.SetActive(false);
        dialogueIsPlaying = false;

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

    public void activatePort(TextAsset inkJson)
    {
        currentStory = new Story(inkJson.text);
        dialogueIsPlaying = true;
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
            dialogueIsPlaying = false;
            settingText.text = "";
            SetSail();
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

    private void Update()
    {
        if (!dialogueIsPlaying)
        {
            return;
        }
    }

    private void LockCameraControl() => freeLook.enabled = false;
    private void UnlockCameraControl() => freeLook.enabled = true;

    public void AnchorAtPort(TextAsset inkJson)
    {
        LockCameraControl();
        activatePort(inkJson);
    }

    public void SetSail()
    {
        UnlockCameraControl();
        DisablePortMenuGUI();
    }
}
