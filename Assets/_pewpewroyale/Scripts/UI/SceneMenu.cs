using Rewired;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneMenu : MonoBehaviour
{
    private Player m_player;

    private int focus = 0;
    public List<Button> menu;

    private void Awake()
    {
        m_player = ReInput.players.GetPlayer(0);
    }

    private void Update()
    {
        if (m_player.GetButtonDown("Up"))
            focus--;
        else if (m_player.GetButtonDown("Down"))
            focus++;

        Debug.Log(focus);

        if (focus > menu.Count - 1)
            focus = menu.Count - 1;
        else if (focus < 0)
            focus = 0;

        Debug.Log(focus);

        menu[focus].Select();

        if (m_player.GetButtonDown("Submit"))
            menu[focus].onClick.Invoke();
    }

    public void NewGame()
    {
        SceneManager.LoadSceneAsync("ChosePlayerMenu");
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
