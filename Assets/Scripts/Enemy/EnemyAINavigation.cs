using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAINavigation : MonoBehaviour
{
    private Transform playerTransform;
    UnityEngine.AI.NavMeshAgent agent;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = playerTransform.position;
    }
}
