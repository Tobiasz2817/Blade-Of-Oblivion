using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHelper : MonoBehaviour
{
    bool activated = false;

    public void SetActivate()
    {
        this.activated = true;
    }
    [Panda.Task]
    public bool GetActivated()
    {
        return activated;
    }
}
