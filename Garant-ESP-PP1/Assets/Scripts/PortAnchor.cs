using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortAnchor : MonoBehaviour
{

    //To parse from JSON file later
    [System.Serializable]
    struct portStatus
    {
        public int statusIndex;
        public string settingDescription;
        public List<portAction> portActions;
    }

    [System.Serializable]
    struct portAction
    {
        public string actionName;
        public int actionEffect;
    }

    public int portIndex;
    public int portProgressIndex;

    [SerializeField] List<portStatus> PortData;
    
    public string getSettingDescription()
    {
        return "";
    }
}
