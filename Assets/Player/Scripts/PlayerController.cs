using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [HideInInspector] public InputManager inputManager; // a reference to the InputManager component
    [HideInInspector] public CharacterController characterController; // a reference to the CharacterController component
    [HideInInspector] public Vector3 motionVector, gravityVector; // vectors used for player movement
    [HideInInspector] public Animator animator;

    [Header("character weapon")]
    public GameObject mele;
    private gameManager manager;

    [Range(1, 4)] public float movementSpeed = 2; // the movement speed of the player
    [Range(0, .5f)] public float groundClearance; // the clearance from the ground
    [Range(0, 1f)] public float groundDistance;

    public float gravityPower = -9.8f; // the power of gravity applied to the player
    public float jumpValue = -9.8f; // the force of the jump

    public int characterState = 0;

    private float gravityForce = -9.8f;

    void Start()
    {
        // Get InputManager and CharacterController components from this GameObject
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        manager = FindObjectOfType<gameManager>();
        if (manager == null) print("No manager in scene");

        spawnWeapon();
    }

    void spawnWeapon()
    {
        if (characterState == 1)
        {
            mele = Instantiate(manager.meleWeapons.GetComponent<meleWeapons>().weapons[PlayerPrefs.GetInt("currentWeaponIndex")]);
            mele.transform.parent = transform;
            mele.transform.localPosition = Vector3.zero;
            mele.transform.localEulerAngles = Vector3.zero;
            mele.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void Update()
    {
        movement();

        if(Input.GetKeyDown(KeyCode.A))
        {
            spawnWeapon();
        }
        
    }


    void movement()
    {
        animator.SetFloat("vertical", inputManager.vertical);
        animator.SetFloat("horizontal", inputManager.horizontal);
        animator.SetBool("grounded", isGrounded());
        animator.SetFloat("jump", inputManager.jump);
        animator.SetInteger("state",characterState);


        // Check if the player is on the ground and apply gravity
        if (isGrounded() && gravityVector.y < 0)
            gravityVector.y = -2;
        gravityVector.y += gravityPower * Time.deltaTime; // Apply gravity to the gravity vector
        characterController.Move(gravityVector * Time.deltaTime); // Move the character controller using the gravity vector


        if (isGrounded())
        {
            // Calculate the motion vector based on input and movement speed
            motionVector = transform.right * inputManager.horizontal + transform.forward * inputManager.vertical;
            characterController.Move(motionVector * movementSpeed * Time.deltaTime);
        }

        if(inputManager.jump != 0)
        {
            jump();
        }

    }

    // If the player presses the jump button, jump
    void jump()
    {
        if (isGrounded())
        {

            // Move the character controller using the motion vector
            characterController.Move(transform.up * (jumpValue * -2 * gravityForce) * Time.deltaTime);
        }
    }

    // Check if the player is grounded using a sphere cast
    bool isGrounded()
    {
        // Cast a sphere below the player to detect if they are on the ground
        return Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - groundDistance, transform.position.z), groundClearance);
    }

    string getCharState()
    {
        switch(characterState)
        {
            case 0:
                return "peaceful";
            break;
            case 1:
                return "combat";
            break;
        }
        return "out of range"; 

    }


    /*void playerStates()
    {
        if (characterState == 0 && !takingDamage)
        {
            peacefulMovement();
        }
        else
        {
            combatMovement();
        }
    }
    */

    // Show if the player is grounded or not in the GUI
    void OnGUI()
    {
        float rectPos = 50;
        GUI.Label(new Rect(20, rectPos, 200, 20), "is Grounded:" + isGrounded());
        rectPos += 30f;

        GUI.Label(new Rect(20, rectPos, 200, 20), "player state:" + getCharState());
        rectPos += 30f;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundDistance, transform.position.z), groundClearance);
    }


}
