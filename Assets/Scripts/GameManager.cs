using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Interacts with scene management
using UnityEngine.UI;  // Interacts with the Buttons
using TMPro; // Interacts with TextMeshProUGUI

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 3.0f;
    public bool isGameActive;

    // UI 
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public Button restartButton;
    public GameObject titleScreen;
    public GameObject pauseScreen;
    private bool paused;

    private int score;
    private int lives;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isGameActive)
        {
            ChangePaused();
        }
    }


    public void StartGame(int difficulty)
    {
        isGameActive = true;
        score = 0;

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

        /************************************************************************************
        * This method will change the paused boolean when it is called. When the boolean is *
        * changed to true, it enables the pauseScreen and sets the Time.timeScale to 0.     *
        * Setting the Time.timeScale to 0 makes it so that physics calculations are paused. *
        * When the boolean is changed to false, it disables the pauseScreen and sets the    *
        * Time.timeScale to 1.                                                              *
        ************************************************************************************/
    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
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
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // or you can use string "Prototype5"
    }
}
