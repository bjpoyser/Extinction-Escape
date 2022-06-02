using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    #region Constants
    private const string SAVE_WARNING_TRIGGERED = "saved_penguin_warning";
    private const string LOST_WARNING_TRIGGERED = "lost_penguin_warning";
    private const string HIGH_SCORE = "penguins_high_score";
    private const string SHOW_INTRO = "show_penguins_intro";
    #endregion

    #region Inspector Variables
    [Header("UI")]
    [SerializeField] private GameObject[] _lives = new GameObject[3];
    [SerializeField] private TMP_Text _saved;

    [Header("Warnings")]
    [SerializeField] private GameObject _lostPenguinWarning;
    [SerializeField] private GameObject _savedPenguinWarning;

    [Header("Game Over")]
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _mesage;
    [SerializeField] private TMP_Text _currentScore;
    [SerializeField] private TMP_Text _highScore;
    [SerializeField] private Button _playAgain;
    [SerializeField] private Button _mainMenu;

    [Header("Tutorials")]
    [SerializeField] private GameObject _tutorials;
    #endregion

    #region Private Variables
    private static UIManager _instance;
    #endregion

    #region Properties
    public static UIManager Instance { get => _instance; set => _instance = value; }
    #endregion

    #region Methods
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }

        _playAgain.onClick.AddListener(PlayAgain);
        _mainMenu.onClick.AddListener(MainMenu);
    }

    private void Start()
    {
        UpdateLives(0);
        UpdateSaved(0);

        _gameOver.SetActive(false);

        if (PlayerPrefs.GetInt(SHOW_INTRO, 1) == 1)
        {
            _tutorials.SetActive(true);
            PenguinGameManager.Instance.Pause(true);
        }
        else
        {
            _tutorials.SetActive(false);
            PenguinGameManager.Instance.Pause(false);
        }
    }

    public void UpdateLives(int pLost)
    {
        int currentLives = _lives.Length - pLost;
        for (int i = 0; i < _lives.Length; i++)
        {
            _lives[i].SetActive(false);
        }

        for (int i = 0; i < currentLives; i++)
        {
            _lives[i].SetActive(true);
        }
    }

    public void UpdateSaved(int pSaved)
    {
        _saved.text = pSaved.ToString();
    }

    public void CheckLostPenguinWarning()
    {
        bool wasTriggered = PlayerPrefs.GetInt(LOST_WARNING_TRIGGERED, 0) == 1;
        if (!wasTriggered)
        {
            _lostPenguinWarning.SetActive(true);
            PenguinGameManager.Instance.Pause(true);
            PlayerPrefs.SetInt(LOST_WARNING_TRIGGERED, 1);
        }
    }

    public void CheckSavedPenguinWarning()
    {
        bool wasTriggered = PlayerPrefs.GetInt(SAVE_WARNING_TRIGGERED, 0) == 1;
        if (!wasTriggered)
        {
            _savedPenguinWarning.SetActive(true);
            PenguinGameManager.Instance.Pause(true);
            PlayerPrefs.SetInt(SAVE_WARNING_TRIGGERED, 1);
        }
    }

    public void ShowGameOver()
    {
        if (PenguinGameManager.Instance.TotalPenguinsSaved >= 10)
        {
            _title.text = "Awesome!";
            _mesage.text = "You've saved a lot of penguins";
        }
        else if (PenguinGameManager.Instance.TotalPenguinsSaved < 10 && PenguinGameManager.Instance.TotalPenguinsSaved >= 4)
        {
            _title.text = "Great!";
            _mesage.text = "You've saved some baby penguins, but could've been more";
        }
        else
        {
            _title.text = "Bad News";
            _mesage.text = "You need to save more penguins next time";
        }

        int highScore = PlayerPrefs.GetInt(HIGH_SCORE, 0);
        int currentScore = PenguinGameManager.Instance.TotalPenguinsSaved;

        _currentScore.text = $"Penguins Saved: {currentScore}";

        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt(HIGH_SCORE, currentScore);
            _highScore.text = "* New High Score *";
        }
        else
        {
            _highScore.text = $"High Score: {highScore}";
        }

        _gameOver.SetActive(true);

    }

    private void PlayAgain()
    {
        PlayerPrefs.SetInt(SHOW_INTRO, 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void MainMenu()
    {
        PlayerPrefs.SetInt(SHOW_INTRO, 1);
        SceneManager.LoadScene(0);
    }
    #endregion
}
