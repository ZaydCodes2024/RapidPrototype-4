using UnityEngine;

public class PuzzleBox : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        InteractionController.Instance.TryInspect(GetGameObject());
    }

    public bool IsInventoryItem()
    {
        return false;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
