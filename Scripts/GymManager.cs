using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GymManager : MonoBehaviour
{
    public int sceneToLoad;
    // Start is called before the first frame update
    public void FightOffers()
    {
        SceneManager.LoadScene("FightOffersMenu");
        Debug.Log("New Scene Loaded");
    }
}