using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class ShootBullet : MonoBehaviour
{
    [Header("Bullets")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform playerBulletSpawnPlaceHolder;
    [SerializeField] [Range(0f, 2f)] float cooldown = 0.5f;
    float buffer = 0f;
    //private Player Joueur1;//
    public int playerId = 0; // The Rewired player id of this character

    public float moveSpeed = 3.0f;
    public float bulletSpeed = 15.0f;

    private Player player; // The Rewired Player
    private CharacterController cc;
    private Vector3 moveVector;
    private bool fire;
    [System.NonSerialized] // Don't serialize this so the value is lost on an editor script recompile.
    private bool initialized;

    void Awake()
    {
        // Get the character controller
        cc = GetComponent<CharacterController>();
    }

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        player = ReInput.players.GetPlayer(playerId);

        initialized = true;
    }

    void Start ()
    {
		
	}
	
	void Update ()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize(); // Reinitialize after a recompile in the editor

        GetInput();
        ProcessInput();

        /*        if (Joueur1.GetButton("ShootP1"))//
                {
                    FireBullet();
                }*/
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("MoveHorizontalP1"); // get input by name or action id
        moveVector.y = player.GetAxis("MoveVerticalP1");
        fire = player.GetButtonDown("Fire");
    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            cc.Move(moveVector * moveSpeed * Time.deltaTime);
        }

        if (fire)
        {
            FireBullet();
//            GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position + transform.right, transform.rotation);
//            bullet.GetComponent<Rigidbody>().AddForce(transform.right * bulletSpeed, ForceMode.VelocityChange);
        }
    }

        private void FireBullet()
    {
        if (buffer > cooldown)
        {
            Instantiate(bulletPrefab, playerBulletSpawnPlaceHolder.position, playerBulletSpawnPlaceHolder.rotation);
            buffer = 0f;
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
}
