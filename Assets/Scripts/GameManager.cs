using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOvertext;
    public TextMeshProUGUI pausedText;
    public Slider volumeSlider;
    public GameObject restartButton;
    public GameObject titleScreen;
    public bool isGameActive;
    public bool timePaused = false;
    public bool aimBotOn = false;

    private AudioSource audioSource;
    private int score;
    private int lives;
    private float spawnRate = 4;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set volume to slider value
        audioSource.volume = volumeSlider.value;

        // Pauses and unpauses the game when space is pressen
        if(Input.GetKeyDown(KeyCode.Space) && !timePaused && isGameActive)
        {
            pausedText.gameObject.SetActive(true);
            timePaused = true;
            Time.timeScale = 0;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && timePaused && isGameActive)
        {
            pausedText.gameObject.SetActive(false);
            timePaused = false;
            Time.timeScale = 1;
        }

        // Go back to main screen when escape is pressed
        if(isGameActive && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Spawns random targets every few seconds
    IEnumerator SpawnTarget()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Updates the score and displays it
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        if(score < 0)
        {
            score = 0;
        }
        scoreText.text = "Score: " + score;
    }

    // Updates the lives and displays them
    public void UpdateLives(int livesToLose)
    {
        lives -= livesToLose;
        livesText.text = "Lives: " + lives;

        // If all lives are lost display game over
        if(lives <= 0)
        {
            GameOver();
        }
    }

    // Displays game over and stops the game
    void GameOver()
    {
        gameOvertext.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    // Restarts the game when called
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Starts the game when called
    public void StartGame(int difficulty)
    {
        titleScreen.gameObject.SetActive(false);

        isGameActive = true;
        timePaused = false;
        Time.timeScale = 1;

        score = 0;
        UpdateScore(0);
        lives = 3;
        UpdateLives(0);

        // If aimbot difficulty was selected activate aimbot
        if(difficulty == 1)
        {
            aimBotOn = true;
            spawnRate = 0.1f;
            StartCoroutine("SpawnTarget");
        }
        else
        {
            spawnRate /= difficulty;
            StartCoroutine("SpawnTarget");
        }
    }
}
