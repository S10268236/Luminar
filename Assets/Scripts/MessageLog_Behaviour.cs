using TMPro;
using UnityEngine;

public class MessageLog_Behaviour : MonoBehaviour
{
    public TextMeshProUGUI ToolName;
    public void InvestigateToolName()
    {
        ToolName.text = "Investigation Tool";
    }
}
