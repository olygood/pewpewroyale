using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar_Score : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public Image healthBar;

    private GameObject player;
    public LevelData m_levelData;
    public PlayerInputManager m_inputManager = new PlayerInputManager();

    private void Awake()
    {
        health = maxHealth;
    }

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

        healthBar.fillAmount = (float) health/maxHealth;

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
        health = maxHealth;
        healthBar.fillAmount = 1;
    }
}
