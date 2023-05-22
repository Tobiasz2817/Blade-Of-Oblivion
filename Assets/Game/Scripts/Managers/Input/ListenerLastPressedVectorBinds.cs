using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.InputSystem;

public class ListenerLastPressedVectorBinds : MonoBehaviour
{
    [SerializeField] private InputActionReference movementCharacterAction;
    
    private int validateValue = 99;

    private Vector2 lastMovement;
    private BindDirection _bindDirection;

    private void Start() {
        _bindDirection = new BindDirection();
        movementCharacterAction.action.Enable();
    }
    
    private void OnEnable() {
        movementCharacterAction.action.performed += ReadMovement;
    }
    
    private void OnDisable() {
        movementCharacterAction.action.performed -= ReadMovement;
    }

    private void ReadMovement(InputAction.CallbackContext obj) {
        var movement = obj.ReadValue<Vector3>();

        if (movement.x != 0 && movement.x != lastMovement.x)
            _bindDirection.SetDirections((int)movement.x, Side.X);
        else if (movement.y != 0 && movement.y != lastMovement.y)
            _bindDirection.SetDirections((int)movement.y, Side.Y);
        else
            ValidateBinds((int)movement.x, (int)movement.y);
        
        lastMovement = movement;
    }

    private void ValidateBinds(int x, int y) {
        if (x == 0 && y == 0)
            _bindDirection.SetDirections(validateValue,Side.None);
        else if (x == 0)
            _bindDirection.SetDirections(y,Side.Y);
        else
            _bindDirection.SetDirections(x,Side.X);
    }

    public bool IsFindingBind() {
        if (_bindDirection.bindValue == validateValue)
            return false;
        
        return true;
    }

    public BindDirection GetDirectionBind() {
        return _bindDirection;
    }

    public struct BindDirection
    {
        public Side side;
        public int bindValue;

        public BindDirection SetDirections(int bindValue_, Side sideDirection) {
            this.bindValue = bindValue_;
            this.side = sideDirection;

            return this;
        }
    }
}
public enum Side
{
    X,
    Y,
    None
}