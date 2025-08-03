using System.Collections;
using UnityEngine;
using UnityEngine.AI;
public class GuardNPC_Behaviour : MonoBehaviour
{
    NavMeshAgent GuardNav;
    [SerializeField]
    string currentState = "Idle";
    [SerializeField]
    Transform GuardPoint1;
    [SerializeField]
    Transform GuardPoint2;
    public Transform[] navGuardPoints;
    private int currentPatrolIndex = 0;

    void Awake()
    {
        GuardNav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        navGuardPoints = new Transform[] { GuardPoint1,GuardPoint2 };
        StartCoroutine(currentState);
    }
    IEnumerator Idle()
    {
        //Debug.Log("Going Idle");
        while (currentState == "Idle")
        {
            yield return new WaitForSeconds(3);
            StartCoroutine(SwitchState("Patrolling"));
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
        //Debug.Log("Starting Patrol");
        while (currentState == "Patrolling")
        {
            Transform CurrentPatrolPoint = navGuardPoints[currentPatrolIndex];
            GuardNav.SetDestination(CurrentPatrolPoint.position);
            while (Vector3.Distance(transform.position, CurrentPatrolPoint.position) >= 0.1f)
            {
                yield return null;
            }
            //Debug.Log("Next Point!");
            currentPatrolIndex = (currentPatrolIndex + 1) % navGuardPoints.Length;
            yield return StartCoroutine(SwitchState("Idle"));
        }
    }
}
