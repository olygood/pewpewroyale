using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

[AddComponentMenu("")]
[RequireComponent(typeof(CharacterController))]
public class Move : MonoBehaviour
{

    public int playerId = 0; // The Rewired player id of this character

    public float moveSpeed = 3.0f;
    public float bulletSpeed = 15.0f;
    public Transform mv;
    public float rotationSpeed = 20f;

    private Player player; // The Rewired Player
    private CharacterController cc;
    private Vector3 moveVector;
    private Vector3 rotateVector;
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

    void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!initialized) Initialize(); // Reinitialize after a recompile in the editor

        GetInput();
        ProcessInput();
    }

    private void GetInput()
    {
        // Get the input from the Rewired Player. All controllers that the Player owns will contribute, so it doesn't matter
        // whether the input is coming from a joystick, the keyboard, mouse, or a custom controller.

        moveVector.x = player.GetAxis("MoveHorizontal"); // get input by name or action id
        moveVector.y = player.GetAxis("MoveVertical");
        rotateVector.x = player.GetAxis("RotateHorizontal");
        rotateVector.y = player.GetAxis("RotateVertical");
    }

    private void ProcessInput()
    {
        // Process movement
        if (moveVector.x != 0.0f || moveVector.y != 0.0f)
        {
            cc.Move(moveVector * moveSpeed * Time.deltaTime);
        }

        // Process rotation
        if (rotateVector.x != 0.0f || rotateVector.y != 0.0f)
        {
            float tru = Mathf.Acos(rotateVector.x);

            Vector3 v3 = new Vector3(0f, 0f, tru);
            Quaternion qTo = Quaternion.LookRotation(v3);

            transform.rotation = Quaternion.Slerp(transform.rotation, qTo, rotationSpeed * Time.deltaTime);
        }
    }
}

	

