using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class MiniGameCard : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _cardButton;

    [Header("Text Fields")]
    [SerializeField] private TMP_Text _gameName;
    [SerializeField] private TMP_Text _gameDescription;


    private string _gameScene;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonPressed);
        _cardButton.onClick.AddListener(OnPlayButtonPressed);
    }

    private void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(_gameScene);
    }

    public void Initialize(MiniGameScriptableObject pScriptable)
    {
        _gameName.text = pScriptable.GameName;
        _gameDescription.text = pScriptable.Description;
        _gameScene = pScriptable.SceneName;
    }
}
