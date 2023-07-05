using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
        Application.targetFrameRate = 60;
        Cursor.visible = false;
    }
}
