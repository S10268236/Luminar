using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JobOffer1_Behaviour : MonoBehaviour
{
    //Check whether tools are being used
    public bool InvestigationOn = false;
    //Show Tool used
    public TextMeshProUGUI ToolName;
    //Access game points within Game Manager
    public GameManager GamePoints;
    //Int to determine how much points with each correct answer
    public int PointWeight;
    //Track Highest Point recieved storage
    public int JO1Points;
    //Store current score
    private int JO1current;
    //Increased score to add to General score
    public int BetterScore;
    //Points recieved text
    public TextMeshProUGUI PointResult;
    //Max Points
    private int Maximum = 6;
    //For change of text of max points
    public TextMeshProUGUI MaxPoints;
    //Show Results Screen
    [SerializeField]
    GameObject ResultsScreen;
    //Determine Verdict
    public TextMeshProUGUI Verdict;
    //Text input for result of Tool
    [SerializeField]
    TextMeshProUGUI ToolResult;
    //Following 6 are Checkbox input for indicators
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
        JO1Points = 0;
        JO1current = 0;
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
            ToolResult.text = "This person is not in your contacts";
        }
    }
    /// <summary>
    /// Text change for URL and unfortunate clicking
    /// </summary>
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
    /// <summary>
    /// Quits game after period
    /// </summary>
    /// <returns></returns>
    IEnumerator CrashGame()
    {
        ToolResult.text = "You have failed the tutorial due to the link hacking into the game, Goodbye.";
        yield return new WaitForSeconds(3);
        Application.Quit();
    }
    /// <summary>
    /// Add point weight to current points
    /// </summary>
    private void TrackHighScore()
    {
        JO1current += PointWeight;
    }
    /// <summary>
    /// Tally results of judgement
    /// </summary>
    public void Results()
    {
        if (Source.isOn)
        {
            TrackHighScore();
            //Debug.Log("Source" + JO1current);
        }
        if (SGError.isOn)
        {
            TrackHighScore();
            //Debug.Log("Error" + JO1current);
        }
        if (!InfoRequest.isOn)
        {
            TrackHighScore();
            //Debug.Log("Info" + JO1current);
        }
        if (!TGTBT.isOn)
        {
            TrackHighScore();
            //Debug.Log("TGT" + JO1current);
        }
        if (!Pressure.isOn)
        {
            TrackHighScore();
            //Debug.Log("Pressure" + JO1current);
        }
        if (SusLink.isOn)
        {
            TrackHighScore();
            //Debug.Log("SusLink" + JO1current);
        }
        PointResult.text = JO1current.ToString();//Display points gotten
        MaxPoints.text = Maximum.ToString();//Show max points
        if (JO1current > JO1Points && correctChoice)//For allowing repeat plays
        {
            BetterScore = JO1current - JO1Points;
            JO1Points = JO1current;
            GamePoints.AddPoints(BetterScore);
        }
        JO1current = 0;//Reset points
        //Debug.Log(JO1Points);
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
