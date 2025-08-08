using System;
using TMPro;
using UnityEngine;

public class HoverManager : MonoBehaviour
{
    public TextMeshProUGUI TipText;
    public RectTransform tipWindow;
    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HideTip();
    }
    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }
    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }
    private void ShowTip(string tip, Vector2 mousePos)
    {
        //Set the tool tip text to what is given
        TipText.text = tip;
        //Ensure the tool tip size changes with length and height of given text but has a maximum width of 200px.
        tipWindow.sizeDelta = new Vector2(TipText.preferredWidth > 200 ? 200 : TipText.preferredWidth, TipText.preferredHeight);
        //Show the tooltip
        tipWindow.gameObject.SetActive(true);
        //Set Tool tip to hover on the right side of the mouse position and not cover the mouse
        tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x * 2, mousePos.y);
    }
    private void HideTip()
    {
        TipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }
}
