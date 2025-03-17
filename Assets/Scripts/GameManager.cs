using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private float timer = 60f;
    private bool isRunning = true;
    [SerializeField] private TextMeshProUGUI timerText;
    private float gameOverDelay = 3f;
    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (isRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = 0;
                isRunning = false;
                CompleteGame();
            }
            float minutes = Mathf.FloorToInt(timer / 60);
            float seconds = Mathf.FloorToInt(timer % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    public void CompleteGame()
    {
        isRunning = false;
        StartCoroutine(GameOver());
    }
    private IEnumerator GameOver()
    {
        yield return new WaitForSeconds(gameOverDelay);
        SceneManager.LoadScene("GameOverScene");
    }
}
