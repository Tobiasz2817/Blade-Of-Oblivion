using UnityEngine;

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
        if (characterMovement.DirectionMovement == (Vector2)Vector3.zero) 
            return;

        targetRotateCharacter.RotateCharacter();
    }
}


