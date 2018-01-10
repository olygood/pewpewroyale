using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathManagement : MonoBehaviour {

    private GameObject player;
    public LevelData m_levelData;
    public Move m_move;

    private void Awake()
    {
        m_move = new Move();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            int id = m_move.m_playerId;
            Vector2 vect = m_levelData.spawnPoints[id];
            transform.position = vect;
        }
    }
}

