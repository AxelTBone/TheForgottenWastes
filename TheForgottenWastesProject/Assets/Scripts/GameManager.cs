using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject modeMenu;

    [Header("Main Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button modeButton;
    [SerializeField] private Button exitButton;

    [Header("Mode Menu Buttons")]
    [SerializeField] private Button normalButton;
    [SerializeField] private Button hardButton;
    [SerializeField] private Button backButton;

    [Header("Game Over Menu Buttons")]
    [SerializeField] private Button gmReturnButton;
    [SerializeField] private Button gmExitButton;

    private void Start()
    {
        if (playButton && modeButton && exitButton)
        {
            playButton.onClick.AddListener(PlayNormalGame);
            modeButton.onClick.AddListener(ModeMenu);
            exitButton.onClick.AddListener(QuitGame);
            backButton.onClick.AddListener(BackToMainMenu);
            normalButton.onClick.AddListener(PlayNormalGame);
            hardButton.onClick.AddListener(PlayHardGame);
        }
        else if (!(playButton && modeButton && exitButton))
        {
            gmReturnButton.onClick.AddListener(LoadMainMenuScene);
            gmExitButton.onClick.AddListener(QuitGame);
        }
    }

    private void PlayNormalGame()
    {
        FindObjectOfType<AudioManager>().Play("OnPlay");
        SceneManager.LoadScene("NormalMode1");
    }

    private void PlayHardGame()
    {
        FindObjectOfType<AudioManager>().Play("OnPlayHard");
        SceneManager.LoadScene("HardMode1");
    }

    private void ModeMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        mainMenu.SetActive(false);
        modeMenu.SetActive(true);
    }
    private void BackToMainMenu()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        mainMenu.SetActive(true);
        modeMenu.SetActive(false);
    }

    private void LoadMainMenuScene()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        SceneManager.LoadScene("Menu");
    }

    private void QuitGame()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_WEBPLAYER
        Application.OpenURL(webplayerQuitURL);
        #else
        Application.Quit();
        #endif
    }
}