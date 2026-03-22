using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    public static OptionsUI Instance {get; private set;}
    [SerializeField] private Button sfxButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsButtonText;
    [SerializeField] private TextMeshProUGUI musicButtonText;
    private Action OnCloseButtonAction;
    private void Awake()
    {
        Instance = this;

        sfxButton.onClick.AddListener( () =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        } );

        musicButton.onClick.AddListener( () =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        } );

        closeButton.onClick.AddListener( () =>
        {
            Hide();
            OnCloseButtonAction();
        } );
    }

    private void Start()
    {
        GameInput.Instance.OnGameUnpauseAction += GameInput_OnGameUnpauseAction;
        UpdateVisual();
        Hide();
    }

    private void GameInput_OnGameUnpauseAction(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectsButtonText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicButtonText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f);
    }
    public void Show(Action closeButtonAction)
    {
        this.OnCloseButtonAction = closeButtonAction;
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
