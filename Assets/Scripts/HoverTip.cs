using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //Input text for tip
    public string TipToShow;
    //Float to control how long of a lag before tip shows
    private float TimeToWait = 0.5f;
    /// <summary>
    /// Listener for when pointer enters
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Hovering");
        StopAllCoroutines();
        StartCoroutine(StartTimer());
    }
    /// <summary>
    /// Listener for when pointer exits
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("Not H");
        StopAllCoroutines();
        HoverManager.OnMouseLoseFocus();
    }
    /// <summary>
    /// OnMouseHover, access HoverManager to show tip at position
    /// </summary>
    private void ShowMessage()
    {
        HoverManager.OnMouseHover(TipToShow, Input.mousePosition);
    }
    /// <summary>
    /// Function for Lag time before tip is shown
    /// </summary>
    /// <returns></returns>
    private IEnumerator StartTimer()
    {
        yield return new WaitForSeconds(TimeToWait);
        ShowMessage();
    }
}
