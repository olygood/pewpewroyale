﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Score : MonoBehaviour
{
    public int m_maxHealth = 100;
    private int m_health;
    public Image m_healthBar;
    
    public LevelData m_levelData;

    [SerializeField]
    private FMA_PlayerScript m_player;

    private void Awake()
    {
        m_health = m_maxHealth;
      
    }
    /*
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            TakeDamage(10);
        }
    }
    */
    public void TakeDamage(int amount)
    {
        m_health -= amount;

        m_healthBar.fillAmount = (float) m_health/m_maxHealth;

        if (m_health <= 0)
        {
            Respawn();
        }
    }
  
    public void Respawn()
    {
        int id = m_player.PlayerID;
        Vector2 vect = m_levelData.spawnPoints[id];
        transform.position = vect;
        m_health = m_maxHealth;
        m_healthBar.fillAmount = 1;
    }
}
