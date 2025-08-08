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
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    //Trigger for whether NPC returns to patrol or is pacified and returns to original position
    public bool QuestSolved = false;
    //Access Player_behaviour

    public Player_Behaviour PlayerObject;
    //Access Interact Message for enabling and disabling
    public GameObject InteractMessage;
    [SerializeField]
    Transform PlayerPosition;
    //Checks if player is within collider
    private bool FinishedConvo = false;
    //Checks if already interacted with-to prevent repeat code
    public bool hasInteracted = false;
    //Access Convo Panel
    [SerializeField]
    GameObject ConvoPanel;
    //Store this approaching NPC's position
    public Vector3 NPCPosition;
    


    void Awake()
    {
        NavPoint = GetComponent<NavMeshAgent>();
        //Assign player behaviour script for access
    }
    void Start()
    {
        StartCoroutine(currentState);
    }
    public void LookAtPlayer()
    {
        StopAllCoroutines();
        transform.LookAt(PlayerPosition.position);
        NavPoint.SetDestination(transform.position);
        hasInteracted = true;
        QuestSolved = true;
        //Lock Player Position and camera
        PlayerObject.SetMoveCamState(false);
        //Enable cursor
        PlayerObject.SetCursorState(true);
        //Display Conversation window
        PlayerObject.StartConvo();
        //Disable Interaction message 
        InteractMessage.SetActive(false);
    }
    IEnumerator Idle()
    {
        //Debug.Log("Going Idle");
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
        //Debug.Log("Chasing!");
        while (currentState == "ApproachPlayer")
        {
            //Set destination to players position
            yield return null;
            NavPoint.SetDestination(Target.position);
            //Stop chasing when at this distance
            if (Vector3.Distance(transform.position, Target.position) <= 3f && !hasInteracted)
            {
                //Set Variable's position to this NPC's position
                // NPCPosition = transform.position;
                // NPCPosition.y = transform.position.y;
                // //NPCPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

                // //Make Player look at NPC
                // PlayerObject.TurnToNPC(NPCPosition);
                hasInteracted = true;
                QuestSolved = true;
                NavPoint.SetDestination(transform.position);
                //Lock Player Position and camera
                PlayerObject.SetMoveCamState(false);
                //Enable cursor
                PlayerObject.SetCursorState(true);
                //Display Conversation window
                PlayerObject.StartConvo();
                //Disable Interaction message 
                InteractMessage.SetActive(false);
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
            Transform CurrentPatrolPoint = patrolPoints[currentPatrolIndex];
            NavPoint.SetDestination(CurrentPatrolPoint.position);
            while (Vector3.Distance(transform.position, CurrentPatrolPoint.position) >= 0.8f)
            {
                if (Target != null)
                {
                    StartCoroutine(SwitchState("ApproachPlayer"));
                    yield break;
                }
                yield return null;
            }
            //Debug.Log("Next Point!");
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            yield return StartCoroutine(SwitchState("Idle"));
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("PLAYER!!");
            Target = other.transform;
        }
    }
    void OnTriggerExit(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            Target = null;
            FinishedConvo = true;
            Debug.Log("FinishedConvo is true");
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
    IEnumerator Pacified()
    {
        if (FinishedConvo)
        {
            NavPoint.SetDestination(patrolPoints[0].position);
            yield return null;
        }
        else
        {
            yield return null;
        }
    }
}
