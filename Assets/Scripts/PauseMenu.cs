using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public MenuManager m_menuManager;
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
    }

    public void PauseGame()
    {
        isPaused = true;
        MenuManager.sharedInstance.HideHUD();
        MenuManager.sharedInstance.ShowPauseMenu();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        MenuManager.sharedInstance.HidePauseMenu();
        MenuManager.sharedInstance.ShowHUD();
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        GameManager.sharedInstance.StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        PauseCheck();
    }

    void PauseCheck()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!isPaused)
                {
                    PauseGame();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
        
    }
}
