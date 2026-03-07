using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Interacts with scene management
using UnityEngine.UI;  // Interacts with the Buttons
using TMPro; // Interacts with TextMeshProUGUI

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;
    private float spawnRate = 3.0f;
    private int score;
    private int bestScore;
    private int ID;
    private int lives;
    private bool paused;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    [SerializeField] private Button restartButton;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject pauseScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       gameOverText.gameObject.SetActive(false);
       restartButton.gameObject.SetActive(false);
       pauseScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MainManager.Instance.isGameActive)
        {
            ChangePaused();
        }
    }


    public void StartGame(int difficulty)
    {
        MainManager.Instance.isGameActive = true;
        score = 0;
        ID = difficulty;
        
        if (ID == 1)
            bestScore = MainManager.Instance.bestEasy;
        else if (ID == 2)
            bestScore = MainManager.Instance.bestMedium;
        else
            bestScore = MainManager.Instance.bestHard;

        bestScoreText.text = "High Score: " + bestScore;

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateLives(3);

        titleScreen.gameObject.SetActive(false);
        spawnRate /= difficulty;
    }

    void ChangePaused()
    {
        if(!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    IEnumerator SpawnTarget()
    {
        while (MainManager.Instance.isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
        if (score < 0)
        {
            GameOver();
        }
    }

    public void UpdateLives(int livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;
        if (lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        MainManager.Instance.isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // or you can use string "Prototype5"
    }
}
