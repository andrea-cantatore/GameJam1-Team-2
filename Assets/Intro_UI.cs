using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro_UI : MonoBehaviour
{
    [SerializeField] GameObject SettingsMenu;
    [SerializeField] GameObject ControlScreen;
    [SerializeField] int sceneIndex = 1;

    private void Start()
    {
        SettingsMenu.SetActive(false);
        ControlScreen.SetActive(false);
        InputManager.SwitchToMenuInput();
    }

    public void OpenSettings()
    {
        SettingsMenu.SetActive(true);
        ControlScreen.SetActive(false);
    }

    public void CloseSettings()
    {
        SettingsMenu.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void OpenControlScreen()
    {
        ControlScreen.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
