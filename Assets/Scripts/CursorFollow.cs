using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    private bool IsMobile
    {
        get
        {
            #if UNITY_ANDROID
                return true;
            #else
                return false;
            #endif
        }
    }

    private void Start()
    {
        if (IsMobile)
        {
            gameObject.SetActive(false);
        }

        Cursor.visible = false;
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }
}
