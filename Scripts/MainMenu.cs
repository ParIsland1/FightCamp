using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public int sceneToLoad;
    // Start is called before the first frame update
    public void NewGame()
    {
        SceneManager.LoadScene("Cage");
        Debug.Log("New Scene Loaded");
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("GymManagement");
        Debug.Log("New Scene Loaded");
    }

    // Update is called once per frame
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game!");
    }
}