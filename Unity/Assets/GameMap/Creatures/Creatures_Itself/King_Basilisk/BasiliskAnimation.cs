using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BasiliskAnimation : MonoBehaviour
{
    [Header("Link")]
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask Ground, Player;
    public Rigidbody rb;
    public Transform eye;
    Ray ray;

    [Header("Ranges")]
    public float walkPointRange;
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    [Header("Patrolling")]
    private Vector3 walkPoint;
    private bool walkPointSet;

    [Header("Animation")]
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
        rb= GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, Vector3.down);
        playerInSightRange = Physics.CheckSphere(eye.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(eye.position, attackRange, Player);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange || playerInAttackRange)
        {
            StartCoroutine(WaitFor(3)); ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            animator.SetBool("Bite", true);
        }
        else
        {
            animator.SetBool("Bite", false);
        }

    }
    IEnumerator WaitFor(int time)
    {
        yield return new WaitForSeconds(time);
        animator.SetBool("PlayerDetect", true);
    }

    private void Patroling()
    {
        agent.speed = 7;
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.speed = 11;
        agent.SetDestination(new Vector3(player.position.x-45, player.position.y, player.position.z-20));
    }


}
