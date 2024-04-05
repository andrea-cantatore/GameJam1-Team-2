using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    // Events
    public static Action OnPause;
    public static Action OnResume;
    public delegate void GameState();
    public static event GameState UnPauseToggle;

    // References
    [Header("Screens References")]
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ControlsScreen;
    [Header("Buttons References")]
    [SerializeField] RectTransform ControlsButton;
    [SerializeField] RectTransform BackButton_InControls;
    
    
    


    private void OnEnable()
    {
        OnPause += OnPauseMenu;
        OnResume += Resume;
    }

    private void OnDisable()
    {
        OnPause -= OnPauseMenu;
        OnResume -= Resume;
    }
    private void Start()
    {
        HUD.SetActive(true);
        SettingsMenu.SetActive(false);
        ControlsScreen.SetActive(false);
    }

    

    public void OnPauseMenu()
    {
        CameraBlur.OnBlurBg?.Invoke();
        SettingsMenu.SetActive(true);
        ControlsScreen.SetActive(false);
        EventSystem.current.SetSelectedGameObject(ControlsButton.gameObject);
    }


    public void OpenControlsScreen()
    {
        //HUD.SetActive(false);
        SettingsMenu.SetActive(false);
        ControlsScreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(BackButton_InControls.gameObject);
    }

    public void Resume()
    {
        CameraBlur.OnBlurOff?.Invoke();
        UnPauseToggle();
        SettingsMenu.SetActive(false);
        ControlsScreen.SetActive(false);
    }
}
    

