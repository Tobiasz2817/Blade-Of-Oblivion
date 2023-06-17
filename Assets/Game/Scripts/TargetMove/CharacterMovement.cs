using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float characterSpeed = 15f;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Transform moveTarget;
    [SerializeField] private CombatManager combat;

    private bool canMove = true;
    
    public Vector2 DirectionMovement { private set; get; }

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
        DirectionMovement = callback.ReadValue<Vector3>();
    }

    private void Update() {
        if (!canMove || combat.IsCurrentAnimationBlockMovement()) return;
        var direction = (DirectionMovement.y * moveTarget.transform.forward + DirectionMovement.x * moveTarget.transform.right).normalized;
        characterController.Move(direction * characterSpeed * Time.deltaTime);
    }

    public void SetCanMove(bool canMove_) {
        this.canMove = canMove_;
    }
}
