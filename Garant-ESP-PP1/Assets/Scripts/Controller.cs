using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Controller : MonoBehaviour
{
    // Didn't have time to include Save and Load features, so the controller doesn't have much.
    public void EndGame(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

}
