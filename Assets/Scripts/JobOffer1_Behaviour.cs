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
    public GameManager GamePoints;
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
    //Determine whether to add points
    private bool correctChoice = false;
    void Start()
    {
        ResultsScreen.SetActive(false);
        JO1Points = 0;
        JO1current = 0;
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
    private void TrackHighScore()
    {
        JO1current += PointWeight;

    }
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
        PointResult.text = JO1current.ToString();
        MaxPoints.text = Maximum.ToString();
        if (JO1current > JO1Points && correctChoice)
        {
            BetterScore = JO1current - JO1Points;
            JO1Points = JO1current;
            GamePoints.AddPoints(BetterScore);
        }
        JO1current = 0;
        Debug.Log(JO1Points);
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
