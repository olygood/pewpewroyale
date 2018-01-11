using Rewired;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour {
    public GameData currentGame;

    public int m_playerId;

    private Player m_player;

    private int focus = 0;
    public List<Button> menu;

    public void Pick(int _selection)
    {
        Debug.Log("Player " + m_playerId + "  DesiredCharacter " + _selection);

        currentGame.players[m_playerId].characterType = _selection;
    }

    private void Awake()
    {
        m_player = ReInput.players.GetPlayer(m_playerId-1);
    }

    public void Update()
    {
        if (m_player == null)
        {
            Awake(); // Too much spam?
            return;
        }

        int up, down, left, right;

        switch (focus)
        {
            case 0:
                up = 0;
                down = 2;
                left = 0;
                right = 1;
                break;
            case 1:
                up = 1;
                down = 3;
                left = 0;
                right = 1;
                break;
            case 2:
                up = 0;
                down = 2;
                left = 2;
                right = 3;
                break;
            case 3:
                up = 1;
                down = 2;
                left = 2;
                right = 3;
                break;
            default:
                up = down = left = right = 0;
                break;
        }

        if (m_player.GetButtonDown("Up"))
            focus = up;
        else if (m_player.GetButtonDown("Down"))
            focus = down;
        else if (m_player.GetButtonDown("Left"))
            focus = left;
        else if (m_player.GetButtonDown("Right"))
            focus = right;


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
}
