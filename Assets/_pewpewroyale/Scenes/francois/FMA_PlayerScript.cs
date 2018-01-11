﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMA_PlayerScript : MonoBehaviour
{
    private bool m_debug = true;

    private Transform m_transform;
    public Vector3 Position
    {
        get { return m_transform.position; }
    }
    private FMA_PlayerWeapons m_weapons;

    public GameObject weaponPlaceHolder;
    private FMA_WeaponSettings weaponsSettings;

    [SerializeField]
    private int m_playerID = -1;
    public int PlayerID
    {
        get { return m_playerID; }
        set { m_playerID = value; }
    }
    
    void Start()
    {
        weaponsSettings = FMA_WeaponSettings.Instance;
        if (m_playerID == -1) Debug.LogError("Player ID is not defined!");
        if (weaponPlaceHolder == null) Debug.LogError("No Weapon place holder defined for player #" + ((m_playerID == -1) ? "?": m_playerID.ToString()));
        m_transform = GetComponent<Transform>();
        m_weapons = new FMA_PlayerWeapons(weaponPlaceHolder, weaponsSettings, this);
    }

    void Update()
    {
        /*
        if (Input.GetButtonDown("Fire1")) m_weapons.Fire();
        if (Input.GetButtonDown("Fire2"))
        {
            switch (m_weapons.Weapon)
            {
                case FMA_WeaponSettings.WeaponType.LASER:
                    m_weapons.Weapon = FMA_WeaponSettings.WeaponType.BOLTER;
                    break;
                case FMA_WeaponSettings.WeaponType.BOLTER:
                    m_weapons.Weapon = FMA_WeaponSettings.WeaponType.LASER;
                    break;
            }
        }
        //*/
    }

    public void Fire()
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Fire with " + m_weapons);
        m_weapons.Fire();
    }

    public void ChangeWeapon(FMA_WeaponSettings.WeaponType weapon)
    {
        m_weapons.Weapon = weapon;
    }

    public void Move(Vector3 movement)
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Move");
    }

    public void Rotate(Vector3 rotation)
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Rotate");
    }

    public void PlayerGetHit(FMA_WeaponSettings.WeaponType weapon, FMA_PlayerScript origin)
    {
        if (m_debug) Debug.Log(string.Format("Player #{0} dealt {1} damages by {2} from player #{3}", m_playerID, weaponsSettings.GetDamage(weapon), weapon, origin.PlayerID));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "bullet":
                FMA_BulletScript bullet = collision.gameObject.GetComponent<FMA_BulletScript>();
                FMA_PlayerScript player = bullet.OriginPlayer.GetComponent<FMA_PlayerScript>();
                PlayerGetHit(FMA_WeaponSettings.WeaponType.BOLTER, player);
                break;
        }
    }
}