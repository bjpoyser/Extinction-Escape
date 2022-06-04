using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private static PauseMenu _instance;

    [SerializeField] private GameObject _content;
    [SerializeField] private Button _resume;
    [SerializeField] private Button _exit;

    private Action _action;

    public static PauseMenu Instance
    {
        get
        {
            if( _instance == null)
            {
                PauseMenu prefab = Resources.Load<PauseMenu>("PopUps/PauseMenu");
                PauseMenu newPause = Instantiate(prefab, null);
                _instance = newPause;
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
        
        _exit.onClick.AddListener(Exit);
    }

    public void Show(Action pCallback)
    {
        Cursor.lockState = CursorLockMode.None;
        _content.SetActive(true);
        _action = pCallback;
        _resume.onClick.AddListener(Resume);
    }

    public void Hide()
    {
        _content.SetActive(false);
        Time.timeScale = 1;
    }

    private void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _action?.Invoke();
        Hide();
    }

    private void Exit()
    {
        Hide();
        SceneManager.LoadScene(0);
    }
}
