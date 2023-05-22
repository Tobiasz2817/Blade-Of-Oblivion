using UnityEngine;
using UnityEngine.Events;

public class RollingAnimEventHandler : MonoBehaviour
{
    public UnityEvent<bool> OnChangeRollingState;

    public void LockRollingAnim() {
        OnChangeRollingState?.Invoke(false);
    }

    public void UnLockRollingAnim() {
        OnChangeRollingState?.Invoke(true);
    }
}
