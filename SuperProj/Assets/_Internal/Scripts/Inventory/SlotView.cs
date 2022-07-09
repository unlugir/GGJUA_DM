using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Canvas canvas; // TODO :: get canvas
    [SerializeField] private Canvas iconCanvas;
    [SerializeField] private Image icon;

    private Slot slot;

    private bool isSuccesfullDrop;

    public void Map(Slot slot)
    {
        if (slot != null) { slot.OnChanged -= OnItemChanged; }

        this.slot = slot;
        slot.OnChanged += OnItemChanged;
        OnItemChanged(slot.Item);
    }

    private void OnItemChanged(Item item)
    {
        ResetPosition();

        if (item == null)
        {
            icon.gameObject.SetActive(false);
        }
        else
        {
            icon.sprite = slot.Item.Icon;
            icon.preserveAspect = true;
            icon.gameObject.SetActive(true);
        }
    }


    private void ResetPosition()
    {
        icon.rectTransform.anchoredPosition = Vector3.zero;
        MoveToTop(false);
    }

    private void MoveToTop(bool toTop)
    {
        iconCanvas.sortingOrder = toTop ? 2 : 1;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isSuccesfullDrop = false;
        MoveToTop(true);
        //eventData.selectedObject = gameObject;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isSuccesfullDrop) return;

        ResetPosition();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!icon.gameObject.activeInHierarchy) return;

        icon.rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        var droppedSlot = eventData.pointerDrag.GetComponent<SlotView>();
        slot.Swap(droppedSlot.slot);

        isSuccesfullDrop = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Debug.Log($"{eventData.clickCount}");

        if (eventData.clickCount % 2 == 0)
        {
            Debug.Log($"Double click");
            //if (InventoryEntry != -1)
            //{
            //    if (Owner.Character.Inventory.Entries[InventoryEntry] != null)
            //        Owner.ObjectDoubleClicked(Owner.Character.Inventory.Entries[InventoryEntry]);
            //}
            //else
            //{
            //    Owner.EquipmentDoubleClicked(EquipmentItem);
            //}
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ObjectHoveredEnter(this);
        // Tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ObjectHoverExited(this);
        // Tooltip.gameObject.SetActive(false);
    }
}
