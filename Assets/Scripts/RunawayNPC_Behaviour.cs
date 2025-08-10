using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class RunawayNPC_Behaviour : MonoBehaviour
{
    //Float for random angle
    private float RandomAngle;
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
    void Awake()
    {
        RunNav = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        StartCoroutine(Idle());
    }
    IEnumerator Idle()
    {
        yield return new WaitForSeconds(2);
        RandomAngle = Random.Range(-91f, 91f);
        StartCoroutine(Turn(RandomAngle));
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!hiding)
            {
                StopAllCoroutines();
                StartCoroutine(StopNStare());
                hiding = true;
                Debug.Log("RUN");
                RunNav.SetDestination(HidingSpot.position);
                StartCoroutine(Idle());
            }
        }
    }
    IEnumerator StopNStare()
    {
        transform.LookAt(PlayerPosition.position);
        yield return new WaitForSeconds(1f);
    }
}
