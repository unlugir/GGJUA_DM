using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorSelector: MonoBehaviour
{
    [SerializeField] Texture2D useCursor;
    private void OnMouseExit()
    {
        CursorController.Instance.SetDefault();
    }

    private void OnMouseEnter()
    {
        CursorController.Instance.SetCursor(useCursor);
    }
}
