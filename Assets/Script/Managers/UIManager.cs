using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    Coroutine ResetProgressBarCoroutine;


    // References
    [Header("Screens References")]
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ControlsScreen;
    [SerializeField] GameObject ResetBar;
    [SerializeField] Image ResetBarFiller;
    [Header("Buttons References")]
    [SerializeField] RectTransform ControlsButton;
    [SerializeField] RectTransform BackButton_InControls;
    [Header("Parameters")]
    [SerializeField] float ResetTimer;
    
    
    


    private void OnEnable()
    {
        EventManager.OnPause += OnPauseMenu;
        EventManager.OnResume += Resume;
        EventManager.OnResetStarted += StartResetTimer;
        EventManager.OnResetCanceled += CancelResetTimer;
        EventManager.OnResetCompleted += PerformReset;      
    }

    private void OnDisable()
    {
        EventManager.OnPause -= OnPauseMenu;
        EventManager.OnResume -= Resume;
        EventManager.OnResetStarted -= StartResetTimer;
        EventManager.OnResetCanceled -= CancelResetTimer;
        EventManager.OnResetCompleted -= PerformReset;
    }
    private void Start()
    {
        HUD.SetActive(true);
        SettingsMenu.SetActive(false);
        ControlsScreen.SetActive(false);
        ResetBar.SetActive(false);
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
        EventManager.UnPauseToggle?.Invoke();
        SettingsMenu.SetActive(false);
        ControlsScreen.SetActive(false);
    }

    void StartResetTimer()
    {
        ResetBar.SetActive(true);
        ResetBarFiller.fillAmount = 0;
        ResetProgressBarCoroutine = StartCoroutine(ResetProgressBar());
    }

    void CancelResetTimer()
    {
        if(ResetProgressBarCoroutine != null)
            StopCoroutine(ResetProgressBarCoroutine);
        ResetProgressBarCoroutine = null;
        ResetBarFiller.fillAmount = 0;
        ResetBar.SetActive(false);
    }

    void PerformReset()
    {
        Debug.Log("RESET");
    }

    IEnumerator ResetProgressBar()
    {
        float time = 0;
        
        while (time < ResetTimer)
        {
            time += Time.deltaTime;
            ResetBarFiller.fillAmount = time/ResetTimer;
            yield return null;
        }

        ResetBarFiller.fillAmount = 1;
        EventManager.OnResetCompleted?.Invoke();
    }


}
    

