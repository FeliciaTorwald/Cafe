using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    [SerializeField] private float timer;
    [SerializeField] private TMP_Text countDownTimer;
    [SerializeField] public float goldCounter;
    
    //Canvas elements references
    [SerializeField] private GameObject difficultySelectionUI;
    [SerializeField] private GameObject mainGameUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject instructionsUI;
    [SerializeField] private GameObject instructionsUI2;
    [SerializeField] private GameObject endGameUI;
    [SerializeField] private TMP_Text endGameStatusText;
    [SerializeField] private TMP_Text endGameGuestsServedCounter;
    [SerializeField] private TMP_Text endGameTimeCounter;

    private bool showingInstructions;
    private bool gameStarted;
    private bool gamePaused;
    
    void Start()
    {
        StartCoroutine(InitializeGame());
    }
    
    void Update()
    {
        if (showingInstructions)
            if (Input.GetKeyDown(KeyCode.Space))
                StartGame();

        if (gameStarted)
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause();
    }
    
    public void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                GameManager.Instance.difficulty = Difficulty.Easy;
                break;
            case 1:
                GameManager.Instance.difficulty = Difficulty.Normal;
                break;
            case 2:
                GameManager.Instance.difficulty = Difficulty.Hard;
                break;
            default:
                Debug.Log("Something has gone wrong, check difficulty toggle.");
                break;
        }
        
        difficultySelectionUI.SetActive(false);
        ShowInstructions();
    }
    

    IEnumerator InitializeGame()
    {
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
        difficultySelectionUI.SetActive(true);
    }
    
    private void ShowInstructions()
    {
        instructionsUI.SetActive(true);
        showingInstructions = !showingInstructions;
        
    }

    private void StartGame()
    {
        instructionsUI.SetActive(false);
        showingInstructions = !showingInstructions;
        Time.timeScale = 1f;
        mainGameUI.SetActive(true);
        gameStarted = !gameStarted;
    }
    
    public void TogglePause()
    {
        if (!gamePaused)
        {
            Time.timeScale = 0f;
            gamePaused = !gamePaused;
            mainGameUI.SetActive(false);
            pauseMenuUI.SetActive(true);
            instructionsUI2.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            gamePaused = !gamePaused;
            mainGameUI.SetActive(true);
            pauseMenuUI.SetActive(false);
            instructionsUI2.SetActive(false);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    
    private void DayTimerCountDown()
    {
        
    }

    public void ShowEndGameUI(bool win, int servedGuests, float time)
    {
        if (win)
        {
            Time.timeScale = 0f;
            gameStarted = !gameStarted;
            mainGameUI.SetActive(false);
            endGameUI.SetActive(true);
            endGameStatusText.SetText("You win!");
            endGameGuestsServedCounter.SetText(servedGuests.ToString());
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            endGameTimeCounter.SetText("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
        else
        {
            Time.timeScale = 0f;
            gameStarted = !gameStarted;
            mainGameUI.SetActive(false);
            endGameUI.SetActive(true);
            endGameStatusText.SetText("Game over");
            endGameStatusText.color = new Color(.85f, .5f, .5f, 1f);
            endGameGuestsServedCounter.SetText(servedGuests.ToString());
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            endGameTimeCounter.SetText("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
        }
    }
}
