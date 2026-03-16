using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private TextMeshProUGUI completionTimeText;
    private float completionTime;
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true; 

        restartButton.onClick.AddListener( () =>
        {
            Loader.Load(Loader.Scene.GameScene);
        });

        mainMenuButton.onClick.AddListener( () =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });

        completionTime = GameManager.Instance.GetRemainingTimer();
        float minutes = Mathf.FloorToInt(completionTime / 60);
        float seconds = Mathf.FloorToInt(completionTime % 60);

        completionTimeText.text = string.Format("Completion Time: " + "{0:00}:{1:00}", minutes, seconds);
    }
}
