using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MiniGameCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Buttons")]
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _cardButton;

    [Header("Text Fields")]
    [SerializeField] private TMP_Text _frontGameName;
    [SerializeField] private TMP_Text _backGameName;
    [SerializeField] private TMP_Text _gameDescription;

    [Header("Card Sides")]
    [SerializeField] private GameObject _frontSide;
    [SerializeField] private GameObject _backSide;


    private string _gameScene;

    private void Awake()
    {
        _playButton.onClick.AddListener(OnPlayButtonPressed);
        _cardButton.onClick.AddListener(OnPlayButtonPressed);

        _frontSide.SetActive(true);
        _backSide.SetActive(false);
    }

    private void OnPlayButtonPressed()
    {
        SceneManager.LoadScene(_gameScene);
    }

    public void Initialize(MiniGameScriptableObject pScriptable)
    {
        _frontGameName.text = pScriptable.GameName;
        _backGameName.text = pScriptable.GameName;
        _gameDescription.text = pScriptable.Description;
        _gameScene = pScriptable.SceneName;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _frontSide.SetActive(false);
        _backSide.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _frontSide.SetActive(true);
        _backSide.SetActive(false);
    }
}
