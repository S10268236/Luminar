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
    public int PointWeight;
    //Points recieved
    public TextMeshProUGUI PointResult;
    //Max Points
    private int Maximum = 6;
    public TextMeshProUGUI MaxPoints;
    //Show Results Screen
    [SerializeField]
    GameObject ResultsScreen;
    //Determine Verdict
    public TextMeshProUGUI Verdict;

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
    void Start()
    {
        ResultsScreen.SetActive(false);
    }
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
    public void Results()
    {
        if (Source.isOn)
        {
            GamePoints.AddPoints(PointWeight);
            //Debug.Log("Source" + GamePoints.chips);
        }
        if (SGError.isOn)
        {
            GamePoints.AddPoints(PointWeight);
            //Debug.Log("Error" + GamePoints.chips);
        }
        if (SusLink.isOn)
        {
            GamePoints.AddPoints(PointWeight);
            //Debug.Log("SusLink" + GamePoints.chips);
        }
        if (!InfoRequest.isOn)
        {
            GamePoints.AddPoints(PointWeight);
            //Debug.Log("Info" + GamePoints.chips);
        }
        if (!TGTBT.isOn)
        {
            GamePoints.AddPoints(PointWeight);
            //Debug.Log("TGT" + GamePoints.chips);
        }
        if (!Pressure.isOn)
        {
            GamePoints.AddPoints(PointWeight);
            //Debug.Log("Pressure" + GamePoints.chips);
        }
        PointResult.text = GamePoints.chips.ToString();
        MaxPoints.text = Maximum.ToString();
        //Debug.Log(GamePoints.chips);
    }
    public void WrongVerdict()
    {
        Verdict.text = "FAIL";
        Results();
        ResultsScreen.SetActive(true);
    }
    public void CorrectVerdict()
    {
        Verdict.text = "SUCCESS";
        Results();
        ResultsScreen.SetActive(true);
    }
}
