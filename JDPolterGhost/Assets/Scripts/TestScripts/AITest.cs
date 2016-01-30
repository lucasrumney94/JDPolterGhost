using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class AITest : MonoBehaviour
{
    public GameObject destination;

    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

	void Update()
    {
        agent.SetDestination(destination.transform.position);
    }
}
