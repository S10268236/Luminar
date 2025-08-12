using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class Parcel1_Behaviour : MonoBehaviour
{
    //Check whether tools are being used
    public bool InvestigationOn = false;
    //Show Tool used
    public TextMeshProUGUI ToolName;
    public GameManager GamePoints;
    public int PointWeight = 1;
    //Track Highest Point recieved storage
    public int JO2Points = 0;
    //Store current score
    private int JO2current;
    //Increased score to add to General score
    public int BetterScore;
    //Points recieved text
    public TextMeshProUGUI PointResult;
    //Max Points
    private int Maximum = 5;
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
    Toggle Pressure;
    [SerializeField]
    Toggle SusLink;
    [SerializeField]
    Toggle CompanyIncons;
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
            ToolResult.text = "Message is from SINGPOST";
        }
    }
    public void IDInvestigate()
    {
        if (InvestigationOn)
        {
            ToolResult.text = "Maybe hover over it?";
        }
    }
    public void SalaryInvestigate()
    {
        if (InvestigationOn)
        {
            ToolResult.text = "Authentic SingPost Logo";
        }
    }
    public void GoodEye()
    {
        if (InvestigationOn)
        {
            ToolResult.text = "Somethingss wrongss heress";
        }
    }
    private void TrackHighScore()
    {
        JO2current += PointWeight;
    }
    public void Results()
    {
        if (!Source.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (SGError.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (Pressure.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (SusLink.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (!CompanyIncons.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        Debug.Log(JO2Points);
        PointResult.text = JO2current.ToString();
        MaxPoints.text = Maximum.ToString();
        if (JO2current > JO2Points && correctChoice)
        {
            BetterScore = JO2current - JO2Points;
            JO2Points = JO2current;
            GamePoints.AddPoints(BetterScore);
        }
        JO2current = 0;
        
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
