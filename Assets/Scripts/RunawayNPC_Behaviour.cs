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
    public float rotateDuration = 0.2f;
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
    void Awake()
    {
        RunNav = GetComponent<NavMeshAgent>();
        mAnimation = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(Idle());
    }
    IEnumerator Idle()
    {
        yield return new WaitForSeconds(2);
        RandomAngle = Random.Range(45f, 91f);
        RandomDirection = Random.Range(-1, 2);
        StartCoroutine(Turn(RandomAngle * RandomDirection));
    }
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
    IEnumerator StopNStare()
    {
        transform.LookAt(PlayerPosition.position);
        yield return new WaitForSeconds(1f);
    }
    public void ScamTest()
    {
        StopAllCoroutines();
        transform.LookAt(PlayerPosition.position);
        RunConvoPanel.SetActive(true);
        RunConvo.text = "H-He-Heyy, I've g-g-got a wa-way to earn aloooot of ca-chips, you i-interested?";
    }
}
