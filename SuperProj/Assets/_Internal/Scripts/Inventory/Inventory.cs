using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Inventory : MonoBehaviour
{
    [SerializeField] private int slotAmount = 8;
    public static Inventory Instance { get; private set; }
    public int SlotAmount => slotAmount;

    public List<Slot> Slots;
    //[SerializeField] List<Item> startingItems;

    private void Awake()
    {
        Instance = this;
        Slots = new List<Slot>(slotAmount);

        for (int i = 0; i < slotAmount; i++)
        {
            Slots.Add(new Slot());
        }

    }

    public bool AddItem(Item item)
    {
        for (int i = 0; i < slotAmount; i++)
        {
            if (Slots[i].Item == null)
            {
                Slots[i].Item = item;
                return true;
            }
        }

        Debug.LogWarning($"No slot for {item.name}");
        return false;
    }

    public bool HasItem(Item item) 
    {
        return Slots.Any(s => s.Item.Id == item.Id);
    }
    public void RemoveItem()
    {

    }

}

public class Slot
{
    public event Action<Item> OnChanged;

    private Item _item;
    public Item Item
    {
        get => _item;
        set
        {
            _item = value;
            OnChanged?.Invoke(_item);
        }
    }

    public void Swap(Slot slot2)
    {
        var temp = Item;
        Item = slot2.Item;
        slot2.Item = temp;
    }
}
