using System.Collections;
using UnityEngine;

public class ChillingNPC_Behaviour : MonoBehaviour
{
    public float rotateDuration = 0.5f;
    private bool isRotating = false;
    void Start()
    {
        StartCoroutine(Idle());
    }
    IEnumerator Idle()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(Turn(90f));
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
}
