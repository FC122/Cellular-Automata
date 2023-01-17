using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Navigate : MonoBehaviour
{
    // Start is called before the first frame update
   
    public void LoadCaveGeneration()
    {
        SceneManager.LoadScene("CaveGeneration");
    }

    public void LoadGameOfLife()
    {
        SceneManager.LoadScene("GameOfLife");
    }

    public void LoadForestFire()
    {
        SceneManager.LoadScene("ForestFire");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("UI");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
