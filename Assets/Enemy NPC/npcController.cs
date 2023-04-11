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
    public float gravityPower = -9.8f;

    private float RoamTimer;

    [HideInInspector] public int characterState;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updatePosition = false;
        navMeshAgent.updateRotation = false;
        navMeshAgent.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        animations();
        Roam();
        ApplyGravity();
    }

    void Roam()
    {
        if (characterState != 0) return;

        if (Time.time > RoamTimer)
        {
            float a = Random.Range(0, 2);
            RoamTimer = Time.time + 20;
            Vector3 destination = new Vector3(transform.position.x + Random.Range(0, maxRoamDistance) * (a == 1 ? 1 : -1), transform.position.y, transform.position.z + Random.Range(0, maxRoamDistance) * (a == 1 ? 1 : -1));
            Vector3 desiredDirection = (destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(desiredDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * navMeshAgent.angularSpeed);
            navMeshAgent.nextPosition = transform.position + desiredDirection * navMeshAgent.speed * Time.deltaTime;
        }
    }

    void animations()
    {
        animator.SetFloat("vertical", navMeshAgent.velocity.normalized.magnitude);
        animator.SetFloat("horizontal", 0);
        animator.SetInteger("state", characterState);
    }

    private void ApplyGravity()
    {
        // check if the agent is on the ground
        if (navMeshAgent.isOnNavMesh)
        {
            // cancel the y-component of the agent's velocity
            Vector3 horizontalVelocity = new Vector3(navMeshAgent.velocity.x, 0, navMeshAgent.velocity.z);
            navMeshAgent.velocity = horizontalVelocity;

            // apply gravity as a separate move command
            Vector3 gravityVector = new Vector3(0, gravityPower, 0);
            navMeshAgent.Move(gravityVector * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            navMeshAgent.enabled = true;
        }
    }
}