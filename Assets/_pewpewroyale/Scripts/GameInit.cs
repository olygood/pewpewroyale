using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameInit : DualBehaviour
{
    #region Public Members

    public GameData m_gameData;
    
    #endregion

    #region Public void

    #endregion

    #region System

    protected override void Awake()
    {
        LoadJSON();
    }

    private void Update()
    {
        
    }

    #endregion

    #region Class Methods

    private void LoadJSON()
    {
        for (int i = 0; i < m_gameData.players.Count; i++)
        {
            PlayerData player = m_gameData.players[i];

            string target = m_gameData.gameData_path + player.name + ".json";

            if(File.Exists(target))
                JsonUtility.FromJsonOverwrite(File.ReadAllText(target), m_gameData.players[i]);
        }
    }

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #endregion
}
