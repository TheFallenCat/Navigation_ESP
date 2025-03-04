using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortAnchor : MonoBehaviour
{

    //To parse from JSON file later
    [System.Serializable]
    struct PortStatus
    {
        public int statusIndex;
        public string settingDescription;
        public List<PortAction> portActions;
    }

    [System.Serializable]
    struct PortAction
    {
        public string actionName;
        public int actionEffect;
    }

    public int portIndex;
    public int portProgressIndex;

    [SerializeField] List<PortStatus> PortData;
    
    public string getSettingDescription()
    {
        return "";
    }
}
