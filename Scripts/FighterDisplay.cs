using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FighterDisplay : MonoBehaviour
{
    public FighterGenerator fighterGenerator;

    public TMP_Text FighterName;
    public Image FighterSprite;

    public TMP_Text FightStyle;

    // Start is called before the first frame update
    void Start()
    {
        FighterName.text = fighterGenerator.fighterName;
        FightStyle.text = fighterGenerator.fightStyle;
        FighterSprite.sprite = fighterGenerator.artwork;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
