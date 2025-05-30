using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{

    public void EndGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }


    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

}
