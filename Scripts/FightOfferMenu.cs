using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightOffersMenu : MonoBehaviour
{
    public int sceneToLoad;
    // Start is called before the first frame update
    public void BackToGymMenu()
    {
        SceneManager.LoadScene("GymManagement");
        Debug.Log("New Scene Loaded");
    }
}
