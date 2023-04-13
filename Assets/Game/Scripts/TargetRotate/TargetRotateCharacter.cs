using UnityEngine;

public class TargetRotateCharacter : MonoBehaviour
{
    [SerializeField] private bool rotateX = true;
    [SerializeField] private bool rotateY = true;
    [SerializeField] private bool rotateZ = true;

    [SerializeField] private float rotateSpeed = 15f;

    [SerializeField] private Transform rotateTarget;
    [SerializeField] private Transform rotateTo;
    
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    public void RotateCharacter() {
        Quaternion rotate = rotateTo.transform.rotation;
        if(!rotateX) rotate.x = 0; 
        if(!rotateY) rotate.y = 0; 
        if(!rotateZ) rotate.z = 0;

        var lerp = Quaternion.Lerp(rotateTarget.rotation, rotate,Time.deltaTime * rotateSpeed);
        rotateTarget.rotation = lerp;
    }
}
