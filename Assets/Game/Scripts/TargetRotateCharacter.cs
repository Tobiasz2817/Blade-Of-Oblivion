
using System;
using UnityEngine;

public class TargetRotateCharacter : MonoBehaviour
{
    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    public Transform rotateTarget;
    public Transform rotateTo;
    
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void LateUpdate() {
        Quaternion rotate = rotateTo.transform.rotation;
        if(!rotateX) rotate.x = 0; 
        if(!rotateY) rotate.y = 0; 
        if(!rotateZ) rotate.z = 0;

        var lerp = Quaternion.Lerp(rotateTarget.rotation, rotate,Time.deltaTime * 15f);
        rotateTarget.rotation = lerp;
    }
}
