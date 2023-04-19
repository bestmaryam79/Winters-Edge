using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]

public class enemyNPC : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private float horizontal, vertical;
    private float RoamTimer;

    public float maxRoamDistance;
    public float gravityPower = -9.8f; // the power of gravity applied to the npc
    public float groundClearance; // the clearance from the ground
    public float groundDistance;


    [HideInInspector] public int characterState;
    [HideInInspector] public Vector3 gravityVector; // vectors used for npc movement

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        gravityVector = new Vector3(0f, gravityPower, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        animations();
        gravity();
        Roam();
    }

    void gravity()
    {
        if (isGrounded() && gravityVector.y < 0)
            gravityVector.y = 0;

        gravityVector.y += gravityPower * Time.deltaTime; // Apply gravity to the gravity vector

        // Apply gravity to the character controller
        var controller = GetComponent<CharacterController>();
        controller.Move(gravityVector * Time.deltaTime);
    }


    void Roam()
    {
        //if (characterState != 0) return;
        //int timeTo = 0;

        
        float a = Random.Range(0, 2);
        RoamTimer = Time.time + 20;
        navMeshAgent.SetDestination(new Vector3(transform.position.x + Random.Range(maxRoamDistance/2, maxRoamDistance) * (a == 1 ? 1 : -1),0 
        ,transform.position.z + Random.Range(maxRoamDistance / 2, maxRoamDistance) * (a == 1 ? 1 : -1)));
    }


    void animations()
    {
        animator.SetFloat("vertical", vertical);
        animator.SetFloat("horizontal", horizontal);
        animator.SetInteger("state", characterState);
    }

    string getCharState()
    {
        switch(characterState)
        {
            case 0:
                return "peaceful";
            break;
            case 1:
                return "Combat";
            break;
        }
        return "out of range";

    }

    // Check if the npc is grounded using a sphere cast
    bool isGrounded()
    {
        // Cast a sphere below the npc to detect if they are on the ground
        return Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - groundDistance, transform.position.z), groundClearance);
    }
}

