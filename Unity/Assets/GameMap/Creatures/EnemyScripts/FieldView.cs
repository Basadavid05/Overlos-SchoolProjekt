using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class FieldView : MonoBehaviour
{
    [Header("Radius")]
    [HideInInspector] public float radius;
    public float normalradius;
    public float attackradius;
    [Range(0,360)]
    public float angle;

    [Header("Find")]
    public GameObject PlayerRef;
    public LayerMask targetMask;
    public LayerMask obsturctionMask;
    public bool canSeePlayer;

    bool attack=true;


    private void Start()
    {
        radius = normalradius;
        StartCoroutine(FOVRoutine());
    }
    private void Update()
    {
        if (canSeePlayer && attack)
        {
            radius = attackradius;
            // Check if player is outside the attack radius
            float distanceToPlayer = Vector3.Distance(transform.position, PlayerRef.transform.position);
            if (distanceToPlayer > attackradius)
            {
                // Player is outside attack radius, start WaitForNormal routine
                StartCoroutine(WaitForNormal());
            }
        }
        else radius = normalradius;
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait=new WaitForSeconds(0.3f);
        while (true)
        {
            yield return wait;
            FieldViewCheck();

        }
    }

    private IEnumerator WaitForNormal()
    {
        yield return new WaitForSeconds(7f);
        attack = false;

    }

    private void FieldViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);
        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float DistanceToTarget=Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, DistanceToTarget, obsturctionMask))
                canSeePlayer = true; 
                else canSeePlayer = false;
            }
            else canSeePlayer = false;
        }
        else if (canSeePlayer)
        {
            canSeePlayer = false;
        }
    }

}
