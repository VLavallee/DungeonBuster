using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSetting : MonoBehaviour
{
    [SerializeField] bool cursorIsVisible;
   

    private void Update()
    {
        if (!cursorIsVisible)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}
