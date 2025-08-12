using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    //Access animator
    private Animator mAnimation;
    //Activate/Deactivate message window
    public GameObject ChillConvoPanel;
    //Activate/Deactivate message button;
    public GameObject DeactivateNeg;
    //Activate/Deactivate message button;
    public GameObject DeactivatePos;
    //Change Text
    public TextMeshProUGUI ChillConvo;
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
                isDead = true;
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
    public void WaitTillDeathTalk()
    {
        ChillConvoPanel.SetActive(true);
        if (!isDead)
        {
            DeactivateNeg.SetActive(false);
            DeactivatePos.SetActive(false);
            ChillConvo.text = "Oh my...";
        }
        else
        {
            DeactivateNeg.SetActive(true);
            DeactivatePos.SetActive(true);
            ChillConvo.text = "Hi, could you move me away from the smell please? Oh... The game wont let you? \nWell I guess you can help scan these messages then.\nThe sooner you finish this game, the sooner my suffering ends.";
        }
    }
}
