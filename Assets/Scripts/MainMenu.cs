using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Options()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void Menu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(2);
    }

    public void Level1()
    {
        SceneManager.LoadSceneAsync(3);
    }

    public void Level2()
    {
        SceneManager.LoadSceneAsync(4);
    }

    public void Level3()
    {
        SceneManager.LoadSceneAsync(5);
    }

    public void Level4()
    {
        SceneManager.LoadSceneAsync(6);
    }

    public void Level5()
    {
        SceneManager.LoadSceneAsync(7);
    }

    public void Level1B()
    {
        SceneManager.LoadSceneAsync(8);
    }

    public void Level2B()
    {
        SceneManager.LoadSceneAsync(9);
    }

    public void Level3B()
    {
        SceneManager.LoadSceneAsync(10);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
