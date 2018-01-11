using UnityEngine;

public class ShootBullet : MonoBehaviour
{
    /*
    [Header("Bullets")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform playerBulletSpawnPlaceHolder;
    [SerializeField] [Range(0f, 2f)] float cooldown = 0.5f;
    float buffer = 0f;
	
    public void FireBullet()
    {
        if (buffer > cooldown)
        {
            buffer = 0f;

            GameObject bullet = Instantiate(bulletPrefab, playerBulletSpawnPlaceHolder.position, playerBulletSpawnPlaceHolder.rotation);
            FMA_BulletScript bulletScript = bullet.GetComponent<FMA_BulletScript>();
            Vector3 translation = (playerBulletSpawnPlaceHolder.position - transform.position);
//            bulletScript.SetVelocity(translation.x, translation.y);
        }
        else buffer += Time.deltaTime;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch(collision.transform.tag)
        {
            case "bullet":
                Debug.Log("Player hit");
                break;
        }        
    }
    */
}
