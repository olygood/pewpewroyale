using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameData : ScriptableObject {

    public bool detect_controllers_automatically = true;

    [Range(2, 4)]
    public int number_of_players = 3;

    public List<PlayerData> players = new List<PlayerData>();
}
