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

    public GameObject creditPanel;
    public GameObject optionPanel;
    public GameObject mainMenu;

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

        if(m_player.GetButtonDown("Start"))
            creditPanel.SetActive(false);
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
        // Priorité 4
        Debug.LogWarning("Options WIP");
        optionPanel.SetActive(true);
        mainMenu.SetActive(false);
        
    }

    public void Credit()
    {
        creditPanel.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        optionPanel.SetActive(false);
        creditPanel.SetActive(false);
    }
	
}
