using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    [SerializeField] private float countdowntimer;
    private float gamePlayingtimer;
    [SerializeField] private TextMeshProUGUI timerText;
    private float waitingToStartTimer = 1f;
    private float gameOverTimer = 3f;
    private float completionTime;
    private enum State
    {
        WaitingToStart, GamePlaying, GameOver
    }
    private State state;
    private void Awake()
    {
        state = State.WaitingToStart;
        Instance = this;
    }
    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;

                if (waitingToStartTimer <= 0)
                {
                    state = State.GamePlaying;
                }
                break;

            case State.GamePlaying:
                
                gamePlayingtimer += Time.deltaTime;
                countdowntimer -= Time.deltaTime;

                if (countdowntimer < 0)
                {
                    Destroy(timerText);
                    state = State.GameOver;
                }
                
                completionTime = gamePlayingtimer;
                float minutes = Mathf.FloorToInt(countdowntimer / 60);
                float seconds = Mathf.FloorToInt(countdowntimer % 60);

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                break;

            case State.GameOver:
                gameOverTimer -= Time.deltaTime;
                
                if(gameOverTimer <= 0)
                {
                    CompleteGame();
                }
                break;
        }
    }

    public float GetCompletionTime()
    {
        return completionTime;
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
    public void CompleteGame()
    {
        Loader.Load(Loader.Scene.GameOverScene);
    }
}
