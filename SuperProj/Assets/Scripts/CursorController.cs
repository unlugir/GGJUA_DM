using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    public static CursorController Instance { get; private set; }
    [SerializeField] Texture2D defaultCursor;
    private void Awake()
    {
        Instance = this;
        SetDefault();
    }
    public void SetDefault()
    {
        var hotSpot = new Vector2(defaultCursor.width / 2, defaultCursor.height / 2);
        Cursor.SetCursor(defaultCursor, hotSpot, CursorMode.Auto);
    }
    public void SetCursor(Texture2D cursor)
    {
        var hotSpot = new Vector2(defaultCursor.width / 2, defaultCursor.height / 2);
        Cursor.SetCursor(cursor, hotSpot, CursorMode.Auto);
    }
}
