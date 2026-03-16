using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
   public void Interact();
   public bool IsInventoryItem();
   public int ItemCount();
}
