using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour {

    public float patrolTime = 10f;
    public float aggroRange = 10f;
    public Transform[] waypoints;

    private int index;
    private float speed, agentSpeed;
    private Transform playerTransform;

    //private Animator anim;
    private NavMeshAgent agent;

    private void Awake() {

        //anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (agent != null) {

            agentSpeed = agent.speed;
        }

        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Patrol", 0, 0.5f);

        if (waypoints.Length > 0) {

            InvokeRepeating("GetNextWaypoint", 0, patrolTime);
        }
    }

    void GetNextWaypoint() {

        index = index == waypoints.Length - 1 ? 0 : index + 1;
    }

    void Patrol() {

        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;

        if (playerTransform != null && Vector3.Distance(transform.position, playerTransform.position) < aggroRange) {

            agent.destination = playerTransform.position;
            agent.speed = agentSpeed;
        }
    }
}
