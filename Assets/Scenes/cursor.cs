using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cursor : MonoBehaviour
{
    public Texture2D currsor;

    void Start()
    {
        Cursor.SetCursor(currsor, Vector2.zero, CursorMode.Auto);
    }
}
