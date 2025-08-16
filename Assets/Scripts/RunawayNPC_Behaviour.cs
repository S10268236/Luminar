using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using TMPro;

public class RunawayNPC_Behaviour : MonoBehaviour
{
    //Float for random angle magnitude
    private float RandomAngle;
    //Float for angle direction
    private int RandomDirection;
    //duration of rotation
    public float rotateDuration = 0.2f;
    //Bool to check whether currently rotating
    private bool isRotating = false;
    //Set position to run to
    [SerializeField]
    Transform HidingSpot;
    //Track whether has run
    private bool hiding = false;
    //Set Navmeshagent
    NavMeshAgent RunNav;
    //Store Player position to look at
    [SerializeField]
    Transform PlayerPosition;
    //Control Animations
    private Animator mAnimation;
    //Activate/Deactivate message window
    public GameObject RunConvoPanel;
    //Change Text
    public TextMeshProUGUI RunConvo;
    /// <summary>
    /// assign the variable mAnimation to access the animator amd Get NavMeshAgent
    /// </summary>
    void Awake()
    {
        RunNav = GetComponent<NavMeshAgent>();
        mAnimation = GetComponent<Animator>();
    }
    /// <summary>
    /// Start Coroutine Idle
    /// </summary>
    void Start()
    {
        StartCoroutine(Idle());
    }
    /// <summary>
    /// Idle coroutine, triggers random turn at the end of every turn
    /// </summary>
    /// <returns></returns>
    IEnumerator Idle()
    {
        yield return new WaitForSeconds(2);
        RandomAngle = Random.Range(45f, 91f);
        RandomDirection = Random.Range(-1, 2);
        StartCoroutine(Turn(RandomAngle * RandomDirection));
    }
    /// <summary>
    /// Coroutine for random turn, uses quaternion for smooth rotation, ensures is not isRotating, starts Idle at the end
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    IEnumerator Turn(float angle)
    {
        isRotating = true;
        Quaternion startRotate = transform.rotation;
        Quaternion endRotate = transform.rotation * Quaternion.Euler(0, angle, 0);
        float elaspedTime = 0f;
        while (elaspedTime < rotateDuration)
        {
            transform.rotation = Quaternion.Slerp(startRotate, endRotate, (elaspedTime / rotateDuration));
            elaspedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = endRotate;
        isRotating = false;
        yield return StartCoroutine(Idle());
    }
    /// <summary>
    /// Make NPC run to hiding spot
    /// </summary>
    /// <returns></returns>
    IEnumerator Runaway()
    {
        RunNav.SetDestination(HidingSpot.position);
        while (Vector3.Distance(transform.position, HidingSpot.position) > 0.8f)
        {
            yield return null;
        }
        if (mAnimation != null)
        {
            mAnimation.SetBool("Run", false);
        }
        StartCoroutine(Idle());
        yield break;
    }
    /// <summary>
    /// Check if player approaches, then trigger Runaway
    /// </summary>
    /// <param name="other"></param>

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hiding)
            {
                StopCoroutine(Idle());
                StartCoroutine(StopNStare());
                hiding = true;
                if (mAnimation != null)
                {
                    Debug.Log("Run");
                    mAnimation.SetBool("Run", true);
                }
                StartCoroutine(Runaway());
            }
        }
    }
    /// <summary>
    /// Make NPC look at player before running
    /// </summary>
    /// <returns></returns>
    IEnumerator StopNStare()
    {
        transform.LookAt(PlayerPosition.position);
        yield return new WaitForSeconds(1f);
    }
    /// <summary>
    /// Interacting window
    /// </summary>
    public void ScamTest()
    {
        StopAllCoroutines();
        transform.LookAt(PlayerPosition.position);
        RunConvoPanel.SetActive(true);
        RunConvo.text = "H-He-Heyy, I've g-g-got a wa-way to earn aloooot of ca-chips, you i-interested?";
    }
}
