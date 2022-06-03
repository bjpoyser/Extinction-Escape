using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string SHOW_INTRO = "show_penguins_intro";

    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _creditsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _backButton;

    [Header("Screens")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _gameSelection;
    [SerializeField] private GameObject _settings;
    [SerializeField] private GameObject _credits;

    private void Awake()
    {
        PlayerPrefs.SetInt(SHOW_INTRO, 1);

        _playButton.onClick.AddListener(OnPlayButtonPressed);
        _settingsButton.onClick.AddListener(OnSettingsButtonPressed);
        _creditsButton.onClick.AddListener(OnCreditsButtonPressed);
        _exitButton.onClick.AddListener(OnExitButtonPressed);
        _backButton.onClick.AddListener(OnBackButtonPressed);

        DisableAll();
        _mainMenu.SetActive(true);
    }

    private void OnBackButtonPressed()
    {
        DisableAll();
        _mainMenu.SetActive(true);
    }

    private void OnPlayButtonPressed()
    {
        DisableAll();
        _backButton.gameObject.SetActive(true);
        _gameSelection.SetActive(true);
    }

    private void OnSettingsButtonPressed()
    {
        DisableAll();
        _backButton.gameObject.SetActive(true);
        _settings.SetActive(true);
    }

    private void OnCreditsButtonPressed()
    {
        DisableAll();
        _backButton.gameObject.SetActive(true);
        _credits.SetActive(true);
    }

    private void OnExitButtonPressed()
    {
        Application.Quit();
    }

    private void DisableAll()
    {
        _mainMenu.SetActive(false);
        _gameSelection.SetActive(false);
        _settings.SetActive(false);
        _credits.SetActive(false);
        _backButton.gameObject.SetActive(false);
    }
}
