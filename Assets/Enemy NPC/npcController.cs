using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class npcController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public float maxRoamDistance;
    public float gravityPower = -9.8f; // the power of gravity applied to the npc
    public float groundClearance; // the clearance from the ground
    public float groundDistance;

    private float RoamTimer;

    [HideInInspector] public int characterState;
    [HideInInspector] public Vector3 motionVector, gravityVector; // vectors used for npc movement

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
        Roam();
        gravity();
    }

    void Roam()
    {
        if (characterState != 0) return;

        if (Time.time > RoamTimer)
        {
            float a = Random.Range(0, 2);
            RoamTimer = Time.time + 20;
            if (isGrounded())
            {
                Vector3 destination = new Vector3(transform.position.x + Random.Range(0, maxRoamDistance) * (a == 1 ? 1 : -1),
                                                  transform.position.y,
                                                  transform.position.z + Random.Range(0, maxRoamDistance) * (a == 1 ? 1 : -1));

                NavMeshHit hit;
                if (NavMesh.SamplePosition(destination, out hit, 2.0f, NavMesh.AllAreas))
                {
                    navMeshAgent.SetDestination(hit.position);

                    // Calculate the direction to the destination and update the vertical and horizontal variables
                    Vector3 direction = (hit.position - transform.position).normalized;
                    navMeshAgent.Move(direction * Time.deltaTime);
                    animator.SetFloat("vertical", direction.z);
                    animator.SetFloat("horizontal", direction.x);
                }
            }
        }
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


    void animations()
    {
        animator.SetInteger("state", characterState);
    }

    string getCharState()
    {
        switch (characterState)
        {
            case 0:
                return "peaceful";
            case 1:
                return "Combat";
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