using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JobOffer2_Behaviour : MonoBehaviour
{
    //Check whether tools are being used
    public bool InvestigationOn = false;
    //Show Tool used
    public TextMeshProUGUI ToolName;
    public GameManager GamePoints;
    public int PointWeight;
    //Track Highest Point recieved storage
    public int JO2Points = 0;
    //Store current score
    private int JO2current;
    //Increased score to add to General score
    public int BetterScore;
    //Points recieved text
    public TextMeshProUGUI PointResult;
    //Max Points
    private int Maximum = 8;
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
    [SerializeField]
    Toggle CompanyIncons;
    [SerializeField]
    Toggle UpfrontFees;
    //Determine whether to add points
    private bool correctChoice = false;
    void Start()
    {
        ResultsScreen.SetActive(false);
        JO2Points = 0;
        JO2current = 0;
    }
    public void InvestigateToolName()
    {
        ToolName.text = "Investigation Tool";
    }
    public void ClearToolName()
    {
        ToolName.text = default;
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
    public void CompanyNameInvestigate()
    {
        if (InvestigationOn)
        {
            ToolResult.text = "This company exists";
        }
    }
    public void IDInvestigate()
    {
        if (InvestigationOn)
        {
            ToolResult.text = "CompanyID is registered under Randstrandus Pte.Ltd";
        }
    }
    public void SalaryInvestigate()
    {
        if (InvestigationOn)
        {
            ToolResult.text = "Market records show this income to be about 300% above the average income";
        }
    }
    private void TrackHighScore()
    {
        JO2current += PointWeight;
    }
    public void Results()
    {
        if (Source.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (!SGError.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (!InfoRequest.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (TGTBT.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (Pressure.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (!SusLink.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (CompanyIncons.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (!UpfrontFees.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }

        PointResult.text = JO2current.ToString();
        MaxPoints.text = Maximum.ToString();
        if (JO2current > JO2Points && correctChoice)
        {
            BetterScore = JO2current - JO2Points;
            JO2Points = JO2current;
            GamePoints.AddPoints(BetterScore);
        }
        JO2current = 0;
        Debug.Log(JO2Points);
    }
    public void WrongVerdict()
    {
        correctChoice = false;
        Verdict.text = "FAIL";
        Results();
        ResultsScreen.SetActive(true);
    }
    public void CorrectVerdict()
    {
        correctChoice = true;
        Verdict.text = "SUCCESS";
        Results();
        ResultsScreen.SetActive(true);
    }
}
