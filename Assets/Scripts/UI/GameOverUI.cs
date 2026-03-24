using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI completionTimeText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    private float completionTime;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 
        
        mainMenuButton.onClick.AddListener( () =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        completionTime = GameManager.Instance.GetCompletionTime();


        float minutes = Mathf.FloorToInt(completionTime / 60);
        float seconds = Mathf.FloorToInt(completionTime % 60);

        completionTimeText.text = string.Format("Completion Time: " + "{0:00}:{1:00}", minutes, seconds);

         if (GameManager.Instance.IsGameWin())
        {
            gameOverText.text = "You Won!";
            Show();
        }
        else
        {
            gameOverText.text = "Game Over!";
            Hide();
        }
    }
    private void Show()
    {
        completionTimeText.gameObject.SetActive(true);
    }
    private void Hide()
    {
        completionTimeText.gameObject.SetActive(false);
    }
}
