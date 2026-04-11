using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject target;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        agent.SetDestination(target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
