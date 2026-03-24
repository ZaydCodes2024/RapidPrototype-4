using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}
    [SerializeField] private float countdowntimer;
    [SerializeField] private PuzzleModule[] puzzleModules;
    [SerializeField] private TextMeshProUGUI timerText; 
    private int solvedPuzzles;
    private float gamePlayingtimer;
    private float waitingToStartTimer = 1f;
    private float gameOverTimer = 3f;
    private float completionTime;
    private bool isGameWin;
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
    private void Start()
    {
        foreach (PuzzleModule puzzle in puzzleModules)
        {
            puzzle.OnSolved += CheckCompletion;
        }
    }
    private void CheckCompletion(object sender, EventArgs e)
    {
        solvedPuzzles++;

        if (solvedPuzzles >= puzzleModules.Length)
        {
            state = State.GameOver;
            isGameWin = true;
        }
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
                CompleteGame();
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
    public bool IsGameWin()
    {
        return isGameWin;
    }
    private void CompleteGame()
    {
        gameOverTimer -= Time.deltaTime;

        if (gameOverTimer <= 0)     Loader.Load(Loader.Scene.GameOverScene);
        
    }
}
