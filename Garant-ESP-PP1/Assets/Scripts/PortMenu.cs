using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortMenu : MonoBehaviour
{
    bool isOpening = false;
  
    //
    // These functions are called using animation events. They are to ensure the proper opening and closing of the menu by the PortMenuController.
    //

    void OpenThis()
    {
        isOpening = true;
    }

    void CloseThis()
    {
        isOpening = false;
    }

    void DisableThis()
    {
        if (!isOpening)
            this.gameObject.SetActive(false);
    }

}
