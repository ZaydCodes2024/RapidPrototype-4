using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CompleteGame()
    {
        Debug.Log("Game Over!");
        // Add game completion logic here (e.g., open the chest, show UI)
    }
}
