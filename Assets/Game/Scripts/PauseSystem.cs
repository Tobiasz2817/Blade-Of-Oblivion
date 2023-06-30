using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseSystem : MonoBehaviour
{
    public GameObject pauseGO;
    public InputActionReference inputActionReference;

    private bool isOnInterface = false;

    private void Start() {
        TurnInterface();
        Time.timeScale =  1;
    }

    private void OnEnable() {
        inputActionReference.action.performed += TurnPauseInterface;
        inputActionReference.action.Enable();
    }

    private void OnDisable() {
        inputActionReference.action.performed -= TurnPauseInterface;
        inputActionReference.action.Disable();
    }

    private void TurnPauseInterface(InputAction.CallbackContext obj) {
        PauseGame();
    }

    private void TurnInterface() {
        pauseGO.SetActive(isOnInterface);
    }

    public void PauseGame() {
        isOnInterface = !isOnInterface;
        Time.timeScale = isOnInterface ? 0f : 1;
        Cursor.lockState = isOnInterface ? CursorLockMode.None : CursorLockMode.Locked;
        TurnInterface();
    }

    public void ExitToMenu() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
