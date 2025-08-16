using TMPro;
using UnityEngine;

public class MessageLog_Behaviour : MonoBehaviour
{
    //Text input for tool name
    public TextMeshProUGUI ToolName;
    /// <summary>
    /// Set tool name text
    /// </summary>
    public void InvestigateToolName()
    {
        ToolName.text = "Investigation Tool";
    }
}
