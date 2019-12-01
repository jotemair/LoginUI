using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : UnitySingleton<InventoryManager>
{
    protected override bool ShouldSurviveSceneChange()
    {
        return false;
    }

    private int _openInventoryCount = 0;

    private void Update()
    {
        if (0 < _openInventoryCount)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void OpenInventory() { ++_openInventoryCount; }

    public void CloseInventory() { --_openInventoryCount; }
}
