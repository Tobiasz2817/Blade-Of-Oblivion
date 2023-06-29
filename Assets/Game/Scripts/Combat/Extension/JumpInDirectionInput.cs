using System;
using UnityEngine;

public class JumpInDirectionInput : MonoBehaviour
{
    public Action<Vector3> OnStartJump;
    public Action OnEndJump;
}
