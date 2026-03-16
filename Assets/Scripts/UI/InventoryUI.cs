using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private Transform template;

    private void Awake()
    {
        Hide();
    }
    private void Start()
    {
        InventoryManager.Instance.onItemAdded += InventoryManager_onItemAdded;
        InventoryManager.Instance.onItemRemoved += InventoryManager_onItemRemoved;
        UpdateVisual();
    }

    private void InventoryManager_onItemAdded(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void InventoryManager_onItemRemoved(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == template)  continue;
            Destroy(child.gameObject);
        }

        foreach (IInteractable interactable in InventoryManager.Instance.GetInteractablesList())
        {
            Transform itemTransform = Instantiate(template,container);
            itemTransform.gameObject.SetActive(true);
            itemTransform.GetComponent<InventoryManagerSingleUI>().SetItemCount(interactable);
        }
    }
    private void Show()
    {
        template.gameObject.SetActive(true);
    }
    private void Hide()
    {
        template.gameObject.SetActive(false);
    }
}
