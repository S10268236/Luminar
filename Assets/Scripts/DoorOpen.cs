using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    //Access animator
    Animator DoorAnim;

    /// <summary>
    /// Assign variable to the access Animator component
    /// </summary>
    void Start()
    {
        DoorAnim = transform.parent.GetComponent<Animator>();

    }
    /// <summary>
    /// When player enters triggerzone, set the bool to activate door open animation
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorAnim.SetBool("isOpening", true);
        }
    }
    /// <summary>
    /// When player leaves triggerzone, set the bool to activate door close animation
    /// </summary>
    /// <param name="other"></param>

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorAnim.SetBool("isOpening", false);
        }
    }
}
