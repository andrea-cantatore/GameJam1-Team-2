using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    [SerializeField] GameObject loseScreen, winScreen;
    [SerializeField] GameObject WinScreenButton;
    [SerializeField] GameObject LoseScreenButton;
    
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
        EventManager.OnPlayerDeath += GameOver;
        EventManager.OnEnterDialogue += ReadDialog;
        EventManager.OnExitDialogue += ExitDialog;
        EventManager.OnTimerStarted += StartTimer;
        EventManager.OnTimerCanceled += TimerCanceled;
        EventManager.OnPlayerChangeHpNotHiFrame += LivesChanges;
        EventManager.OnKeyCollected += KeyCollected;
        EventManager.OnPlayerWin += GameWin;
    }

    private void OnDisable()
    {
        EventManager.OnPause -= OnPauseMenu;
        EventManager.OnResume -= Resume;
        EventManager.OnResetStarted -= StartResetTimer;
        EventManager.OnResetCanceled -= CancelResetTimer;
        EventManager.OnPlayerDeath -= GameOver;
        EventManager.OnEnterDialogue -= ReadDialog;
        EventManager.OnExitDialogue -= ExitDialog;
        EventManager.OnTimerStarted -= StartTimer;
        EventManager.OnTimerCanceled -= TimerCanceled;
        EventManager.OnPlayerChangeHpNotHiFrame -= LivesChanges;
        EventManager.OnKeyCollected -= KeyCollected;
        EventManager.OnPlayerWin -= GameWin;
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
        loseScreen.SetActive(false);
        winScreen.SetActive(false);
        livesIndex = 4;
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
        InputManager.SwitchToPlayerInput();
        
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
    void GameOver()
    {
        loseScreen.SetActive(true);
        InputManager.SwitchToMenuInput();
        InputManager.actionMap.UI_Toggle.Disable();
        EventSystem.current.SetSelectedGameObject(LoseScreenButton);
    }
    
    void GameWin()
    {
        winScreen.SetActive(true);
        InputManager.SwitchToMenuInput();
        InputManager.actionMap.UI_Toggle.Disable();
        EventSystem.current.SetSelectedGameObject(WinScreenButton);
    }

    public void PerformReset()
    {
        SceneManager.LoadScene(0);
    }
    
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
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

    public void Quit()
    {
        Application.Quit();
    }
}
    

