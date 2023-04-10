using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class CharacterRotator : MonoBehaviour
{
    [SerializeField] private float speedRotation = 250f;
    [SerializeField] private Transform rotateTarget;

    private float minValidateX = -5f;
    private float maxValidateX = 5f;
    private float lastInputX;
    public Vector2 mouseDelta;

    private void Awake() {
        if (rotateTarget == null) this.enabled = false;
    }
    
    private void OnEnable() {
        InputManager.Input.Camera.Look.performed += OnLookDelta;
    }

    private void OnDisable() {
        InputManager.Input.Camera.Look.performed -= OnLookDelta;
    }
    
    private void OnLookDelta(InputAction.CallbackContext obj) {
        var mouseDeltaInput = obj.ReadValue<Vector2>();
        if (mouseDeltaInput.x > minValidateX && mouseDeltaInput.x < maxValidateX) return;
        mouseDelta = mouseDeltaInput;
    }
    
    private void Update() {
        if (lastInputX == mouseDelta.x) return;
        UpdateRotate();

        lastInputX = mouseDelta.x;
    }

    private void UpdateRotate() {
        float dampDelta = Mathf.Clamp(mouseDelta.x, -1, 1) * speedRotation * Time.deltaTime;
        float angle = rotateTarget.localEulerAngles.y + dampDelta;
        Quaternion rotation =  Quaternion.Euler(new Vector3(0, angle, 0));
        rotateTarget.rotation = Quaternion.Lerp(rotateTarget.rotation, rotation, Time.deltaTime * speedRotation);
    }
}


