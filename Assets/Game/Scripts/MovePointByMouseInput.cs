using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovePointByMouseInput : MonoBehaviour
{
    [SerializeField] private float sensitivity = 5.0f;
    [SerializeField] private float smoothTime = 0.3f; 
    [SerializeField] private float minY = 0.0f;
    [SerializeField] private float maxY = 2.0f; 

    [SerializeField] private Transform moveTarget;
    
    private float lastInputY;
    private float currentVelocity = 0.0f; 
    private float targetY;  
    private Vector2 mouseInput;

    private void Awake() {
        if (moveTarget == null) this.enabled = false;
    }

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable() {
        InputManager.Input.Camera.Look.performed += OnLookDelta;
    }

    private void OnDisable() {
        InputManager.Input.Camera.Look.performed -= OnLookDelta;
    }

    private void OnLookDelta(InputAction.CallbackContext context) {
        mouseInput = context.ReadValue<Vector2>();
    }

    private void Update() {
        if (lastInputY == mouseInput.y) return;
        UpdateCameraFollow();

        lastInputY = mouseInput.y;
    }

    
    private void UpdateCameraFollow() {
        float inputY = mouseInput.y * sensitivity * Time.deltaTime;
        targetY = Mathf.Clamp(moveTarget.position.y + inputY, minY, maxY);
        float newY = Mathf.SmoothDamp(moveTarget.position.y, targetY, ref currentVelocity, smoothTime);
        moveTarget.position = new Vector3(moveTarget.position.x, newY, moveTarget.position.z);
    }
    
#if UNITY_EDITOR
    
    private void OnDrawGizmos() {
        if (moveTarget == null) return;
        
        var minVector = new Vector3(moveTarget.transform.position.x, minY, moveTarget.transform.position.z);
        var maxVector = new Vector3(moveTarget.transform.position.x, maxY, moveTarget.transform.position.z);
        Handles.color = Color.red;
        Handles.DrawLine(minVector,maxVector);
    }

#endif
}