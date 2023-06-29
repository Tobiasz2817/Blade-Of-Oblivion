using System.Collections;
using UnityEngine;

public class JumpInDirection : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private JumpInDirectionInput jumpInvoker;
    [SerializeField] private float jumpSpeed;

    public bool IsJumping { private set; get; } = false;

    private void OnEnable() {
        jumpInvoker.OnStartJump += StartJumping;
        jumpInvoker.OnEndJump += EndJumping;
    }

    private void OnDisable() {
        jumpInvoker.OnStartJump -= StartJumping;
        jumpInvoker.OnEndJump -= EndJumping;
    }

    private void StartJumping(Vector3 direction) {
        StartCoroutine(Jump(direction));
        IsJumping = true;
    }

    private void EndJumping() {
        StopAllCoroutines();
        IsJumping = false;
    }

    private IEnumerator Jump(Vector3 direction) {
        float time = 0f;
        while (true) {
            transform.position += direction * animationCurve.Evaluate(time) * jumpSpeed * Time.deltaTime;
            time += Time.deltaTime;

            yield return null;
        }
    }
}
