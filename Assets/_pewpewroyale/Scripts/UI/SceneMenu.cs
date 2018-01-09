using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadSceneAsync("jeromej");
    }

    public void ExitGame()
    {
        Debug.Log("Quitting");

        Application.Quit();

        Debug.Break();
    }

    public void Options()
    {
        // Priorité 3
        Debug.LogWarning("Options WIP");
    }

    public void Credit()
    {
        // Priorité 4

        Debug.LogWarning("Credit WIP");
    }
	
}
