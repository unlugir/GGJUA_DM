using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    [SerializeField] CharacterController2D cc2d;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask interactionLayer;
    public bool IsEnabled = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsEnabled) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Input.GetMouseButtonDown(0))
        {
            var hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity, interactionLayer);
            if (hit.collider == null) MoveCharacter(null);

        }
    }

    void MoveCharacter(System.Action onReach)
    {
        var click = cc2d.transform.position;
        click.x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        var hit = Physics2D.Raycast(click, Vector2.down, Mathf.Infinity, groundLayer);
        if (hit.collider != null) cc2d.MoveTo(hit.point, onReach);
    }
}
