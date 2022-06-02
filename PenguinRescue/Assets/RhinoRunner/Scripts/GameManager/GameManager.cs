using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
            return;
    }
    #endregion

    GameState state;
    [HideInInspector] public bool b_gameActive;

    public float levelTime;
    string winScene;
    string loseScene;


    // Start is called before the first frame update
    void Start()
    {
        state = GameStateMachine.Instance.gameState;

        b_gameActive = StateComparitor(state);

        winScene = "RhinoRunner_WinCondition";
        loseScene = "RhinoRunner_LoseCondition";
    }

    // Update is called once per frame
    void Update()
    {
        state = GameStateMachine.Instance.gameState;
        b_gameActive = StateComparitor(state);

        CondtionComparitor(state);
    }

    private void FixedUpdate()
    {
        if (b_gameActive)
        {
            levelTime -= Time.fixedDeltaTime;
            Mathf.Clamp(levelTime, 0000.0000f, 9999.9999f);

            if (levelTime <= 0.0f)
            {
                GameStateMachine.Instance.ChangeGameState(GameState.lose);
            }
        }
    }

    bool StateComparitor(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.play:
                return true;
            default:
                return false;
        }
    }

    void CondtionComparitor(GameState _gameState)
    {
        switch (_gameState)
        {
            case GameState.win:
                SceneManager.LoadScene(winScene);
                break;
            case GameState.lose:
                SceneManager.LoadSceneAsync(loseScene);
                break;
            default:
                break;
        }
    }

    #region Level Time Functionality

    public void AdjustTime(float _time)
    {
        levelTime += _time;
    }

    #endregion


    #region Play/Pause Functionality

    [ContextMenu("Pause Game")]
    public void PauseGame()
    {
        GameStateMachine.Instance.ChangeGameState(GameState.pause);
    }

    [ContextMenu("Resume Game")]
    public void ResumeGame()
    {
        GameStateMachine.Instance.ChangeGameState(GameState.play);
    }

    #endregion
}
