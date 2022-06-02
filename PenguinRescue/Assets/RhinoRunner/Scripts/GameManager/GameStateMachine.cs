using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    play,
    pause, 
    win, 
    lose
}

public class GameStateMachine : MonoBehaviour
{
    #region Singleton
    public static GameStateMachine Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
            return;
    }
    #endregion

    [SerializeField] 
    public GameState gameState = GameState.play;
    GameState currState;
    GameState prevState;
    GameState nextState;

    public void ChangeGameState(GameState newState)
    {
        CachePreviousState(currState);
        CacheNextState(newState);

        StateChange();
    }

    void CachePreviousState(GameState curr)
    {
        prevState = curr;
    }

    void CacheNextState(GameState next)
    {
        nextState = next;
    }

    void StateChange()
    {
        currState = nextState;
        gameState = currState;
    }
}
