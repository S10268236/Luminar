using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Store Chip score
    public int chips;
    //Display score on screen with this object
    public TextMeshProUGUI ChipScore;
    public static GameManager instance;
    //Track every Quest's completion
    private JobOffer1_Behaviour Job1Complete;
    private JobOffer2_Behaviour Job2Complete;
    private Parcel1_Behaviour ParcelComplete;
    private LuggageBag_Behaviour LuggageComplete;
    private Phishing_Behaviour PhishingComplete;
    //Game Window for End game
    public GameObject GameCompleteWindow;
    //End Game Score Successes
    public TextMeshProUGUI SuccessesScore;
    //End Game Score Fails
    public TextMeshProUGUI FailsScore;
    //EndGame Chip text
    public TextMeshProUGUI ChipNum;
    //End Game Score Verdict
    public TextMeshProUGUI VerdictText;
    //Number to track success and failures
    public int SucFail = 0;
    void Awake()
    {
        instance = this;
        Job1Complete = GetComponent<JobOffer1_Behaviour>();
        Job2Complete = GetComponent<JobOffer2_Behaviour>();
        ParcelComplete = GetComponent<Parcel1_Behaviour>();
        LuggageComplete = GetComponent<LuggageBag_Behaviour>();
        PhishingComplete = GetComponent<Phishing_Behaviour>();
    }
    void Update()
    {
        if (Job1Complete != null)
        {

            if (Job1Complete.questSolved && Job2Complete.questSolved && ParcelComplete.questSolved && LuggageComplete.questSolved && PhishingComplete.questSolved)
            {
                Job1Complete.questSolved = false;
                Results();
            }
        }
    }
    void Start()
    {
        if (ChipScore != null)
        {
            chips = 0;
            ChipScore.text = chips.ToString();
        }
    }
    public void AddPoints(int points)
    {
        chips += points;
        //Debug.Log("Chip Score: " + chips);
        ChipScore.text = chips.ToString();
    }
    public void TallySucFail()
    {
        if (Job1Complete.succeded)
        {
            SucFail++;
        }
        if (Job2Complete.succeded)
        {
            SucFail++;
        }
        if (ParcelComplete.succeded)
        {
            SucFail++;
        }
        if (LuggageComplete.succeded)
        {
            SucFail++;
        }
        if (PhishingComplete.succeded)
        {
            SucFail++;
        }
    }
    public void Results()
    {
        TallySucFail();
        GameCompleteWindow.SetActive(true);
        SuccessesScore.text = SucFail.ToString();
        FailsScore.text = (5 - SucFail).ToString();
        ChipNum.text = chips.ToString();
        if (SucFail <= 2)
        {
            VerdictText.text = "Congratulations! You get to play this game again to train your scam awareness!";
        }
        else if (SucFail <= 4)
        {
            VerdictText.text = "Congratulations! You are Scam Aware! Play again for a perfect score and a chance to win an achievement card!";
        }
        else if (SucFail >= 5)
        {
            VerdictText.text = "Congratulations! You are Scam Resistant! Show this screen to a facilitator for a lucky draw!";
        }
    }
}
