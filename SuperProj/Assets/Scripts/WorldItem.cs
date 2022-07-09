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
        Inventory.Instance.AddItem(item);
        PickItemAnimator.Instance.Animate(item, transform.position);
        Destroy(this.gameObject);
    }
}
