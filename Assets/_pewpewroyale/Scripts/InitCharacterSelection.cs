using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCharacterSelection : MonoBehaviour {

    public GameData m_gameData;

    void Awake()
    {
        foreach (PlayerData player in m_gameData.players)
            player.characterType = -1;
    }
}
