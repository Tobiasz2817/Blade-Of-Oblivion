using System;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(TargetRotateCharacter), typeof(CharacterMovement))]
public class CharacterRotator : MonoBehaviour
{
    [SerializeField] private TargetRotateCharacter targetRotateCharacter;
    [SerializeField] private CharacterMovement characterMovement;

    private void Awake() {
        targetRotateCharacter = GetComponent<TargetRotateCharacter>();
        characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update() {
        if (characterMovement.GetDirectionMovement() == Vector3.zero) 
            return;

        targetRotateCharacter.RotateCharacter();
    }
}


