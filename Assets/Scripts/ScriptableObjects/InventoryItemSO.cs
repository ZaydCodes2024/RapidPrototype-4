using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemSO : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public bool isStackable = true;
}
