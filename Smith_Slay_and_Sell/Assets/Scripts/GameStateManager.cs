using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Idle,
        Active,
        End,
        Paused,
    }

    public static GameStateManager Instance { get; private set; }
    public GameState CurrentState { get; private set; } = GameState.Active;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PauseGame()
    {
        if (CurrentState == GameState.Active)
        {
            Time.timeScale = 0;
            ChangeState(GameState.Paused);
        }
    }

    public void UnPauseGame()
    {
        if (CurrentState == GameState.Paused)
        {
            Time.timeScale = 1;
            ChangeState(GameState.Active);
        }
    }

    public void StartGame()
    {
        if (CurrentState == GameState.Idle)
        {
            Time.timeScale = 1;
            ChangeState(GameState.Active);
        }
    }

    public void EndGame()
    {
        if (CurrentState != GameState.End)
        {
            ChangeState(GameState.End);
        }
    }

    public void RestartGame()
    {
        if (CurrentState != GameState.Idle)
        {
            Time.timeScale = 1;
            ChangeState(GameState.Idle);
        }
    }

    private void ChangeState(GameState newState)
    {
        CurrentState = newState;
        Debug.Log($"Changing GameState to {newState}");
    }

    public bool IsPaused()
    {
        if (CurrentState == GameState.Paused)
        {
            return true;
        }
        return false;
    }
}
