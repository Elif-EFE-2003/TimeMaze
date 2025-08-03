using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Transform[] checkpoints;  // Checkpoint noktalarý
    public Transform player;          // Oyuncu referansý
    public float chaseDistance = 10f; // Kovalama mesafesi

    private NavMeshAgent agent;
    private int currentCheckpointIndex = 0;
    private bool chasingPlayer = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextCheckpoint();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance && CanSeePlayer())
        {
            chasingPlayer = true;
        }
        else if (distanceToPlayer > chaseDistance + 5f || !CanSeePlayer())
        {
            chasingPlayer = false;
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GoToNextCheckpoint();
            }
        }

        if (chasingPlayer)
        {
            agent.SetDestination(player.position);
        }
    }

    void GoToNextCheckpoint()
    {
        if (checkpoints.Length == 0) return;

        currentCheckpointIndex = Random.Range(0, checkpoints.Length);
        agent.SetDestination(checkpoints[currentCheckpointIndex].position);
    }

    bool CanSeePlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.position);

        // NPC göz yüksekliðini ayarla (örneðin 1.5f)
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance))
        {
            if (hit.transform == player)
            {
                return true; // Oyuncu doðrudan görülebiliyor
            }
        }
        return false; // Engel var veya oyuncu görünmüyor
    }
}
