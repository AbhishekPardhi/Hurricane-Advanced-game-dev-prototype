using UnityEngine;
using UnityEngine.AI;

public class zombie2 : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public float fov = 120f;
    public float awakeDistance = 200f;
    public bool awareOfPlayer;
    public LayerMask whatIsPlayer,whatIsGround;

    public float health;
    public bool inSight;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    bool playerInVision;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float attackRange;
    public bool playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attack range
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        DrawRay();
        float PlayerDistance = Vector3.Distance(player.position, transform.position);
        Vector3 playerDirection = player.position - transform.position;
        float playerAngle = Vector3.Angle(transform.forward, playerDirection);
        if (playerAngle <= fov / 2f)
        {
            inSight = true;
        }
        if (inSight == true && PlayerDistance <= awakeDistance && playerInVision == true)
        {
            awareOfPlayer = true;
        }
        if (!awareOfPlayer && !playerInAttackRange) Patroling();
        if (awareOfPlayer && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && awareOfPlayer) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, awakeDistance);
    }
    void DrawRay()
    {
        Vector3 playerDirection = player.position - transform.position;
        RaycastHit hit;

        if (Physics.Raycast(transform.position, playerDirection, out hit))
        {
            if (hit.transform.tag == "Player")
            {
                playerInVision = true;
            }
            else
            {
                playerInVision = false;
            }
        }
    }
}

