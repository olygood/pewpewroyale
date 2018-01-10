using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerScript : MonoBehaviour
{
    private bool m_debug = true;

    private Transform m_transform;

    private int m_playerID;
    public int ID
    {
        get { return m_playerID; }
        set { m_playerID = value; }
    }

    private void Awake()
    {

    }

    void Start()
    {
        m_transform = GetComponent<Transform>();
        InitWeapon();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Fire();
        if (Input.GetButtonDown("Fire2"))
        {
            switch(Weapon)
            {
                case E_weapon.LASER:
                    Weapon = E_weapon.BOLTER;
                    break;
                case E_weapon.BOLTER:
                    Weapon = E_weapon.LASER;
                    break;
            }
        }
    }

    public void Move(Vector3 movement)
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Move");
    }

    public void Rotate(Vector3 rotation)
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Rotate");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "bullet":
                Debug.Log("Player hit");
                break;
        }
    }

    #region weapon
    [Header("WEAPONS")]
    private E_weapon m_currentWeapon;
    [SerializeField]
    public E_weapon Weapon
    {
        get { return m_currentWeapon; }
        set
        {
            m_currentWeapon = value;
            ChangeWeapon();
        }
    }
    public enum E_weapon
    {
        BOLTER,
        LASER
    }

    [SerializeField]
    public GameObject m_weaponPlaceHolder;

    private Transform m_weaponPlaceHolderTransform;
    private SpriteRenderer m_weaponPlaceHolderSpriteRenderer;
    private AudioSource m_weaponPlaceHolderAudioSource;
    
    private void InitWeapon()
    {
        m_weaponPlaceHolderTransform = m_weaponPlaceHolder.GetComponent<Transform>();
        if (m_debug && !m_weaponPlaceHolderTransform) Debug.LogError("Can't find Transform on WeaponPlaceHolder for player #" + m_playerID);
        m_weaponPlaceHolderSpriteRenderer = m_weaponPlaceHolder.GetComponent<SpriteRenderer>();
        if (m_debug && !m_weaponPlaceHolderSpriteRenderer) Debug.LogError("Can't find SpriteRenderer on WeaponPlaceHolder for player #" + m_playerID);
        m_weaponPlaceHolderAudioSource = m_weaponPlaceHolder.GetComponent<AudioSource>();
        if (m_debug && !m_weaponPlaceHolderAudioSource) Debug.LogError("Can't find AudioSource on WeaponPlaceHolder for player #" + m_playerID);

        InitLaserLineRenderer();

        m_currentWeapon = E_weapon.BOLTER;
        ChangeWeapon();
    }
    public void Fire()
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Fire");
        switch (m_currentWeapon)
        {
            case E_weapon.BOLTER:
                FireBullet();
                break;
            case E_weapon.LASER:
                GameObject collide = FireLaser();
                if (collide != null)
                {
                    if (collide.tag == "Player")
                    {
                        Debug.Log("Player hit by laser");
                    }
                }
                break;
        }
    }
    private void ChangeWeapon()
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : ChangeWeapon");
        switch (m_currentWeapon)
        {
            case E_weapon.BOLTER:
                m_weaponPlaceHolderSpriteRenderer.sprite = m_WeaponBolterSprite;
                if (m_weaponPlaceHolderAudioSource) m_weaponPlaceHolderAudioSource.clip = m_WeaponBolterAudioClip;
                break;
            case E_weapon.LASER:
                m_weaponPlaceHolderSpriteRenderer.sprite = m_WeaponLaserSprite;
                if (m_weaponPlaceHolderAudioSource) m_weaponPlaceHolderAudioSource.clip = m_WeaponLaserAudioClip;
                break;
        }
    }

    [Header("Bolter Bullets")]
    public Sprite m_WeaponBolterSprite;
    public AudioClip m_WeaponBolterAudioClip;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] [Range(0f, 2f)] float bulletsCooldown = 0.5f;
    private float bulletsCooldownBuffer = 0f;

    public void FireBullet()
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Fire bullet");
        if (bulletsCooldownBuffer > bulletsCooldown)
        {
            bulletsCooldownBuffer = 0f;

            GameObject bullet = Instantiate(bulletPrefab, m_weaponPlaceHolderTransform.position, m_weaponPlaceHolderTransform.rotation);
            BulletScript bulletScript = bullet.GetComponent<BulletScript>();
            Vector3 translation = (m_weaponPlaceHolderTransform.position - transform.position);
            bulletScript.SetVelocity(translation.x, translation.y);
        }
        else bulletsCooldownBuffer += Time.deltaTime;
    }

    [Header("Laser")]
    public Sprite m_WeaponLaserSprite;
    public AudioClip m_WeaponLaserAudioClip;
    private LineRenderer m_laserLineRenderer;
    [SerializeField] [Range(0f, 2f)] float laserCooldown = 0.5f;
    [SerializeField] [Range(1f, 10f)] float laserReachDistance = 5f;
    private float laserCooldownBuffer = 0f;
    private void InitLaserLineRenderer()
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : InitLaserLineRenderer");
        m_laserLineRenderer = GetComponent<LineRenderer>();
        if (m_debug && !m_laserLineRenderer) Debug.LogError("Can't find AudioSource on WeaponPlaceHolder for player #" + m_playerID);
        m_laserLineRenderer.enabled = true;
        m_laserLineRenderer.useWorldSpace = true;
        m_laserLineRenderer.startWidth = 0.1f;
    }
    private GameObject FireLaser()
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Fire laser");
        if (laserCooldownBuffer > laserCooldown)
        {
            if (m_debug) Debug.Log("Player #" + m_playerID + " : laserCooldownBuffer > laserCooldown");
            m_laserLineRenderer.enabled = true;
            laserCooldownBuffer = 0f;

            Ray2D ray = new Ray2D(m_weaponPlaceHolderTransform.position, (m_weaponPlaceHolderTransform.position - m_transform.position));
            RaycastHit2D hit;

            m_laserLineRenderer.SetPosition(0, ray.origin);

            hit = Physics2D.Raycast(ray.origin, ray.direction, laserReachDistance);

            if (hit.collider)
            {
                m_laserLineRenderer.SetPosition(1, hit.point);
                return hit.collider.gameObject;
            }
            else
            {
                m_laserLineRenderer.SetPosition(1, ray.GetPoint(laserReachDistance));
                return null;
            }
        }
        else
        {
            m_laserLineRenderer.enabled = false;
            laserCooldownBuffer += Time.deltaTime;
        }
        return null;
    }
    #endregion
}
