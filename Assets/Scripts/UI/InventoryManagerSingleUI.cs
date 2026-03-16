using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNumberText;

    public void SetItemCount(IInteractable interactable)
    {
        itemNumberText.text = "x" + interactable.ItemCount().ToString();
    }
}
