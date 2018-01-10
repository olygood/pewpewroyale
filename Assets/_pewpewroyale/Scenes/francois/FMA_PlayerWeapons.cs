using UnityEngine;

public class FMA_PlayerWeapons
{
    private bool m_debug = false;
    private int m_playerID;

    private FMA_PlayerScript m_playerScript;

    private GameObject m_weaponPlaceHolder;
    private Transform m_weaponPlaceHolderTransform;
    private SpriteRenderer m_weaponPlaceHolderSpriteRenderer;
    private AudioSource m_weaponPlaceHolderAudioSource;

    private LineRenderer m_weaponPlaceHolderLaserLineRenderer;

    private FMA_WeaponSettings m_weaponsSettings;

    public FMA_PlayerWeapons(int playerID, GameObject weaponPlaceHolder, FMA_WeaponSettings weaponsSettings, FMA_PlayerScript playerScript)
    {
        m_playerID = playerID;
        m_weaponPlaceHolder = weaponPlaceHolder;
        m_playerScript = playerScript;
        m_weaponsSettings = weaponsSettings;

        InitWeapon();
    }

    private FMA_WeaponSettings.WeaponType m_currentWeapon;
    public FMA_WeaponSettings.WeaponType Weapon
    {
        get { return m_currentWeapon; }
        set
        {
            m_currentWeapon = value;
            ChangeWeapon();
        }
    }

    private void InitWeapon()
    {
        m_weaponPlaceHolderTransform = m_weaponPlaceHolder.GetComponent<Transform>();
        if (m_debug && !m_weaponPlaceHolderTransform) Debug.LogError("Can't find Transform on WeaponPlaceHolder for player #" + m_playerID);

        m_weaponPlaceHolderSpriteRenderer = m_weaponPlaceHolder.GetComponent<SpriteRenderer>();
        if (m_debug && !m_weaponPlaceHolderSpriteRenderer) Debug.LogError("Can't find SpriteRenderer on WeaponPlaceHolder for player #" + m_playerID);

        m_weaponPlaceHolderAudioSource = m_weaponPlaceHolder.GetComponent<AudioSource>();
        if (m_debug && !m_weaponPlaceHolderAudioSource) Debug.LogError("Can't find AudioSource on WeaponPlaceHolder for player #" + m_playerID);

        m_weaponPlaceHolderLaserLineRenderer = m_weaponPlaceHolder.GetComponent<LineRenderer>();
        if (m_debug && !m_weaponPlaceHolderLaserLineRenderer) Debug.LogError("Can't find LineRenderer on WeaponPlaceHolder for player #" + m_playerID);
        m_weaponPlaceHolderLaserLineRenderer.enabled = true;
        m_weaponPlaceHolderLaserLineRenderer.useWorldSpace = true;
        m_weaponPlaceHolderLaserLineRenderer.startWidth = 0.1f;

        Weapon = FMA_WeaponSettings.WeaponType.BOLTER;
    }

    public void Fire()
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Fire");
        switch (m_currentWeapon)
        {
            case FMA_WeaponSettings.WeaponType.BOLTER:
                FireBullet();
                break;
            case FMA_WeaponSettings.WeaponType.LASER:
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
            case FMA_WeaponSettings.WeaponType.BOLTER:
                m_weaponPlaceHolderSpriteRenderer.sprite = m_weaponsSettings.WeaponBolterSprite;
                if (m_weaponPlaceHolderAudioSource) m_weaponPlaceHolderAudioSource.clip = m_weaponsSettings.WeaponBolterAudioClip;
                break;
            case FMA_WeaponSettings.WeaponType.LASER:
                m_weaponPlaceHolderSpriteRenderer.sprite = m_weaponsSettings.WeaponLaserSprite;
                if (m_weaponPlaceHolderAudioSource) m_weaponPlaceHolderAudioSource.clip = m_weaponsSettings.WeaponLaserAudioClip;
                break;
        }
    }

    private float bulletsCooldownBuffer = 0f;
    public void FireBullet()
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Fire bullet");
        if (bulletsCooldownBuffer > m_weaponsSettings.BolterCooldown)
        {
            bulletsCooldownBuffer = 0f;

            GameObject bullet = m_weaponsSettings.CreateBullet(m_weaponPlaceHolderTransform.position, m_weaponPlaceHolderTransform.rotation);

            FMA_BulletScript bulletScript = bullet.GetComponent<FMA_BulletScript>();
            Vector3 translation = (m_weaponPlaceHolderTransform.position - m_playerScript.Position);
            bulletScript.SetVelocity(translation.x, translation.y, m_weaponsSettings.BulletSpeed);
        }
        else bulletsCooldownBuffer += Time.deltaTime;
    }

    private float laserCooldownBuffer = 0f;
    private GameObject FireLaser()
    {
        if (m_debug) Debug.Log("Player #" + m_playerID + " : Fire laser");
        if (laserCooldownBuffer > m_weaponsSettings.LaserCooldown)
        {
            if (m_debug) Debug.Log("Player #" + m_playerID + " : laserCooldownBuffer > laserCooldown");
            m_weaponPlaceHolderLaserLineRenderer.enabled = true;
            laserCooldownBuffer = 0f;

            Ray2D ray = new Ray2D(m_weaponPlaceHolderTransform.position, (m_weaponPlaceHolderTransform.position - m_playerScript.Position));
            RaycastHit2D hit;

            m_weaponPlaceHolderLaserLineRenderer.SetPosition(0, ray.origin);

            hit = Physics2D.Raycast(ray.origin, ray.direction, m_weaponsSettings.LaserReachDistance);

            if (hit.collider)
            {
                m_weaponPlaceHolderLaserLineRenderer.SetPosition(1, hit.point);
                return hit.collider.gameObject;
            }
            else
            {
                m_weaponPlaceHolderLaserLineRenderer.SetPosition(1, ray.GetPoint(m_weaponsSettings.LaserReachDistance));
                return null;
            }
        }
        else
        {
            m_weaponPlaceHolderLaserLineRenderer.enabled = false;
            laserCooldownBuffer += Time.deltaTime;
        }
        return null;
    }
}
