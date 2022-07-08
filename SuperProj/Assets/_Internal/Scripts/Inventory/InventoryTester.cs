using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTester : MonoBehaviour
{
    [SerializeField] private List<Item> items;
    [SerializeField] private Inventory inventory;

    private void Start()
    {
        foreach (var item in items)
        {
            inventory.AddItem(item);
        }
    }
}
