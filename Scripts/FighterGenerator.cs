using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fighter", menuName = "Fighter")]
public class FighterGenerator : ScriptableObject
{
    public string fighterName;
    public string fightStyle;
   
    public int handsDamage;
    public int kickDamage;
    public int maxHealth;
    public int currentHealth;
    public int recoveryRate;
    public int speed;
    
    public bool goodGuy;
    public int fans;
    public int haters;
    public int experience;
    public int currentExperience;

    public Sprite artwork;

}

