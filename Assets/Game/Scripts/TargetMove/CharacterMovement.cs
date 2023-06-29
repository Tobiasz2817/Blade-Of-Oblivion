using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float characterSpeed = 5f;
    [SerializeField] private float sprintCharacterSpeed = 10f;
    [SerializeField] private float gravity = 7.5f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform moveTarget;
    [SerializeField] private CombatManager combat;

    private bool canMove = true;
    private Vector3 velocity;
    private float speed;

    public Vector2 DirectionMovement { private set; get; }

    public void Awake() {
        if(characterController == null)
            characterController = GetComponent<CharacterController>();
        if (moveTarget == null)
            moveTarget = transform;
        if (characterController == null)
            this.enabled = true;
    }

    private void Start() {
        speed = characterSpeed;
    }

    private void OnEnable() {
        InputManager.Input.Character.Movement.performed += ReadMovement;
        InputManager.Input.Character.Sprint.performed += ChangeSprintState;
        InputManager.Input.Character.Sprint.canceled += ChangeSprintState;
    }

    private void OnDisable() {
        InputManager.Input.Character.Movement.performed -= ReadMovement;
        InputManager.Input.Character.Sprint.performed -= ChangeSprintState;
        InputManager.Input.Character.Sprint.canceled -= ChangeSprintState;
    }
    
    private void ChangeSprintState(InputAction.CallbackContext inputs) {
        var isSprinting = inputs.ReadValue<float>() == 0 ? false : true;
        speed = isSprinting ? sprintCharacterSpeed : characterSpeed;
    }
    
    private void ReadMovement(InputAction.CallbackContext callback) {
        DirectionMovement = callback.ReadValue<Vector3>();
    }

    private void Update() {
        Falling();
        if (!canMove || combat.IsCurrentAnimationBlockMovement()) return;
        MoveTowards();
    }

    private void Falling() {
        if (!characterController.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
        else
            velocity = Vector3.zero;
    }

    private void MoveTowards() {
        var direction = (DirectionMovement.y * moveTarget.transform.forward + DirectionMovement.x * moveTarget.transform.right).normalized;
        characterController.Move(direction * speed * Time.deltaTime);
    }

    public void SetCanMove(bool canMove_) {
        this.canMove = canMove_;
    }
}
