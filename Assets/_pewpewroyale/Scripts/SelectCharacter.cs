using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour {

    public GameData currentGame;

    public int m_playerId;
	
	public void Pick(int _selection)
    {
        Debug.Log(m_playerId + " " + _selection);
    }
}
