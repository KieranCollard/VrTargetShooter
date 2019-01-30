using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//managing object for any game - state changes required
public class GameStateManager : MonoBehaviour
{

    public string MainMenuName = "MainMenu";
    public string GameName = "Level01";
    public string EndGameScene = "EndOfGame";
    public void Reload()
    {
        Debug.Log("Reloading current level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MainMenuName);
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene(GameName);
    }
    public void LoadEndGameScene()
    {
        SceneManager.LoadScene(EndGameScene);
    }
}