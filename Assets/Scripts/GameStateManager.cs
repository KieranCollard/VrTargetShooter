using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//managing object for any game - state changes required
public class GameStateManager : MonoBehaviour
{

    public void Reload()
    {
        Debug.Log("Reloading current level");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    ///TODO load other scenesetc.
}