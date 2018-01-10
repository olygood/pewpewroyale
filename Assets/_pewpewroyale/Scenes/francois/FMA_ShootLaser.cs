using UnityEngine;

public class ShootLaser : MonoBehaviour {

    private Transform m_transform;
    private LineRenderer m_laserLineRenderer;

    [Header("Laser")]
    [SerializeField] Transform playerBulletSpawnPlaceHolder;
    [SerializeField] [Range(0f, 2f)] float cooldown = 0.5f;
    [SerializeField] [Range(1f, 10f)] float reachDistance = 5f;
    float buffer = 0f;
    
    void Start ()
    {
        m_transform = GetComponent<Transform>();
        m_laserLineRenderer = GetComponent<LineRenderer>();
        m_laserLineRenderer.enabled = true;
        m_laserLineRenderer.useWorldSpace = true;
        m_laserLineRenderer.startWidth = 0.1f;
    }
	
	void Update ()
    {
        GameObject collide = FireLaser();
        if(collide != null)
        {
            if(collide.tag == "Player")
            {
                Debug.Log("Player hit by laser");
            }
        }
    }

    private GameObject FireLaser()
    {
        if (Input.GetButton("Fire2"))
        {
            m_laserLineRenderer.enabled = true;
            if (buffer > cooldown)
            {
                buffer = 0f;

                Ray2D ray = new Ray2D(playerBulletSpawnPlaceHolder.position, (playerBulletSpawnPlaceHolder.position - m_transform.position));
                RaycastHit2D hit;

                m_laserLineRenderer.SetPosition(0, ray.origin);

                hit = Physics2D.Raycast(ray.origin, ray.direction, reachDistance);

                if (hit.collider)
                {
                    m_laserLineRenderer.SetPosition(1, hit.point);
                    return hit.collider.gameObject;
                }
                else
                {
                    m_laserLineRenderer.SetPosition(1, ray.GetPoint(reachDistance));
                    return null;
                }
            }
            else buffer += Time.deltaTime;
        }
        else m_laserLineRenderer.enabled = false;
        return null;
    }

    /*
    //source : https://www.youtube.com/watch?v=UZwIni8sf8o
    IEnumerator _FireLaser()
    {
        m_laserLineRenderer.enabled = true;

        while (Input.GetButton("Fire2"))
        {
            Ray2D ray = new Ray2D(playerBulletSpawnPlaceHolder.position, playerBulletSpawnPlaceHolder.right);
            RaycastHit2D hit;

            m_laserLineRenderer.SetPosition(0, ray.origin);

            hit = Physics2D.Raycast(ray.origin, Vector2.right, reachDistance);

            if (hit.collider)
            {
                m_laserLineRenderer.SetPosition(1, hit.point);
            }
            else
                m_laserLineRenderer.SetPosition(1, ray.GetPoint(reachDistance));

            yield return null;
        }
        m_laserLineRenderer.enabled = false;
    }
    */
}
