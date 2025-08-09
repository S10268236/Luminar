using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int chips;
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
    }
    public void AddPoints(int points)
    {
        chips += points;
    }
}
