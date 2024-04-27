using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinalTwoLayerEnemy : MonoBehaviour
{
        [Header("Enemy")]
        public Enemy Enemy;
        private NavMeshAgent agent;
        private LayerMask Ground, Player;

        [Header("Privates")]
        private Transform player;
        [HideInInspector] public  List<Transform> Damagers;
        [HideInInspector] public int heal;
        [HideInInspector] public int CurrentHeal;
        private bool healDifference;
        private float rotationSpeed = 0.5f;

    [Header("Animations")]
        private Animator animator;
        private string currentAnimation = "";
        private string currentAnimation2 = "";
        private string attack;
        private bool NextAttack;
        //private Coroutine attackCoroutine;

        //States
        private float sightRange;
        private float sightAngle;


        private float attackRange;
        private bool playerInSightRange;
        private bool playerWasSeen;
        private Vector3 Searchpoint;
        private bool searchtimer;


        private Vector3 walkPoint;
        private bool walkPointSet;
        private float walkPointRange = 400;

        private bool fly;
        private bool canthreath;
        private int spawn = 0;
        private bool death = false;




        private void Start()
        {
            Damagers.Clear();
            canthreath = Enemy.CanThreath;
            player = GameObject.Find("PlayerControl").transform.Find("Player").transform;
            agent = GetComponent<NavMeshAgent>();
            Ground = LayerMask.GetMask("Ground");
            Player = LayerMask.GetMask("Player");
            sightRange = Enemy.SeeDistance;
            sightAngle = Enemy.SeeAngle;
            attackRange = Enemy.AttackDistance;
            Speed(Enemy.WalkSpeed);
            animator = GetComponent<Animator>();
            NextAttack = false;
            agent.stoppingDistance = Enemy.MinDistance+3;
            DamageCheck(transform);
            SwitchAttackMethod();
            if (spawn == 0)
            {
                heal = Enemy.Heal;
                CurrentHeal = Enemy.Heal;
                ChangeAnimation("walk");
                ChangeAction("stand");
                spawn = 1;
                death = false;
            }
            if (Enemy.AttackCount > 1)
            {
                StartCoroutine(AttackSwitch());

            }
            AddScriptToChildrenWithCollider(transform);
    }

        private IEnumerator AttackSwitch()
        {
            while (true)
            {
                float waitTime = UnityEngine.Random.Range(2f,4f);
                yield return new WaitForSeconds(waitTime);
                SwitchAttackMethod();
            }
        }

        private void AddScriptToChildrenWithCollider(Transform parent)
        {
            // Iterate over each child of the parent transform
            foreach (Transform child in parent)
            {
                // Check if the child has a Collider component
                if (child.GetComponent<Collider>() != null)
                {
                    // Add the EnemyCollider script to the child
                    if (child.GetComponent<EnemyCollider>() == null)
                    {
                        child.gameObject.AddComponent(typeof(EnemyCollider));
                    }
                    child.GetComponent<EnemyCollider>().finaltwo = this;
                }

                // Recursively call this function for each child
                AddScriptToChildrenWithCollider(child);
            }
        }

        private void DamageCheck(Transform parent)
        {
            foreach (Transform child in parent)
        {
            // Check if the child has a Collider component
            if (child.GetComponent<DamageToPlayer>() != null)
            {
                Damagers.Add(child.transform);
                child.GetComponent<DamageToPlayer>().knowbacky=Enemy.knowbacky;
                child.GetComponent<DamageToPlayer>().knowbackxz= Enemy.knowbackxz;
            }

            // Recursively call this function for each child
            DamageCheck(child);
        }
        }

        // Update is called once per frame
        private void Update()
        {
            FieldViewCheck();
            if (heal != CurrentHeal) { healDifference = true; GetHit(); }
            else { healDifference = false; }
            if (!playerInSightRange && !playerWasSeen && !healDifference) Patroling();
            if (playerInSightRange && !healDifference && !fly) ChasePlayer();
            if (!playerInSightRange && playerWasSeen && !healDifference) SearchPlayer();
            if (fly && playerInSightRange) { ChangeAnimation("walk"); Speed(Enemy.WalkSpeed); fly = false; }
        }

        private void Patroling()
        {
            ChangeAnimation("walk");
            ChangeAction("stand");
            Speed(Enemy.WalkSpeed);
            if (!walkPointSet) SearchWalkPoint();

            if (walkPointSet)
                agent.SetDestination(walkPoint);

            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            if (distanceToWalkPoint.magnitude < 1f)
                walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            float randomZ = UnityEngine.Random.Range(-walkPointRange, walkPointRange);
            float randomX = UnityEngine.Random.Range(-walkPointRange, walkPointRange);

            walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            if (Physics.Raycast(walkPoint, -transform.up, 2f, Ground))
                walkPointSet = true;
        }

        private void ChasePlayer()
        {
            if(transform.position.y+8 <player.position.y)
        {
            ChangeAnimation("stand");
            Speed(1);
            agent.SetDestination(transform.position);
            ChangeAction("stand");
            fly=true;
        }
        else
        {
            playerWasSeen = true;
            SeeMe(true);
            Vector3 lookAtPosition = Vector3.zero; // Set your target position here
            Quaternion targetRotation = Quaternion.LookRotation(lookAtPosition - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > Enemy.MinDistance && distance < Enemy.AttackDistance)
            {
                ColliderSwitch(true);
                Speed(Enemy.WalkSpeed);
                ChangeAnimation("run");
                ChangeAction(attack);
                agent.SetDestination(player.position);

            }
            else if (distance >= Enemy.AttackDistance)
            {
                ColliderSwitch(false);
                ChangeAction("stand");
                Speed(Enemy.RunSpeed);
                agent.SetDestination(player.position);
            }
            else
            {
                ChangeAnimation("stand");
                Speed(1);
                agent.SetDestination(transform.position);
                ChangeAction(attack);
            }

        }
            
        }

        private void SwitchAttackMethod()
        {
            if (!NextAttack)
            {
                attack = "attack";
                int random = Random.Range(1, Enemy.AttackCount);

                if (Damagers.Count > 1)
                {
                    for (int i = 0; i < Damagers.Count; i++)
                    {
                        Damagers[i].GetComponent<DamageToPlayer>().Damage = Enemy.Damages[random-1];
                    }
                }
                else
                {
                Damagers[0].GetComponent<DamageToPlayer>().Damage = Enemy.Damages[random-1];
                }

                attack = attack + random;
            }

        }

        private void SearchPlayer()
        {
            if (!searchtimer)
            {

                StartCoroutine(SearchDelay());
                Speed(Enemy.SearchSpeed);


                float randomChance = UnityEngine.Random.Range(0f, 1f);
                float rnd = UnityEngine.Random.Range(-100, 100);

                if (Enemy.SearchChance > randomChance)
                {
                    Searchpoint = (transform.position + new Vector3(rnd, 0, rnd));
                }
                else
                {
                    // Generate a random direction for searching
                    float randomAngle = UnityEngine.Random.Range(0f, 360f);
                    Vector3 randomDirection = Quaternion.Euler(0f, randomAngle, 0f) * transform.forward;

                    // Calculate a random position within the sight range
                    randomDirection = transform.position + randomDirection + new Vector3(50 + rnd, 0, 30 + rnd);
                    if (Physics.Raycast(randomDirection, -transform.up, 3f, Ground))
                    {
                        Searchpoint = randomDirection;
                    }
                }

            }
            else
            {
                agent.SetDestination(Searchpoint);
            }



        }

        private IEnumerator SearchDelay()
        {
            searchtimer = true;
            float waitTime = UnityEngine.Random.Range(10f, 20f);
            yield return new WaitForSeconds(waitTime);
            if (playerInSightRange)
            {
                searchtimer = false;
                yield break; // Exit the coroutine
            }
            if (searchtimer)
            {
                playerWasSeen = false;
                searchtimer = false;
                SeeMe(false);
                ChangeAnimation("walk");
                ChangeAction("stand");
                Speed(Enemy.WalkSpeed);
            }
        }

        public void GetHit()
        {
            Debug.Log(CurrentHeal);
            int healDifference = heal - CurrentHeal;
            heal = heal - healDifference;
            CurrentHeal = heal;
            if (CurrentHeal > 0)
            {
                ChangeAnimation("hit1");
                ChangeAction("stand");
        }
            else
            {

                if (!death)
                {
                    death = true;
                    ChangeAnimation("death");
                    ChangeAction("stand");
                }
                else
                {
                    Destroy(gameObject);
                }
            }


        }

        private void FieldViewCheck()
        {
            Collider[] rangeChecks = Physics.OverlapSphere(transform.position, sightRange, Player);
            if (rangeChecks.Length != 0)
            {
                Transform target = rangeChecks[0].transform;
                Vector3 directionToTarget = (target.position - transform.position).normalized;

                if (Vector3.Angle(transform.forward, directionToTarget) < sightAngle / 2)
                {
                    float DistanceToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, directionToTarget, DistanceToTarget, Ground))
                        playerInSightRange = true;
                    else playerInSightRange = false;
                }
                else playerInSightRange = false;
            }
            else if (playerInSightRange)
            {
                playerInSightRange = false;
            }
        }

        private void SeeMe(bool Seen)
        {
            if (Seen)
            {
                sightAngle = 360;
            }
            else
            {
                sightAngle = Enemy.SeeAngle;
            }
        }

        private void Speed(float speed)
        {
            if (agent.speed != speed)
            {
                agent.acceleration = Enemy.RunSpeed * 1.3f;
                agent.speed = Enemy.RunSpeed;
            }
        }


        private void ChangeAnimation(string animation, int layer=0, float crossfade = 0.2f)
        {
            if (currentAnimation != animation)
            {
               currentAnimation = animation; 
               animator.CrossFade(animation, crossfade,layer);
                
            }
        }

        private void ChangeAction(string animation, int layer = 1, float crossfade = 0.2f)
        {
            if (currentAnimation2 != animation)
            {
                currentAnimation2 = animation;
                animator.Play(animation, layer);
            }
        }

        private void ColliderSwitch(bool status)
        {
                for (int i = 0; i < Damagers.Count; i++)
                {
                Damagers[i].GetComponent<DamageToPlayer>().Attack = status;
                }
        }


}
