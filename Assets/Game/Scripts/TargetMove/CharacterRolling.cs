using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class CharacterRolling : MonoBehaviour
{
    public UnityEvent OnStartRolling;
    public UnityEvent OnEndRolling;

    [SerializeField] private AnimationCurve rollingCurve;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private ListenerLastPressedVectorBinds listenerBinds;
    [SerializeField] private InputActionReference rollTriggerAction;

    private bool canRolling = true;
    private float rollSpeed;
    private void Start() {
        rollTriggerAction.action.Enable();
    }
    
    private void OnEnable() {
        rollTriggerAction.action.performed += Roll;
    }
    
    private void OnDisable() {
        rollTriggerAction.action.performed -= Roll;
    }

    private void Roll(InputAction.CallbackContext obj) {
        if (!canRolling || !listenerBinds.IsFindingBind()) return;
        
        var direction = GetDirectionByLastInput();
        StartCoroutine(Rolling(direction));
    }

    private IEnumerator Rolling(Vector3 direction) {
        yield return new WaitForSeconds(0.2f);
        OnStartRolling?.Invoke();
        
        float time = 0f;
        var lastKey = rollingCurve.keys[^1];
        
        while (time < lastKey.time) {
            characterController.Move(direction * rollingCurve.Evaluate(time) * Time.deltaTime); 
            time += Time.deltaTime;

            yield return null;
        }
        
        OnEndRolling?.Invoke();
    }

    private Vector3 GetDirectionByLastInput() {
        var directionBind = listenerBinds.GetDirectionBind();
        if (directionBind.side == Side.X)
            return transform.right * directionBind.bindValue;

        return transform.forward * directionBind.bindValue;
    }

    public void ChangeStateRolling(bool canRolling_) {
        canRolling = canRolling_;
    }
}


