using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerPause : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        var pauseAction = playerInput.actions["Pause"];
        pauseAction.performed += OnPausePerformed;
    }

    private void OnPausePerformed(InputAction.CallbackContext ctx)
    {
        TogglePause();
    }

    void TogglePause()
    {
        Debug.Log("Pausing Game");
        if (GameStateManager.Instance.IsPaused())
        {
            GameStateManager.Instance.UnPauseGame();
        }
        else
        {
            GameStateManager.Instance.PauseGame();
        }
    }
}
