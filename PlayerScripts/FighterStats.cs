using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStats : MonoBehaviour
{
    //Fighter physical stats:
    public int handsDamage = 1;
    public int kickDamage = 5;
    public int maxHealth = 100;
    public int currentHealth;
    public int recoveryRate = 10;
    public int speed = 2;
    //Fighter personality stats:
    public bool goodGuy = true;
    public int fans;
    public int haters;
    public int experience;
    public int currentExperience;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
