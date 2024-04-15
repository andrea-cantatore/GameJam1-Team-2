using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Intro_UI : MonoBehaviour
{
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject PlayButton;
    [SerializeField] GameObject SettingsButon;
    [SerializeField] GameObject QuitButton;
    [SerializeField] GameObject Controlscreen;
    [SerializeField] GameObject BackButton;

    private void Start()
    {
        SettingsMenu.SetActive(false);
        Controlscreen.SetActive(false);
        InputManager.SwitchToMenuInput();
        EventSystem.current.SetSelectedGameObject(PlayButton);
    }

    public void OpenSettings()
    {
        SettingsMenu.SetActive(true);
        PlayButton.SetActive(false);
        QuitButton.SetActive(false); 
        SettingsButon.SetActive(false);
        //EventSystem.current.SetSelectedGameObject()
    }

    public void OpenControls()
    {
        Controlscreen.SetActive(true);
        EventSystem.current.SetSelectedGameObject(BackButton);
    }

    public void CloseSettings() 
    {
        SettingsMenu.SetActive(false);
        QuitButton.SetActive(true);
        PlayButton.SetActive(true);
        EventSystem.current.SetSelectedGameObject(PlayButton);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMainMenu()
    {
        PlayButton.SetActive(true);
        QuitButton.SetActive(true);
        SettingsButon.SetActive(true);
        Controlscreen.SetActive(false) ;
        SettingsMenu.SetActive(false);
    }


}
