using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inventory Mapper
public class InventoryView : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private SlotView slotViewPrefab;
    private List<SlotView> views;

    private void Awake()
    {
        var amount = inventory.SlotAmount;
   
        views = new List<SlotView>(amount);
        for (int i = 0; i < amount; i++)
        {
            var view = Instantiate(slotViewPrefab, this.transform);
            view.gameObject.SetActive(true);
            views.Add(view);
        }
    }

    private void Start()
    {
        // Map SlotView => Inventory

        var slots = inventory.Slots;

        for (int i = 0; i < inventory.SlotAmount; i++)
        {
            var view = views[i];
            var data = slots[i];
            view.Map(data);
        }

    }

}
