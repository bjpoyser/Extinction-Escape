using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _volumeButton;
    [SerializeField] private Button _accessibilityButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _mainBackButton;

    [Header("Screens")]
    [SerializeField] private GameObject _settingsScreen;
    [SerializeField] private GameObject _volumeScreen;
    [SerializeField] private GameObject _accessibilityScreen;


    /// <summary>
    /// Do nothing
    /// </summary>
    [Header("Slidders")]
    [SerializeField] private Slider _musicSlidder;
    [SerializeField] private Slider _sfxSlidder;
    [SerializeField] private Slider _dialogueSlidder;

    [Header("Toggles")]
    [SerializeField] private Toggle _colorBlind;

    private void Awake()
    {
        _volumeButton.onClick.AddListener(OnVolumeButtonPressed);
        _accessibilityButton.onClick.AddListener(OnAccesibilityButtonPressed);
        _backButton.onClick.AddListener(OnBackButtonPressed);

    }

    private void OnVolumeButtonPressed()
    {
        DisableAll();
        _backButton.gameObject.SetActive(true);
        _volumeScreen.SetActive(true);
    }
    private void OnAccesibilityButtonPressed()
    {
        DisableAll();
        _backButton.gameObject.SetActive(true);
        _accessibilityScreen.SetActive(true);
    }

    private void OnBackButtonPressed()
    {
        DisableAll();
        _mainBackButton.gameObject.SetActive(true);
        _settingsScreen.SetActive(true);
    }

    private void DisableAll()
    {
        _settingsScreen.SetActive(false);
        _volumeScreen.SetActive(false);
        _accessibilityScreen.SetActive(false);
        _backButton.gameObject.SetActive(false);
        _mainBackButton.gameObject.SetActive(false);
    }
}
