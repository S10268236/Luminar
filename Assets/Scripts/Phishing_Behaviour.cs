using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Phishing_Behaviour : MonoBehaviour
{
    //Check whether tools are being used
    public bool InvestigationOn = false;
    //Show Tool used
    public TextMeshProUGUI ToolName;
    //Access game points within Game Manager
    public GameManager GamePoints;
    //Int to determine how much points with each correct answer
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
    private int Maximum = 8;
    public TextMeshProUGUI MaxPoints;
    //Show Results Screen
    [SerializeField]
    GameObject ResultsScreen;
    //Determine Verdict
    public TextMeshProUGUI Verdict;
    //Text input for result of Tool
    [SerializeField]
    TextMeshProUGUI ToolResult;
    //Following 8 are Checkbox input for indicators
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
    //Determine whether to end quest
    public bool questSolved = false;
    //Track success or fail
    public bool succeded = true;
    /// <summary>
    /// Disable results screen at start, reset points
    /// </summary>
    void Start()
    {
        ResultsScreen.SetActive(false);
        JO2Points = 0;
        JO2current = 0;
    }
    /// <summary>
    /// Show Tool name
    /// </summary>
    public void InvestigateToolName()
    {
        ToolName.text = "Investigation Tool";
    }
    /// <summary>
    /// Clear Tool name
    /// </summary>
    public void ClearToolName()
    {
        ToolName.text = default;
    }
    /// <summary>
    /// Switch tool bool to On
    /// </summary>
    public void InvestigationToolOn()
    {
        InvestigationOn = true;
    }
    /// <summary>
    /// Switch tool bool to off
    /// </summary>
    public void InvestigationToolOff()
    {
        InvestigationOn = false;
    }
    /// <summary>
    /// Text change for investigating the sender
    /// </summary>
    public void SenderInvestigate()
    {
        if (InvestigationOn)
        {
            ToolResult.text = "ocbcbank.sg";
        }
    }
    /// <summary>
    /// Text change for ID
    /// </summary>
    public void IDInvestigate()
    {
        if (InvestigationOn)
        {
            ToolResult.text = "Maybe hover over it?";
        }
    }
    /// <summary>
    /// Add point weight to current points
    /// </summary>
    private void TrackHighScore()
    {
        JO2current += PointWeight;
    }
    /// <summary>
    /// Tally results of judgement
    /// </summary>
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
        if (InfoRequest.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO2current);
        }
        if (!TGTBT.isOn)
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
    /// <summary>
    /// Show wrong choice verdict, track offer fail
    /// </summary>
    public void WrongVerdict()
    {
        correctChoice = false;
        Verdict.text = "FAIL";
        Results();
        ResultsScreen.SetActive(true);
        questSolved = true;
        succeded = false;
    }
    /// <summary>
    /// Show correct choice verdict, track success
    /// </summary>
    public void CorrectVerdict()
    {
        correctChoice = true;
        Verdict.text = "SUCCESS";
        Results();
        ResultsScreen.SetActive(true);
        questSolved = true;
    }
}
