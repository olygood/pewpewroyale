using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Score : MonoBehaviour
{
    public int health = 100;
    public Image healthBar;

    private GameObject player;
    public LevelData m_levelData;
    public PlayerInputManager m_inputManager = new PlayerInputManager();

  

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "bullet")
        {
            TakeDamage(10);
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;

        healthBar.fillAmount = health;

        if (health <= 0)
        {
            Respawn();
        }
    }
  
    public void Respawn()
    {
        int id = m_inputManager.m_playerId;
        Vector2 vect = m_levelData.spawnPoints[id];
        transform.position = vect;
    }
}
