﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using System;

[AddComponentMenu("")]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerInputManager : MonoBehaviour
{
    private bool m_initialized;

    public int m_playerId; // The Rewired player id of this character
    public Move m_move;
    public ShootBullet m_shoot;

    private Player m_player; // The Rewired Player
    private Vector3 m_moveVector;
    private Vector3 m_rotateVector;
    private bool m_shotfired;
    private bool m_changeWeapon;

    public FMA_PlayerScript m_play;
    private FMA_WeaponSettings m_weapon;

    //[System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private void Awake()
    {
        m_initialized = false;
    }
    

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        m_player = ReInput.players.GetPlayer(m_playerId);
        Debug.Log(m_player.id);

        m_initialized = true;
    }

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!m_initialized) Initialize(); // Reinitialize after a recompile in the editor
        GetInput();
        m_move.ProcessMovement(m_moveVector);
        m_move.ProcessRotation(m_rotateVector);
        if (m_shotfired == true)
        {
            m_play.Fire();
            //m_shoot.FireBullet();
            //m_shotfired = false;
        }
        else
        {
            m_shotfired = false;
        }
        if (m_changeWeapon == true)
        {
            m_play.ChangeWeapon();
            /*
            switch (m_play.Weapon)
            {
                case FMA_WeaponSettings.WeaponType.BOLTER:
                    m_play.ChangeWeapon(FMA_WeaponSettings.WeaponType.LASER);
                    break;
                case FMA_WeaponSettings.WeaponType.LASER:
                    m_play.ChangeWeapon(FMA_WeaponSettings.WeaponType.BOLTER);
                    break;
                default:
                    break;
            }
             //            m_play.ChangeWeapon(FMA_WeaponSettings.WeaponType.);
             */
            Debug.Log("Changed weapon");
        }
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        m_moveVector.x = m_player.GetAxis("MoveHorizontal"); // get input by name or action id
        m_moveVector.y = m_player.GetAxis("MoveVertical");
        m_rotateVector.x = m_player.GetAxis("RotateHorizontal");
        m_rotateVector.y = m_player.GetAxis("RotateVertical");
        m_shotfired = m_player.GetButton("Shoot");
        m_changeWeapon = m_player.GetButton("ChangeWeapon");
        if(m_playerId == 1)
        {
            Debug.Log(m_moveVector);
        }
    }
}
