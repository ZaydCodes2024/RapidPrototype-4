using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNumberText;
    [SerializeField] private Image itemSprite;

    public void SetItemData(InventoryItemSO inventoryItemSO ,int itemCount)
    {
        itemNumberText.text = "x" + itemCount.ToString();
        itemSprite.sprite = inventoryItemSO.itemImage;
    }
}
