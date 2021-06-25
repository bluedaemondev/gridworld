using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorLocker : MonoBehaviour
{
    public static CursorLocker instance { get; private set; }
    public bool isLocked { get; private set; }

    private void Awake()
    {
        instance = this;
    }
    public void LockCursorOnCenter()
    {
        isLocked = true;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnlockFreeCursor()
    {
        isLocked = false;
        Cursor.lockState = CursorLockMode.None;
    }
}
