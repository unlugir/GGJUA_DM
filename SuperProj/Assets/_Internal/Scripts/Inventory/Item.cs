using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Item")]
public class Item : ScriptableObject
{
    public int Id;
    public Sprite Icon;
}
