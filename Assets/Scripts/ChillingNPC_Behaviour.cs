using System.Collections;
using UnityEngine;

public class ChillingNPC_Behaviour : MonoBehaviour
{
    //Float for random angle
    private float RandomAngle;
    public float rotateDuration = 0.5f;
    private bool isRotating = false;
    //Track time 
    public float TimetillD = 0f;
    //Activate Time tracker
    private bool StartTimer = false;
    //Track death
    private bool isDead = false;
    //Activate animation
    //Access animator
    private Animator mAnimation;
    void Start()
    {
        StartCoroutine(Idle());
        mAnimation = GetComponent<Animator>();
    }
    void Update()
    {
        if (StartTimer)
        {
            TimetillD += Time.deltaTime;
            if (TimetillD >= 3f)
            {
                Debug.Log("Ded");
                mAnimation.SetTrigger("Death");
                StopAllCoroutines(); 
            }
        }
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
            Debug.Log("Timer started");
            StartTimer = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimetillD = 0;
        }
    }
}
