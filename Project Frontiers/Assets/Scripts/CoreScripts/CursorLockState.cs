using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLockState : MonoBehaviour
{
    public void SwitchLockState(string lockState)
    {
        switch (lockState)
        {
            case "None":
                Cursor.lockState = CursorLockMode.None;
                break;
            case "Locked":
                Cursor.lockState = CursorLockMode.Locked;
                break;
            case "Convined":
                Cursor.lockState = CursorLockMode.Confined;
                break;
        }
    }
}
