using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState {menu, inGame, gameOver};

public class GameManager : MonoBehaviour
{
    public int collectedCoins = 0;
    public int collectedHealthPotion = 0;
    public int collectedManaPotion = 0;
    public GameState currentGameState;

    public static GameManager sharedInstance;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Menu();
    }

    //Este metodo se ejecutara cuando el jugador comience una partida
    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    //Este método se ejecutara cuando el jugador muera
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    //Este metodo se ejecutara para regresar al menu
    public void Menu()
    {
        SetGameState(GameState.menu);
    }

    private void SetGameState(GameState newGameState)
    {
        switch (newGameState)
        {
            case GameState.menu:
                MenuManager.sharedInstance.ShowMainMenu();
                MenuManager.sharedInstance.HideHUD();
            break;
            case GameState.inGame:
                LevelManager.sharedInstance.RemoveAllLevelBlocks();
                LevelManager.sharedInstance.GenerateInitialBlocks();
                PlayerController.sharedInstance.StartGame();
                MenuManager.sharedInstance.HideMainMenu();
                MenuManager.sharedInstance.HideDeathMenu();
                MenuManager.sharedInstance.ShowHUD();
                break;
            case GameState.gameOver:
                MenuManager.sharedInstance.ShowDeathMenu();
                MenuManager.sharedInstance.HideHUD();
            break;
        }

        this.currentGameState = newGameState;
    }
}
