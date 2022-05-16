using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOvertext;
    public GameObject restartButton;
    public bool isGameActive;

    private int score;
    private int lives;
    private float spawnRate = 1;

    // Start is called before the first frame update
    void Start()
    {
        isGameActive = true;

        StartCoroutine("SpawnTarget");

        score = 0;
        UpdateScore(0);
        lives = 3;
        UpdateLives(0);
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
