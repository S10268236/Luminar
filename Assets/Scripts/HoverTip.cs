using UnityEngine;
using UnityEngine.EventSystems;
public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Hovered!");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Not Hovered!");
    }
}
