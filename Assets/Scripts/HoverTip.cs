using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string TipToShow;
    private float TimeToWait = 0.5f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Hovering");
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Not H");
        StopAllCoroutines();
        HoverManager.OnMouseLoseFocus();
    }
    private void ShowMessage()
    {
        HoverManager.OnMouseHover(TipToShow, Input.mousePosition);
    }
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(TimeToWait);
        ShowMessage();
    }
}
