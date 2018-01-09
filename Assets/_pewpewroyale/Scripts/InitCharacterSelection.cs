using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitCharacterSelection : MonoBehaviour {

    public GameData m_gameData;

    void Awake()
    {
        foreach (PlayerData player in m_gameData.players)
            player.characterType = -1;
    }

    public void StartGame()
    {
        Debug.Log("Starting game! (Loading scene: " + m_gameData.gameSceneName + ")");

        foreach(PlayerData player in m_gameData.players)
            File.WriteAllText(m_gameData.gameData_path + player.name + ".json", JsonUtility.ToJson(player));

        SceneManager.LoadSceneAsync(m_gameData.gameSceneName);
    }
}
