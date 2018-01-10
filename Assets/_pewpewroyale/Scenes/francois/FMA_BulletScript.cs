using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour {

    private Rigidbody2D m_body;

    [SerializeField]
    [Range(1f, 50f)]
    float speed = 10f;

    float lifetime = 2.5f;

    void Awake()
    {
        m_body = GetComponent<Rigidbody2D>();
    }

    public void SetVelocity(float x, float y)
    {
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
