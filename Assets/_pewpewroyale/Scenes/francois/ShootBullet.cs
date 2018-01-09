using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShootBullet : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform playerBulletSpawnPlaceHolder;
    [SerializeField] [Range(0f, 2f)] float cooldown = 0.5f;
    float buffer = 0f;

    void Start ()
    {
		
	}
	
	void Update ()
    {

        if (Input.GetButton("Fire1")) 
        {
            FireBullet();

        }
    }

    private void FireBullet()
    {
        if (buffer > cooldown)
        {
            Instantiate(bulletPrefab, playerBulletSpawnPlaceHolder.position, playerBulletSpawnPlaceHolder.rotation);
            buffer = 0f;
        }
        else buffer += Time.deltaTime;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.transform.tag)
        {
            case "bullet":
                Debug.Log("Player hit");
                break;
        }        
    }
}
