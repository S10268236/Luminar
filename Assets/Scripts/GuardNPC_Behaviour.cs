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
    //Change convo text
    public TextMeshProUGUI Convo1;
    //Control Animations
    private Animator mAnimation;
    //Debugging SwitchState-access currnt state
    private Coroutine stateCoroutine;

    void Awake()
    {
        GuardNav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        mAnimation = GetComponent<Animator>();
        //Debug.Log($"GuardPoint1: {(GuardPoint1 == null ? "null" : GuardPoint1.name)}");
        //Debug.Log($"GuardPoint2: {(GuardPoint2 == null ? "null" : GuardPoint2.name)}");
        navGuardPoints = new Transform[] { GuardPoint1, GuardPoint2 };
        StartCoroutine(currentState);
    }
    public IEnumerator Idle()
    {
        //Debug.Log("Going Idle");
        while (currentState == "Idle")
        {
            if (mAnimation != null)
            {
                mAnimation.SetBool("Patrol",false);
            }
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
        if (stateCoroutine != null)
        {
            StopCoroutine(stateCoroutine);
            stateCoroutine = null;
        }
        switch (currentState)
        {
            case "Patrolling":
                stateCoroutine = StartCoroutine(Patrolling());
                break;
            case "Idle":
                stateCoroutine = StartCoroutine(Idle());
                break;
            // Add other states if needed
            default:
                Debug.LogWarning($"Unknown state: {currentState}");
                break;
        }
        yield return null;
    }
    IEnumerator Patrolling()
    {
        Debug.Log($"Patrol points: {navGuardPoints?.Length}");
        //Debug.Log("Starting Patrol");
        while (currentState == "Patrolling")
        {
            if (mAnimation != null)
            {
                mAnimation.SetBool("Patrol", true);
            }
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
        if (mAnimation != null)
        {
            mAnimation.SetBool("Patrol",false);
        }
        StopAllCoroutines();
        transform.LookAt(PlayerPosition.position);
        GuardNav.SetDestination(transform.position);
        GuardConvoNo = Random.Range(0, 4);
        //Display Conversation window
        if (GuardConvoNo == 0)
        {
            Convo1.text = "I smell something fishy";
            GuardConvo1.SetActive(true);
        }
        else if (GuardConvoNo == 1)
        {
            Convo1.text = "I used to run track, but then I took a scam to the knee";
            GuardConvo1.SetActive(true);
        }
        else if (GuardConvoNo == 2)
        {
            Convo1.text = "Did you know that fish are built different in this game? Last week, one of them took my wallet";
            GuardConvo1.SetActive(true);
        }
        else if (GuardConvoNo == 3)
        {
            Convo1.text = "I wish I was Skyrim NPC. I might have had better lines. ";
            GuardConvo1.SetActive(true);
        }
    }
    public void ResumePatrol()
    {
        //Debug.Log($"ResumePatrol called; navGuardPoints length = {navGuardPoints?.Length}");
        StartCoroutine(Patrolling());
    }
}
