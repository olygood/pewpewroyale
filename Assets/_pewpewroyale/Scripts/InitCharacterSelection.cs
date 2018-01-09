using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

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

        SaveToJSON();

        SceneManager.LoadSceneAsync(m_gameData.gameSceneName);
    }

    private void SaveToJSON()
    {
        foreach (PlayerData player in m_gameData.players)
            File.WriteAllText(m_gameData.gameData_path + player.name + ".json", JsonUtility.ToJson(player));

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }
}
