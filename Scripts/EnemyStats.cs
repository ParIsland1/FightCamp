using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    //Fighter physical stats:
    public int handsDamage = 1;
    public int kickDamage = 2;
    public int maxHealth = 300;
    public int currentHealth;
    public int recoveryRate = 10;
    public int speed = 1;
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
