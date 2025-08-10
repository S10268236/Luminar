using System.Collections;
using TMPro;
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
    //For looking at player during convo
    [SerializeField]
    Transform PlayerPosition;
    //Generate random number for conversation selection
    private int GuardConvoNo;
    //Access guard conversation
    [SerializeField]
    GameObject GuardConvo1;
    [SerializeField]
    GameObject GuardConvo2;
    [SerializeField]
    GameObject GuardConvo3;
    [SerializeField]
    GameObject GuardConvo4;
    //Change convo text
    public TextMeshProUGUI Convo1;
    public TextMeshProUGUI Convo2;
    public TextMeshProUGUI Convo3;
    public TextMeshProUGUI Convo4;

    void Awake()
    {
        GuardNav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        navGuardPoints = new Transform[] { GuardPoint1, GuardPoint2 };
        StartCoroutine(currentState);
    }
    public IEnumerator Idle()
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
        Debug.Log("Starting Patrol");
        while (currentState == "Patrolling")
        {
            Transform CurrentPatrolPoint = navGuardPoints[currentPatrolIndex];
            GuardNav.SetDestination(CurrentPatrolPoint.position);
            while (Vector3.Distance(transform.position, CurrentPatrolPoint.position) >= 0.8f)
            {
                yield return null;
            }
            //Debug.Log("Next Point!");
            currentPatrolIndex = (currentPatrolIndex + 1) % navGuardPoints.Length;
            yield return StartCoroutine(SwitchState("Idle"));
        }
    }
    public void LookAtPlayer()
    {
        StopAllCoroutines();
        transform.LookAt(PlayerPosition.position);
        GuardNav.SetDestination(transform.position);
        GuardConvoNo = Random.Range(0, 4);
        //Display Conversation window
        if (GuardConvoNo == 0)
        {
            Convo1.text = "Eat my shit";
            GuardConvo1.SetActive(true);
        }
        else if (GuardConvoNo == 1)
        {
            Convo2.text = "I used to run track, but then I took a Jeffrey to the knee";
            GuardConvo2.SetActive(true);
        }
        else if (GuardConvoNo == 2)
        {
            Convo3.text = "Did you know that bells sound different in this game? They go YiTing~~";
            GuardConvo3.SetActive(true);
        }
        else if (GuardConvoNo == 3)
        {
            Convo4.text = "BOO! Hmmm...Baihui would have been scared there..";
            GuardConvo4.SetActive(true);
        }
    }
    public void ResumePatrol()
    {
        StartCoroutine(Idle());
    }
}
