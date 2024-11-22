using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TP 2 - Ahumada, Leandro
// Script de Lockeo/Desbloqueo Cursor para Menús.

public class UnlockCursor : MonoBehaviour
{
    void Awake()
    {
        ActivateCursor();
    }

    public void ActivateCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
