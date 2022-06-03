using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TurtleGameOver : MonoBehaviour
{
    int _score;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameOverScreen;

    public void GameOver(int score)
    {
        Cursor.lockState = CursorLockMode.None;
        gameOverScreen.SetActive(true);
        _score = score;
        scoreText.text = _score.ToString();
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
