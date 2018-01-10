using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManagement : MonoBehaviour {

    private GameObject player;
    public LevelData m_levelData;
    public InputManager m_inputManager = new InputManager();

  
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            Respawn();
        }
    }

    private void Respawn()
    {
            int id = m_inputManager.m_playerId;
            Vector2 vect = m_levelData.spawnPoints[id];
            transform.position = vect;
    }

}

