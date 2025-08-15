using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    Animator DoorAnim;

    void Start()
    {
        DoorAnim = transform.parent.GetComponent<Animator>();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorAnim.SetBool("isOpening", true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DoorAnim.SetBool("isOpening", false);
        }
    }
}
