using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject GameOverScreen;
    public TextMeshProUGUI player1ScoreText;
    public TextMeshProUGUI player2ScoreText;
    public TextMeshProUGUI winnerText;

    private int player1Score = 0;
    private int player2Score = 0;

    private bool isPaused = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject); // Ensure only one GameManager exists
            return;
        }

    }

    private void Start()
    {
        Time.timeScale = 0;// Pause game on start
        startMenu.SetActive(true);
        pauseMenu.SetActive(false);
        GameOverScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    public void StartGame()
    {
        Time.timeScale = 1;// Unpause game
        startMenu.SetActive(false);
    }
    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenu.SetActive(isPaused);
    }
    public void GameOver(string winner)
    {
        Time.timeScale = 0; // Stop game
        GameOverScreen.SetActive(true);
        winnerText.text = "Winner: " + winner;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void UpdateScore(int player, int Score)
    {
        if (player == 1)
        {
            player1Score += Score;
            player1ScoreText.text = "P1 Score: " + player1Score;
        }
        else
        {
            player2Score += Score;
            player2ScoreText.text = "P2 Score: " + player2Score;
        }
    }
}
