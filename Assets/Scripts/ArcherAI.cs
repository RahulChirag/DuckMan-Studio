using UnityEngine;
using UnityEngine.AI;

public class ArcherAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target;

    private float rotationSpeed = 3;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (target != null)
        {
            // Move towards the target
            agent.SetDestination(target.position);

            Vector3 direction = target.position - transform.position;
            direction.y = 0f; // Optional: If you don't want vertical rotation
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
