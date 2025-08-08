using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JobOffer1_Behaviour : MonoBehaviour
{
    //Check whether tools are being used
    public bool InvestigationOn = false;
    public GameManager GamePoints;
    public int PointWeight = 1;

    [SerializeField]
    TextMeshProUGUI ToolResult;
    [SerializeField]
    Toggle Source;
    [SerializeField]
    Toggle SGError;
    [SerializeField]
    Toggle InfoRequest;
    [SerializeField]
    Toggle TGTBT;
    [SerializeField]
    Toggle Pressure;
    [SerializeField]
    Toggle SusLink;
    public void InvestigationToolOn()
    {
        InvestigationOn = true;
    }
    public void InvestigationToolOff()
    {
        InvestigationOn = false;
    }
    public void SenderInvestigate()
    {
        if (InvestigationOn)
        {
            ToolResult.text = "This person is not in your contacts";
        }
    }
    public void ScamURL()
    {
        if (!InvestigationOn)
        {
            StartCoroutine(CrashGame());
        }
        else if (InvestigationOn)
        {
            ToolResult.text = "Link does not seem to be an official LinkedIn page";
        }
    }
    IEnumerator CrashGame()
    {
        ToolResult.text = "You have failed the tutorial due to the link hacking into the game, Goodbye.";
        yield return new WaitForSeconds(3);
        Application.Quit();
    }
    public void Results(int PointWeight)
    {
        if (Source.isOn)
        {
            GamePoints.AddPoints(PointWeight);
        }
        if (SGError.isOn)
        {
            GamePoints.AddPoints(PointWeight);
        }
        if (SusLink.isOn)
        {
            GamePoints.AddPoints(PointWeight);
        }
    }
}
