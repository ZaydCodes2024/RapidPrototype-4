using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement; 
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    [SerializeField] private float gamePlayingtimer = 60f;
    [SerializeField] private TextMeshProUGUI timerText;
    private float waitingToStartTimer = 1f;
    private float remainingTime;
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
                
                gamePlayingtimer -= Time.deltaTime;

                if (gamePlayingtimer <= 0)
                {
                    Destroy(timerText);
                    state = State.GameOver;
                }
                
                remainingTime = gamePlayingtimer;
                float minutes = Mathf.FloorToInt(gamePlayingtimer / 60);
                float seconds = Mathf.FloorToInt(gamePlayingtimer % 60);

                timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

                break;

            case State.GameOver:
                CompleteGame();
                break;
        }
    }

    public float GetRemainingTimer()
    {
        return remainingTime;
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
