using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float characterSpeed = 15f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform moveTarget;
    
    private Vector2 directionMovement;
    public void Awake() {
        if(characterController == null)
            characterController = GetComponent<CharacterController>();
        if (moveTarget == null)
            moveTarget = transform;
        if (characterController == null)
            this.enabled = true;
    }

    private void OnEnable() {
        InputManager.Input.Character.Movement.performed += ReadMovement;
    }

    private void OnDisable() {
        InputManager.Input.Character.Movement.performed -= ReadMovement;
    }
    
    private void ReadMovement(InputAction.CallbackContext callback) {
        directionMovement = callback.ReadValue<Vector3>();
    }

    private void Update() {
        var direction = (directionMovement.y * moveTarget.transform.forward + directionMovement.x * moveTarget.transform.right).normalized;
        characterController.Move(direction * (characterSpeed * Time.deltaTime));
    }

    public Vector3 GetDirectionMovement() {
        return directionMovement;
    }
}
