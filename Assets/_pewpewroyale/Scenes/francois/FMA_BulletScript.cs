﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FMA_BulletScript : MonoBehaviour {

    private Rigidbody2D m_body;
    private float lifetime = 5f;

    void Awake()
    {
        m_body = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(float x, float y, float speed)
    {
        Debug.Log("SetVelocity");
        m_body.velocity = new Vector2(x * speed, y * speed);
    }

    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0) { Destroy(gameObject); }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
