using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class WorriedNPC_Behaviour : MonoBehaviour
{
    NavMeshAgent NavPoint;
    [SerializeField]
    Transform Target;
    [SerializeField]
    string currentState = "Idle";
    [SerializeField]
    Transform WorryPoint1;
    [SerializeField]
    Transform WorryPoint2;
    [SerializeField]
    Transform WorryPoint3;
    [SerializeField]
    Transform WorryPoint4;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    void Awake()
    {
        NavPoint = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        patrolPoints = new Transform[] { WorryPoint1, WorryPoint2, WorryPoint3, WorryPoint4 };
        StartCoroutine(currentState);
    }
    IEnumerator Idle()
    {
        Debug.Log("Going Idle");
        while (currentState == "Idle")
        {
            if (Target != null)
            {
                StartCoroutine(SwitchState("ApproachPlayer"));
            }
            yield return new WaitForSeconds(1);
            StartCoroutine(SwitchState("Patrolling"));
        }
    }
    IEnumerator ApproachPlayer()
    {
        Debug.Log("Chasing!");
        while (currentState == "ApproachPlayer")
        {
            yield return null;
            NavPoint.SetDestination(Target.position);
            if (Target == null)
            {
                //change state to Idle
                StartCoroutine(SwitchState("Idle"));
            }
            else
            {
                NavPoint.SetDestination(Target.position);
            }
        }
    }
    IEnumerator SwitchState(string newState)
    {
        if (currentState == newState)
        {
            yield break;
        }
        currentState = newState;
        StartCoroutine(currentState);
    }
    IEnumerator Patrolling()
    {
        Debug.Log("Starting Patrol");
        while (currentState == "Patrolling")
        {
            Transform CurrentPatrolPoint = patrolPoints[currentPatrolIndex];
            NavPoint.SetDestination(CurrentPatrolPoint.position);
            while (Vector3.Distance(transform.position, CurrentPatrolPoint.position) >= 0.1f)
            {
                if (Target != null)
                {
                    StartCoroutine(SwitchState("ChaseTarget"));
                    yield break;
                }
                yield return null;
            }
            Debug.Log("Next Point!");
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            yield return StartCoroutine(SwitchState("Idle"));
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Target = other.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Target = null;
            StartCoroutine(SwitchState("Idle"));
        }
    }
}
