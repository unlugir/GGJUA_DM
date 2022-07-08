using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour
{
    [SerializeField] Item item;
    Usable usable;
    void Start()
    {
        usable = GetComponent<Usable>();
    }

    public void Pick()
    {
        Debug.Log("ADAS");
        Inventory.Instance.AddItem(item);
        Destroy(this.gameObject);
    }
}
