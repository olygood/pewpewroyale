using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    /*public bool detect_number_of_players_automatically = true;

    [Range(2, 4)]
    public int number_of_players = 3;*/
    
    public string gameSceneName;

    public string gameData_path;

    public List<Color> playersColor = new List<Color>()
    {
        Color.yellow,
        Color.green,
        Color.blue,
        Color.red
    };

    public List<PlayerData> players = new List<PlayerData>();
}
