using System.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public class GameInit : DualBehaviour
{
    #region Public Members

    public GameData m_gameData;
    public LevelData m_levelData;

    public GameObject m_playerPrefab;

    /// <summary>
    ///  List of players who have selected a character during CharacterSelection.
    /// </summary>
    public List<PlayerData> players;
    
    #endregion

    #region Public void

    #endregion

    #region System

    protected override void Awake()
    {
        LoadJSON();

        StartGame();
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

    private void StartGame()
    {
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        players = (from player in m_gameData.players where player.characterType != -1 select player).ToList();

        foreach (PlayerData player in players)
            SpawnPlayer(player);
    }

    private void SpawnPlayer(PlayerData _player)
    {
        Debug.Log("Spawning player: " + _player.id);

        GameObject player = GameObject.Instantiate(m_playerPrefab);

        player.GetComponent<PlayerInputManager>().m_playerId = _player.id;

        player.transform.position = new Vector3(
            m_levelData.spawnPoints[_player.id].x,
            m_levelData.spawnPoints[_player.id].y,
            0
        );

        // TODO: [Fix] Displays as "type mismatch"... Figure out why and if it's problematic
        // TODO: Move in its own method?
        m_gameData.players[_player.id].instance = player;

        UpdateColor(_player);
    }

    private void UpdateColor(PlayerData _player)
    {
        _player.color = m_gameData.playersColor[_player.characterType];
    }

    #endregion

    #region Tools Debug and Utility

    #endregion

    #region Private and Protected Members

    #endregion
}
