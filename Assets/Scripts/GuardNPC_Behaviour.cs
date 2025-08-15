using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GuardNPC_Behaviour : MonoBehaviour
{
    //For Navmesh to work
    NavMeshAgent GuardNav;
    //display current FSM state
    [SerializeField]
    string currentState = "Idle";
    //Inputs for position of guard spots
    [SerializeField]
    Transform GuardPoint1;
    //Inputs for position of guard spots
    [SerializeField]
    Transform GuardPoint2;
    //Create array for guard points to populate
    public Transform[] navGuardPoints;
    //Track which guard point is currently being used
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
    /// <summary>
    /// Assign Navmeshagent
    /// </summary>
    void Awake()
    {
        GuardNav = GetComponent<NavMeshAgent>();
    }
    /// <summary>
    /// Assign animation variable and guard point locations to array. Begin Coroutine
    /// </summary>
    void Start()
    {
        mAnimation = GetComponent<Animator>();
        //Debug.Log($"GuardPoint1: {(GuardPoint1 == null ? "null" : GuardPoint1.name)}");
        //Debug.Log($"GuardPoint2: {(GuardPoint2 == null ? "null" : GuardPoint2.name)}");
        navGuardPoints = new Transform[] { GuardPoint1, GuardPoint2 };
        StartCoroutine(currentState);
    }
    /// <summary>
    /// Idle Coroutine, Stops walking animation,Starts idle animation for period, begins Patrol Coroutine on end
    /// </summary>
    /// <returns></returns>
    public IEnumerator Idle()
    {
        //Debug.Log("Going Idle");
        while (currentState == "Idle")
        {
            if (mAnimation != null)
            {
                mAnimation.SetBool("Patrol", false);
            }
            yield return new WaitForSeconds(3);
            StartCoroutine(SwitchState("Patrolling"));
        }
    }
    /// <summary>
    /// Coroutine for switching states, tracks current and next coroutine and stops it when switching to a new one
    /// </summary>
    /// <param name="newState"></param>
    /// <returns></returns>
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
            default:
                Debug.LogWarning($"Unknown state: {currentState}");
                break;
        }
        yield return null;
    }
    /// <summary>
    /// Patrol coroutine
    /// </summary>
    /// <returns></returns>
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
            Transform CurrentPatrolPoint = navGuardPoints[currentPatrolIndex];//Set next guard point
            GuardNav.SetDestination(CurrentPatrolPoint.position);//Make npc move to it
            while (Vector3.Distance(transform.position, CurrentPatrolPoint.position) >= 0.8f)//Check if have not reached, continue moving
            {
                yield return null;
            }
            //Debug.Log("Next Point!");
            currentPatrolIndex = (currentPatrolIndex + 1) % navGuardPoints.Length;//Upon reaching, set next Guard point, code causes points to wrap around
            yield return StartCoroutine(SwitchState("Idle"));
        }
    }
    /// <summary>
    /// Make NPC look at player
    /// </summary>
    public void LookAtPlayer()
    {
        if (mAnimation != null)
        {
            mAnimation.SetBool("Patrol", false);//Stop walking animation
        }
        StopAllCoroutines();//Stop npc from trying to patrol again
        transform.LookAt(PlayerPosition.position); //Point npc at player
        GuardNav.SetDestination(transform.position); //Stop npc from moving from position
        GuardConvoNo = Random.Range(0, 4); //Randomise convo
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
    /// <summary>
    /// Restart patrol coroutine
    /// </summary>
    public void ResumePatrol()
    {
        //Debug.Log($"ResumePatrol called; navGuardPoints length = {navGuardPoints?.Length}");
        StartCoroutine(Patrolling());
    }
}
