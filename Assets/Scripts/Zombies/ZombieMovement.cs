using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    public float fov = 120f;
    public Transform target;
    public bool inSight;
    public float awakeDistance = 200f;
    public bool awareOfPlayer;
    public NavMeshAgent zombieAgent;
    bool playerInVision;
    private void Update()
    {
        DrawRay();
        float PlayerDistance = Vector3.Distance(target.position, transform.position);
        Vector3 playerDirection = target.position - transform.position;
        float playerAngle = Vector3.Angle(transform.forward, playerDirection);
        if (playerAngle <= fov / 2f)
        {
            inSight = true;
            Debug.Log("player in sight");
        }
        else
        {
            inSight = false;
        }
        if (inSight == true && PlayerDistance <= awakeDistance && playerInVision == true)
        {
            awareOfPlayer = true;
        }
        if (awareOfPlayer == true)
        {
            zombieAgent.SetDestination(target.position);
        }
    }
    void DrawRay()
    {
        Vector3 playerDirection = target.position - transform.position;
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
