using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour {

    private Rigidbody2D m_body;

    [SerializeField]
    [Range(1f, 20f)]
    float speed = 10f;

    float lifetime = 2.5f;

    void Start()
    {
        if (!m_body) m_body = gameObject.GetComponent<Rigidbody2D>();
        m_body.velocity = new Vector3(speed, 0f, 0f);
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
