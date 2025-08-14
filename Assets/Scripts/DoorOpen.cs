using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Animation doorLeft;
    public Animation doorRight;

    public string leftOpenAnim;
    public string rightOpenAnim;

    public string leftCloseAnim;
    public string rightCloseAnim;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorLeft.Play(leftOpenAnim);
            doorRight.Play(rightOpenAnim);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorLeft.Play(leftCloseAnim);
            doorRight.Play(rightCloseAnim);
        }
    }
}
