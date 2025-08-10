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
        if (instance != null && instance != this)
        {
            // If it is not, destroy this object
            Destroy(gameObject);
        }
        else
        {
            // If there is no instance, set this object as the instance
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        chips = 0;
        ChipScore.text = chips.ToString();
    }
    public void AddPoints(int points)
    {
        chips += points;
        //Debug.Log("Chip Score: " + chips);
        ChipScore.text = chips.ToString();
    }
}
