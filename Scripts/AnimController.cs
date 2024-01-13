using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    FighterStats fighterStatSheet;
    public Animator anim;
    public bool isBlocking = false;
    
    // Start is called before the first frame update
    void Start()
    {
        fighterStatSheet = GameObject.Find("Player").GetComponent<FighterStats>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Play Jab Animation
        if (Input.GetKeyDown("mouse 0"))
        {
            anim.Play("KarateJab");
        }
        //Play Block Animation
        if (Input.GetKeyDown("space"))
        {
            anim.Play("KarateBlock");
            isBlocking = true;
        }
        if (Input.GetKeyUp("space"))
        {
            anim.Play("IdleBounce");
            isBlocking = false;
        }

        //Play PowerHand Animation
        if (Input.GetKeyDown("mouse 1"))
        {
            anim.Play("KaratePowerHand");
        }
        //Play HeadKick Animation
        if (Input.GetKeyDown("e"))
        {
            anim.Play("KarateLeadLegHeadKick");
        }
        //Play FrontKick Animation

        //Play WalkForward Animation
        if (Input.GetKeyDown("d"))
        {
            anim.Play("KarateWalk");
        }
        //Stop WalkForward Animation
        if (Input.GetKeyUp("d"))
        {
            anim.Play("IdleBounce");
        }
        //Play WalkBack Animation
        if (Input.GetKeyDown("a"))
        {
            anim.Play("KarateWalkBack");
        }
        //Stop WalkBack Animation
        if (Input.GetKeyUp("a"))
        {
            anim.Play("IdleBounce");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isBlocking == true)
        {
            
        } else
        {
            fighterStatSheet.currentHealth -= damage;

            anim.Play("KarateHeadHitLight");
        }

        if (fighterStatSheet.currentHealth <= 0)
        {
            Defeated();
        }
    }
    void Defeated()
    {
        //Defeated animation (ko)
        Debug.Log("Defeated");

        anim.SetBool("isDefeated", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
