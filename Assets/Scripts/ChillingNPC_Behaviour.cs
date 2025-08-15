using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChillingNPC_Behaviour : MonoBehaviour
{
    //Float for random angle npc will turn
    private float RandomAngle;
    //Float for time it takes to rotate
    public float rotateDuration = 0.5f;
    //Bool to check whether currently rotating
    private bool isRotating = false;
    //Track time for animation trigger
    public float TimetillD = 0f;
    //Activate Time tracker
    private bool StartTimer = false;
    //Track whether dead
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
    /// <summary>
    /// Begin Corouting and assign the variable mAnimation to access the animator
    /// </summary>
    void Start()
    {
        StartCoroutine(Idle());
        mAnimation = GetComponent<Animator>();
    }
    /// <summary>
    /// Track how long player is within trigger zone, trigger death animation when over time
    /// </summary>
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
    /// <summary>
    /// Idle coroutine, triggers random turn at the end of every turn
    /// </summary>
    /// <returns></returns>
    IEnumerator Idle()
    {
        yield return new WaitForSeconds(2);
        RandomAngle = Random.Range(-91f, 91f);
        StartCoroutine(Turn(RandomAngle));
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
    /// Checks if collider hits player, then begins timer
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Timer started");
            StartTimer = true;
        }
    }
    /// <summary>
    /// Resets timer if player leaves
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            TimetillD = 0;
        }
    }
    /// <summary>
    /// Checks whether NPC animation death has played and changes dialogue if has or hasnt
    /// </summary>
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
