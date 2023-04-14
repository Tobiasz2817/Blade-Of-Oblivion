using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementCharacterAnimation : MonoBehaviour
{
    [SerializeField] private CharacterMovement characterMovement;
    [SerializeField] private Animator characterAnimator;
    
    [SerializeField] private string blendTreeNameAxisX = "MoveX";
    [SerializeField] private float smoothTimeX = 0.2f;
    
    [SerializeField] private string blendTreeNameAxisZ = "MoveZ";
    [SerializeField] private float smoothTimeZ = 0.2f;
    
    [SerializeField] private string nameSprintParameter = "IsSprint";
    
    private float valueMoveX;
    private float valueMoveZ;
    private float velocityX = 0.0f;
    private float velocityZ = 0.0f;

    private void Awake() {
        InputManager.Input.Character.Sprint.performed += ChangeSprintState;
        InputManager.Input.Character.Sprint.canceled += ChangeSprintState;
    }

    private void OnDestroy() {
        InputManager.Input.Character.Sprint.performed -= ChangeSprintState;
        InputManager.Input.Character.Sprint.canceled -= ChangeSprintState;
    }

    private void ChangeSprintState(InputAction.CallbackContext inputs) {
        var isSprinting = inputs.ReadValue<float>() == 0 ? false : true;
        characterAnimator.SetBool(nameSprintParameter, isSprinting);
    }


    void Update() {
        valueMoveX = Mathf.SmoothDamp(valueMoveX, characterMovement.DirectionMovement.x, ref velocityX, smoothTimeX);
        valueMoveZ = Mathf.SmoothDamp(valueMoveZ,characterMovement.DirectionMovement.y, ref velocityZ,smoothTimeZ);
        characterAnimator.SetFloat(blendTreeNameAxisX,valueMoveX);
        characterAnimator.SetFloat(blendTreeNameAxisZ,valueMoveZ);
    }
}
