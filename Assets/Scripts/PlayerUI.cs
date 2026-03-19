using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI exitInspectText;

    private void Start()
    {
        InteractionController.Instance.OnInteractDetected += InteractionController_OnInteractDetected;
        InteractionController.Instance.OnInteractUndetected += InteractionController_OnInteractUndetected;
        Inspectable.OnEnterInspect += Inspectable_OnEnterInspect;
        Inspectable.OnExitInspect += Inspectable_OnExitInspect;
        
        UpdateVisual();
        Hide();
        HideInspectText();
    }
    
    private void Inspectable_OnExitInspect(object sender, EventArgs e)
    {
       HideInspectText();
    }

    private void Inspectable_OnEnterInspect(object sender, EventArgs e)
    {
        ShowInspectText();
    }

    private void InteractionController_OnInteractUndetected(object sender, EventArgs e)
    {
       Hide();
    }

    private void InteractionController_OnInteractDetected(object sender, EventArgs e)
    {
        Show();
    }

    private void UpdateVisual()
    {
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
    }

    private void Show()
    {
        container.gameObject.SetActive(true);
    }
    private void Hide()
    {
        container.gameObject.SetActive(false);
    }
    private void ShowInspectText()
    {
        exitInspectText.gameObject.SetActive(true); 
    }
    private void HideInspectText()
    {
        exitInspectText.gameObject.SetActive(false); 
    }
}
