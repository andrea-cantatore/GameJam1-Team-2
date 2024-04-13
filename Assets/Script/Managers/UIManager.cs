using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Coroutines
    Coroutine ResetProgressBarCoroutine;
    Coroutine TimerCoroutine;

    // References
    [Header("Screens References")]
    [SerializeField] GameObject HUD;
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ControlsScreen;
    [SerializeField] GameObject ResetBar;
    [SerializeField] Image ResetBarFiller;
    [SerializeField] GameObject DialogObj;
    [SerializeField] TMP_Text Dialog_txt;
    [SerializeField] TMP_Text Timer_txt;
    [SerializeField] List<GameObject> Lives;
    [SerializeField] GameObject emptyKey;
    [SerializeField] GameObject keyObtained;
    
    private int livesIndex = 4;
    [Header("Buttons References")]
    [SerializeField] RectTransform ControlsButton;
    [SerializeField] RectTransform BackButton_InControls;
    [Header("Parameters")]
    [SerializeField] float ResetTimer;
    [Header("Debug")]
    bool DialogBoxActive;
    
    


    private void OnEnable()
    {
        EventManager.OnPause += OnPauseMenu;
        EventManager.OnResume += Resume;
        EventManager.OnResetStarted += StartResetTimer;
        EventManager.OnResetCanceled += CancelResetTimer;
        EventManager.OnReset += PerformReset;
        EventManager.OnPlayerDeath += Restart;
        EventManager.OnEnterDialogue += ReadDialog;
        EventManager.OnExitDialogue += ExitDialog;
        EventManager.OnTimerStarted += StartTimer;
        EventManager.OnTimerCanceled += TimerCanceled;
        EventManager.OnPlayerChangeHpNotHiFrame += LivesChanges;
        EventManager.OnKeyCollected += KeyCollected;
    }

    private void OnDisable()
    {
        EventManager.OnPause -= OnPauseMenu;
        EventManager.OnResume -= Resume;
        EventManager.OnResetStarted -= StartResetTimer;
        EventManager.OnResetCanceled -= CancelResetTimer;
        EventManager.OnReset -= PerformReset;
        EventManager.OnPlayerDeath -= Restart;
        EventManager.OnEnterDialogue -= ReadDialog;
        EventManager.OnExitDialogue -= ExitDialog;
        EventManager.OnTimerStarted -= StartTimer;
        EventManager.OnTimerCanceled -= TimerCanceled;
        EventManager.OnPlayerChangeHpNotHiFrame -= LivesChanges;
        EventManager.OnKeyCollected -= KeyCollected;
    }
    private void Start()
    {
        HUD.SetActive(true);
        SettingsMenu.SetActive(false);
        ControlsScreen.SetActive(false);
        ResetBar.SetActive(false);
        DialogObj.SetActive(false);
        Timer_txt.gameObject.SetActive(false);
        emptyKey.SetActive(true);
        keyObtained.SetActive(false);
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
        Debug.Log("RESET"); //TODO: Implementare quando sono pronti gli sprite
    }

    void Restart()
    {
        foreach(GameObject heart in Lives)
            heart.SetActive(true);
        livesIndex = 4;
    }


    void LivesChanges(int value)
    {
        if (value < 0)
        {
            Lives[livesIndex].SetActive(false);
            livesIndex += value;
        }
        else if(livesIndex < Lives.Count - 1 && value > 0)
        {
            Lives[livesIndex].SetActive(true);
            livesIndex += value;
        }
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
        EventManager.OnReset?.Invoke();
    }

    void ReadDialog(string message)
    {
        if (DialogBoxActive == false)
        {

            DialogBoxActive = true;
            DialogObj.SetActive(true);
            Dialog_txt.text = message;
        }
        else ExitDialog();

        
    }

    void ExitDialog()
    {
        if (DialogObj.activeInHierarchy)
        {
            DialogBoxActive = false;
            Dialog_txt.text = "";
            DialogObj.SetActive(false);
        }
    }

    void StartTimer(int time)
    {
        Timer_txt.gameObject.SetActive(true);
        TimerCoroutine = StartCoroutine(Timer(time));
    }

    void TimerCanceled()
    {
        Timer_txt.gameObject.SetActive(false);
        StopCoroutine(TimerCoroutine);
    }

    IEnumerator Timer(int time)
    {
        while (time > 0) 
        {
            Timer_txt.text = time.ToString();
            time--;
            Timer_txt.fontSize += 10;
            yield return new WaitForSeconds(0.5f);
            Timer_txt.fontSize -= 10;
            yield return new WaitForSeconds(0.5f);
        }
        Timer_txt.gameObject.SetActive(false);
        EventManager.OnTimerEnded?.Invoke();
    }

    void KeyCollected()
    {
        emptyKey.SetActive(false);
        keyObtained.SetActive(true);
    }
}
    

