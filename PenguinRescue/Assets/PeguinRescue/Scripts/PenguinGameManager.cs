using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinGameManager : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField] private Camera _currentCamera;
    [SerializeField] private int _lives;
    #endregion

    #region Private Variables
    private static PenguinGameManager _instance;

    private bool _isPaused;

    private int _currentWave;

    private int _penguinsToSave;
    private int _currentPenguinsSavedOrLost;
    private int _currentPenguinsSaved;
    private int _currentPenguinsLost;

    private int _totalPenguinsSaved;
    private int _totalPenguinsSpawned;
    private int _totalPenguinLost;
    #endregion

    #region Properties
    public static PenguinGameManager Instance { get => _instance; set => _instance = value; }
    public Camera CurrentCamera { get => _currentCamera; set => _currentCamera = value; }
    public int CurrentWave { get => _currentWave; set => _currentWave = value; }
    public int PenguinsToSave { get => _penguinsToSave; set => _penguinsToSave = value; }
    public int TotalPenguinsSaved { get => _totalPenguinsSaved; set => _totalPenguinsSaved = value; }
    public int TotalPenguinLost { get => _totalPenguinLost; set => _totalPenguinLost = value; }
    public int TotalPenguinsSpawned { get => _totalPenguinsSpawned; set => _totalPenguinsSpawned = value; }
    #endregion

    #region Methods
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        _currentWave = 1;
        _currentPenguinsSavedOrLost = 0;
        _totalPenguinsSaved = 0;

        SpawnerManager.Instance.SpawnNextWave(_currentWave);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause(!_isPaused);
            if (_isPaused) PauseMenu.Instance.Show(() => { Pause(!_isPaused); });
            else PauseMenu.Instance.Hide();
        }
    }

    public void Pause(bool pPause)
    {
        if (pPause) Time.timeScale = 0f;
        else Time.timeScale = 1f;

        _isPaused = pPause;
    }

    public void PenguinSaved()
    {
        _currentPenguinsSaved++;
        _currentPenguinsSavedOrLost++;
        _totalPenguinsSaved++;
        UIManager.Instance.UpdateSaved(_totalPenguinsSaved);
        UIManager.Instance.CheckSavedPenguinWarning();
        CheckWave();
    }

    public void PenguinTrapped()
    {
        _currentPenguinsSavedOrLost++;
        _totalPenguinLost++;
        UIManager.Instance.UpdateLives(_totalPenguinLost);
        UIManager.Instance.CheckLostPenguinWarning();
        CheckWave();
    }

    private void CheckWave()
    {
        if (_currentPenguinsSavedOrLost == _penguinsToSave)
        {
            bool upgradeWave = _currentPenguinsSaved > Mathf.RoundToInt(_penguinsToSave / 2);
            if (upgradeWave) _currentWave++;

            _currentPenguinsSavedOrLost = 0;
            _currentPenguinsSaved = 0;
            _penguinsToSave = 0;

            SpawnerManager.Instance.SpawnNextWave(_currentWave);
            if (PenguinsToSave == 0)
            {
                GameOver();
            }
        }
    }

    public void GameOver()
    {
        Pause(true);
        UIManager.Instance.ShowGameOver();
    }
    #endregion
}
