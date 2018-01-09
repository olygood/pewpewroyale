using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;

[AddComponentMenu("")]
[RequireComponent(typeof(Rigidbody2D))]
public class Move : MonoBehaviour
{
    public int m_playerId = 0; // The Rewired player id of this character

    public float m_moveSpeed = 3.0f;
    public float m_bulletSpeed = 15.0f;
    public float m_rotationSpeed = 20f;

    private Player m_player; // The Rewired Player
    private Vector3 m_moveVector;
    private Vector3 m_rotateVector;
    private Rigidbody2D m_rigidbody;

    [System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private bool m_initialized = false;

    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        m_player = ReInput.players.GetPlayer(m_playerId);

        m_initialized = true;
    }

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!m_initialized) Initialize(); // Reinitialize after a recompile in the editor
        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        m_moveVector.x = m_player.GetAxis("MoveHorizontal"); // get input by name or action id
        m_moveVector.y = m_player.GetAxis("MoveVertical");
        m_rotateVector.x = m_player.GetAxis("RotateHorizontal");
        m_rotateVector.y = m_player.GetAxis("RotateVertical");
    }

    private void ProcessInput()
    {
        // Process movement
        if (m_moveVector.x > 0.1f || m_moveVector.y > 0.1f || m_moveVector.x < -0.1f || m_moveVector.y < -0.1f)
        {
            Vector2 movement = new Vector2(m_moveVector.x, m_moveVector.y);
            m_rigidbody.velocity = movement * m_moveSpeed;
        }

        // Process rotation
        if (m_rotateVector.x > 0.1f || m_rotateVector.y > 0.1f || m_rotateVector.x < -0.1f || m_rotateVector.y < -0.1f)
        {
            float playerAngle = Vector2.SignedAngle(Vector2.right, new Vector2(m_rotateVector.x, m_rotateVector.y));
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, playerAngle), m_rotationSpeed * Time.deltaTime);
        }
    }

    private void OnGUI()
    {
        GUILayout.Button(m_rotateVector.x + " - " + m_rotateVector.y);
    }
}

	

