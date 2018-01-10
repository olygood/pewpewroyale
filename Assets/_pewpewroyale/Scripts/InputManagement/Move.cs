using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;

[AddComponentMenu("")]
[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    public float m_moveSpeed = 3.0f;
    public float m_rotationSpeed = 20f;

    private Rigidbody2D m_rigidbody;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    public void ProcessMovement(Vector3 _moveVector)
    {
        // Process movement
        Debug.Log("process move");
        if (_moveVector.x > 0.1f || _moveVector.y > 0.1f || _moveVector.x < -0.1f || _moveVector.y < -0.1f)
        {
            Debug.Log("on move");
            Vector2 movement = new Vector2(_moveVector.x, _moveVector.y);
            m_rigidbody.velocity = movement * m_moveSpeed;
        }
    }

    public void ProcessRotation(Vector3 _rotateVector)
    {
        // Process rotation
        if (_rotateVector.x > 0.1f || _rotateVector.y > 0.1f || _rotateVector.x < -0.1f || _rotateVector.y < -0.1f)
        {
            float playerAngle = Vector2.SignedAngle(Vector2.right, new Vector2(_rotateVector.x, _rotateVector.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, playerAngle), m_rotationSpeed * Time.deltaTime);
        }
    }
}

	

