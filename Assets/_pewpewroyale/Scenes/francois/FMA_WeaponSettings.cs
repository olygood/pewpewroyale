using UnityEngine;

public class FMA_WeaponSettings : MonoBehaviour
{
    #region singleton
    private static FMA_WeaponSettings _instance;
    public static FMA_WeaponSettings Instance
    {
        get { return _instance; }
    }
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public enum WeaponType
    {
        BOLTER,
        LASER
    }

    #region Bolter
    [Header("Bolter")]
    [SerializeField]
    [Range(0f, 2f)]
    public float m_bolterCooldown = 0.1f;
    public float BolterCooldown
    {
        get { return m_bolterCooldown; }
    }
    public Sprite m_weaponBolterSprite;
    public Sprite WeaponBolterSprite
    {
        get { return m_weaponBolterSprite; }
    }
    public AudioClip m_weaponBolterAudioClip;
    public AudioClip WeaponBolterAudioClip
    {
        get { return m_weaponBolterAudioClip; }
    }

    [Header("Bolter's bullets")]
    [SerializeField] [Range(1f, 50f)]
    public float m_bulletSpeed = 10f;
    public float BulletSpeed
    {
        get { return m_bulletSpeed; }
    }
    /*
    public float m_bulletLifeTime = 2.5f;
    public float BulletLifeTime
    {
        get { return m_bulletLifeTime; }
    }*/
    #endregion

    #region Laser
    [Header("Laser")]
    [SerializeField]
    [Range(0f, 2f)]
    float m_laserCooldown = 0f;
    public float LaserCooldown
    {
        get { return m_laserCooldown; }
    }

    [SerializeField] [Range(1f, 10f)] float m_laserReachDistance = 10f;
    public float LaserReachDistance
    {
        get { return m_laserReachDistance; }
    }

    public Sprite m_weaponLaserSprite;
    public Sprite WeaponLaserSprite
    {
        get { return m_weaponLaserSprite; }
    }
    public AudioClip m_weaponLaserAudioClip;
    public AudioClip WeaponLaserAudioClip
    {
        get { return m_weaponLaserAudioClip; }
    }

    [SerializeField] GameObject bulletPrefab;
    public GameObject CreateBullet(Vector3 position, Quaternion rotation)
    {
        return Instantiate(bulletPrefab, position, rotation); ;
    }
    #endregion
}
