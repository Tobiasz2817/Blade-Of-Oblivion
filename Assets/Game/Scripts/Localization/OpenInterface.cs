using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class OpenInterface : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectToShow;
    [SerializeField] private InputActionReference actionReference;

    private bool isHidded = false;
    private bool canRead = false;
    
    private void OnEnable() {
        actionReference.action.started += MakeAction;
        actionReference.action.Enable();
    }

    private void OnDisable() {
        actionReference.action.started -= MakeAction;
        actionReference.action.Disable();
    }

    private void OnTriggerEnter(Collider other) {
        canRead = true;
    }

    private void OnTriggerExit(Collider other) {
        canRead = false;
        
        if (!isHidded) return;
        ShowInterface(false);
    }

    private void MakeAction(InputAction.CallbackContext callbackContext) {
        if (!canRead) return;
        
        ShowInterface(!isHidded);
    }

    private void ShowInterface(bool state) {
        gameObjectToShow.SetActive(state);

        isHidded = state;
    }
}
