using System;
using TMPro;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    //Input for Hovering tip text
    public TextMeshProUGUI TipText;
    //Input for Window
    public RectTransform tipWindow;
    //Input What to do when OnMouseHover
    public static Action<string, Vector2> OnMouseHover;
    //Input what happens when mouse loses focus
    public static Action OnMouseLoseFocus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideTip();//Hide tip window on every game start
    }
    /// <summary>
    /// Function to trigger tip
    /// </summary>
    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }
    /// <summary>
    /// Function to disable tip
    /// </summary>
    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }
    /// <summary>
    /// Show Tip function
    /// </summary>
    /// <param name="tip"></param>
    /// <param name="mousePos"></param>
    private void ShowTip(string tip, Vector2 mousePos)
    {
        //Set the tool tip text to what is given
        TipText.text = tip;
        //Ensure the tool tip size changes with length and height of given text but has a maximum width of 200px.
        tipWindow.sizeDelta = new Vector2(TipText.preferredWidth > 200 ? 200 : TipText.preferredWidth, TipText.preferredHeight);
        //Show the tooltip
        tipWindow.gameObject.SetActive(true);
        //Set Tool tip to hover on the right side of the mouse position and not cover the mouse
        tipWindow.transform.position = new Vector2(mousePos.x + 300, mousePos.y);
    }
    /// <summary>
    /// Hide Tip function
    /// </summary>
    private void HideTip()
    {
        TipText.text = default;//Sets text to nothing
        tipWindow.gameObject.SetActive(false);//Deactivate the tip window
    }
}
