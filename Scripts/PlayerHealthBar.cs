using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    FighterStats fighterStatSheet;
    private Image HealthGreen;
    public int health = maxHealth;
    private const int maxHealth = 300;
    // Start is called before the first frame update
    void Start()
    {
        fighterStatSheet = GameObject.Find("Player").GetComponent<FighterStats>();
        
        HealthGreen = GetComponent<Image>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        HealthGreen.fillAmount = health / maxHealth;
    }
}
