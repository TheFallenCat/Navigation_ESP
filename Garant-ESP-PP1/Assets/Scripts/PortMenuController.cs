using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Ink.Runtime;
using Cinemachine;

public class PortMenuController : MonoBehaviour
{
    // Menu
    [SerializeField] GameObject portMenuGUI;
    [SerializeField] TextMeshProUGUI settingText;
    [SerializeField] RawImage characterSprite;

    // Animation
    Animator portMenuAnimator;
    [SerializeField] Animator characterAnimator;
    [SerializeField] Animator mapAnimator;


    //Anchor controls
    [SerializeField] CinemachineFreeLook freeLook;
    [SerializeField] ShipController shipController;

    //Ink stories
    [SerializeField] GameObject[] actions;
    TextMeshProUGUI[] actionsText;
    private Story currentStory;

    //Global variables
    DialogueVariables dialogueVariables;
    [SerializeField] TextAsset loadGlobalsJSON;

    //First Story
    [SerializeField] PortAnchor firstPort;

    //Tag Handling
    const string CHARACTER_TAG = "character";


    // Start is called before the first frame update
    void Start()
    {
        portMenuAnimator = portMenuGUI.GetComponent<Animator>();
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
        portMenuGUI.SetActive(false);

        // Prepare action buttons according to what is supported by the UI
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

        // Start First story
        AnchorAtPort(firstPort.inkJson);
    }
    /// <summary>
    /// Opens the port menu and slides the map out of the way.
    /// </summary>
    public void EnablePortMenuGUI()
    {
        portMenuGUI.SetActive(true);
        if (mapAnimator != null)
        {
            mapAnimator.Play("SlideOut");
        }
        
        portMenuAnimator.SetTrigger("ActivatePortMenu");
    }
    /// <summary>
    /// CLoses the port menu and slides the map back in.
    /// </summary>
    public void DisablePortMenuGUI()
    {
        if (mapAnimator != null)
        {
            mapAnimator.Play("SlideIn");
        }
        portMenuAnimator.SetTrigger("ClosePortMenu");
        //SetActive(false) est déclencher par un animationEvent pour s'assurer que l'animation soit terminé avant de désactiver.
    }
    /// <summary>
    /// Starts the story associated with that port
    /// </summary>
    /// <param name="inkJson">The port story</param>
    public void activatePort(TextAsset inkJson)
    {
        currentStory = new Story(inkJson.text);
        EnablePortMenuGUI();
        settingText.text = "";

        dialogueVariables.StartListening(currentStory);

        ContinueStory();
    }
    /// <summary>
    /// Iterates throught the story until the player has to make a choice. If the story ends, closes the menu.
    /// On story end, end conditons are checked.
    /// </summary>
    void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            settingText.text += currentStory.Continue();
            settingText.text += "\n";
            HandleTags(currentStory.currentTags);
            if (currentStory.currentChoices.Count < 1)
            {
                ContinueStory();
            }
            else
            {
                DisplayActions();
            }
        }
        else
        {
            dialogueVariables.StopListening(currentStory);
            settingText.text = "";
            RaiseAnchor();
            CheckForEndCondition();
        }
    }

    /// <summary>
    /// Check for tags and proceed according to tag behavior.
    /// </summary>
    /// <param name="currentTags"></param>
    void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            Debug.Log(tagValue);
            switch (tagKey)
            {
                case CHARACTER_TAG:
                    characterAnimator.Play(tagValue);
                    break;
                // Add more case for more tags
            }
        }
    }
    /// <summary>
    /// Populate the actions in action bar with story choices.
    /// </summary>
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
    /// <summary>
    /// Called by action button to make a choice.
    /// </summary>
    /// <param name="actionIndex"></param>
    public void DoAction(int actionIndex)
    {
        Debug.Log(actionIndex);
        currentStory.ChooseChoiceIndex(actionIndex);
        settingText.text = "";
        ContinueStory();
    }

    /// <summary>
    /// Read from story global variables.
    /// </summary>
    /// <param name="variableName"></param>
    /// <returns></returns>
    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if (variableValue == null)
        {
            Debug.LogWarning("Ink Variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    /// <summary>
    /// Check if any end condition is met, then plays associated endScene
    /// </summary>
    void CheckForEndCondition()
    {
        if (((Ink.Runtime.BoolValue)GetVariableState("endingWorldTraveler")).value)
        {
            GetComponent<Controller>().EndGame(2);
        }
        else if (((Ink.Runtime.BoolValue)GetVariableState("endingSirenPlan")).value)
        {
            GetComponent<Controller>().EndGame(3);
        }
        else if (((Ink.Runtime.BoolValue)GetVariableState("endingDoomsdayRitual")).value)
        {
            GetComponent<Controller>().EndGame(4);
        }
    }

    private void LockCameraControl() => freeLook.enabled = false;
    private void UnlockCameraControl() => freeLook.enabled = true;

    /// <summary>
    /// Stop ship controls and activate port story
    /// </summary>
    /// <param name="inkJson"></param>
    public void AnchorAtPort(TextAsset inkJson)
    {
        shipController.isAnchored = true;
        LockCameraControl();
        activatePort(inkJson);
        shipController.tryAnchor = false;
        shipController.SwitchAnchor();
    }

    /// <summary>
    /// Reenable ship controls and stops port story
    /// </summary>
    public void RaiseAnchor()
    {
        shipController.isAnchored = false;
        UnlockCameraControl();
        DisablePortMenuGUI();
        shipController.SwitchAnchor();
    }
    private void Update()
    {
        // Anchor controls
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
    /// <summary>
    /// Signals that the player wants to anchor.
    /// </summary>
    /// <returns></returns>
    IEnumerator TryAnchor()
    {
        shipController.tryAnchor = true;
        yield return new WaitForFixedUpdate();
        shipController.tryAnchor = false;
    }

}
