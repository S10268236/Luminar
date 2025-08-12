using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Store Chip score
    public int chips;
    //Display score on screen with this object
    public TextMeshProUGUI ChipScore;
    public static GameManager instance;
    void Awake()
    {
        instance = this;
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
}
