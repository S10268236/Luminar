using TMPro;
using UnityEngine;

public class MessageLog_Behaviour : MonoBehaviour
{
    public TextMeshProUGUI ToolName;
    public bool InvestigateActive = false;
    public void InvestigateToolName()
    {
        ToolName.text = "Investigation Tool";
    }
    public void EnquiryToolName()
    {
        ToolName.text = "Enquiry Tool";
    }
}
