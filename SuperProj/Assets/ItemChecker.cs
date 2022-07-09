using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class ItemChecker : MonoBehaviour
{
    [SerializeField] List<Item> requiredItems;
    [SerializeField] UnityEvent failedCheckAction;
    [SerializeField] UnityEvent successCheckAction;
    public void Check() 
    {
        if (requiredItems.All(i => Inventory.Instance.HasItem(i)))
        {
            successCheckAction.Invoke();
        }
        else
        {
            failedCheckAction.Invoke();
        }
    }
}
