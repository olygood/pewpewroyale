using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {

    public GameData currentGame;

    public int m_playerId;
	
	public void Pick(int _selection)
    {
        Debug.Log("Player " + m_playerId + "  DesiredCharacter " + _selection);

        currentGame.players[m_playerId].characterType = _selection;

    }
}
