using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WorriedNPC_Behaviour : MonoBehaviour
{
    //Set Navmesh vaiable
    NavMeshAgent NavPoint;
    //Input for Player target
    [SerializeField]
    Transform Target;
    //Display current state
    [SerializeField]
    string currentState = "Idle";
    //Array for patrol points
    public Transform[] patrolPoints;
    //Track which point is being patrolled currently
    private int currentPatrolIndex = 0;
    //Trigger for whether NPC returns to patrol or is pacified and returns to original position
    public bool QuestSolved = false;
    //Access Player_behaviour

    public Player_Behaviour PlayerObject;
    //Access Interact Message for enabling and disabling
    public GameObject InteractMessage;
    //Input for Player position
    [SerializeField]
    Transform PlayerPosition;
    //Checks if player is within collider
    private bool FinishedConvo = false;
    //Checks if already interacted with-to prevent repeat code
    public bool hasInteracted = false;
    //Access Convo Panel
    [SerializeField]
    GameObject ConvoPanel;
    //Change convo
    public TextMeshProUGUI Conversation;
    
    //Store this approaching NPC's position
    public Vector3 NPCPosition;
    //Control Animations
    private Animator mAnimation;

    /// <summary>
    /// Set navmeshagent and animator
    /// </summary>
    void Awake()
    {
        NavPoint = GetComponent<NavMeshAgent>();
        //Set animator
        mAnimation = GetComponent<Animator>();

    }
    /// <summary>
    /// Begin coroutine
    /// </summary>
    void Start()
    {
        StartCoroutine(currentState);
    }
    /// <summary>
    /// Make npc turn to face player
    /// </summary>
    public void LookAtPlayer()
    {
        if (mAnimation != null)
        {
            mAnimation.SetBool("Walk", false);
        }
        StopAllCoroutines();
        transform.LookAt(PlayerPosition.position);
        NavPoint.SetDestination(transform.position);
        hasInteracted = true;
        QuestSolved = true;
        //Display Conversation window
        PlayerObject.StartConvo();
    }
    /// <summary>
    /// Trigger idle animation and idle phase
    /// </summary>
    /// <returns></returns>
    IEnumerator Idle()
    {
        //Debug.Log("Going Idle");
        while (currentState == "Idle")
        {
            if (mAnimation != null)
            {
                //Debug.Log("Idle");
                mAnimation.SetBool("Walk", false);
            }
            if (Target != null)
            {
                StartCoroutine(SwitchState("ApproachPlayer"));
            }
            yield return new WaitForSeconds(1);
            StartCoroutine(SwitchState("Patrolling"));
        }
    }
    /// <summary>
    /// If player in triggerzone, approach and activate walk animation
    /// </summary>
    /// <returns></returns>
    IEnumerator ApproachPlayer()
    {
        //Debug.Log("Chasing!");
        while (currentState == "ApproachPlayer")
        {
            //Set destination to players position
            yield return null;
            NavPoint.SetDestination(Target.position);
            //Stop chasing when at this distance
            if (mAnimation != null)
            {
                mAnimation.SetBool("Walk", true);
            }
            if (Vector3.Distance(transform.position, Target.position) <= 3f && !hasInteracted)//if distance to player less than float and has not interacted
            {
                if (mAnimation != null)
                {
                    mAnimation.SetBool("Walk", false);
                }
                hasInteracted = true;
                QuestSolved = true;
                NavPoint.SetDestination(transform.position);
                //Display Conversation window
                PlayerObject.StartConvo();
                StopAllCoroutines();
                yield break;
            }
            else if (Target == null)
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
    /// <summary>
    /// Switch between states
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
        StartCoroutine(currentState);
    }
    /// <summary>
    /// Activate walk animation and Patrol state
    /// </summary>
    /// <returns></returns>
    IEnumerator Patrolling()
    {
        //Debug.Log("Starting Patrol");
        while (currentState == "Patrolling")
        {
            if (mAnimation != null)
            {
                //Debug.Log("Walkk");
                mAnimation.SetBool("Walk",true);
            }
            Transform CurrentPatrolPoint = patrolPoints[currentPatrolIndex];
            NavPoint.SetDestination(CurrentPatrolPoint.position);
            while (Vector3.Distance(transform.position, CurrentPatrolPoint.position) >= 1f)//if distance to current patrol point greater than float, keep going
            {
                if (Target != null)
                {
                    StartCoroutine(SwitchState("ApproachPlayer"));
                    yield break;
                }
                yield return null;
            }
            //Debug.Log("Next Point!");
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;//Make patrol point wrap around back to start
            yield return StartCoroutine(SwitchState("Idle"));
        }
    }
    /// <summary>
    /// Set target to Player
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("PLAYER!!");
            Target = other.transform;
        }
    }
    /// <summary>
    /// Reset target and track whther has interacted
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Target = null;
            FinishedConvo = true;
            //Debug.Log("FinishedConvo is true");
            if (!QuestSolved)
            {
                StartCoroutine(SwitchState("Idle"));
            }
            else
            {
                StartCoroutine(Pacified());
            }
        }
    }
    /// <summary>
    /// Return NPC to initial position
    /// </summary>
    /// <returns></returns>
    IEnumerator Pacified()
    {
        if (FinishedConvo)
        {
            NavPoint.SetDestination(patrolPoints[0].position);
            while (Vector3.Distance(transform.position, patrolPoints[0].position) >= 0.8f)
            {
                if (mAnimation != null)
                {
                    mAnimation.SetBool("Walk", true);
                }
                yield return null;
            }
            mAnimation.SetBool("Walk", false);
            yield return StartCoroutine(Idle());
        }
        else
        {
            yield return null;
        }
    }
}
