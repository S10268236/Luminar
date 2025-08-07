using UnityEngine;

public class GameManager : MonoBehaviour
{
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
}
