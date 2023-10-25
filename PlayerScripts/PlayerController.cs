using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalInput;
    public float maxXRange = 20.0f;
    public float minXRange = 0.0f;

    public Transform AttackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    Enemy enemy;
    FighterStats fighterStatSheet;
    public bool headKicking;
    public bool heavyPunching;
    public bool isBlocking;
    public Animator anim;


    // Start is called before the first frame update
    private void Start()
    {
        fighterStatSheet = GameObject.Find("Player").GetComponent<FighterStats>();
        enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
        fighterStatSheet.currentHealth = fighterStatSheet.maxHealth;
        heavyPunching = false;
        headKicking = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        Vector2 newPosition = transform.position + Vector3.right * horizontalInput * Time.deltaTime * (fighterStatSheet.speed);

        newPosition.x = Mathf.Clamp(newPosition.x, minXRange, maxXRange);

        transform.position = newPosition;

        if (Input.GetKeyDown("mouse 0"))
        {
            Jab();
        }
        if (Input.GetKeyDown("mouse 1"))
        {
            PowerHand();
            heavyPunching = true;
        }
        if (Input.GetKeyUp("mouse 1"))
        {
            heavyPunching = false;
        }
        if (Input.GetKeyDown("e"))
        {
            LeadLegHeadKick();
            headKicking = true;
        }
        if (Input.GetKeyUp("e"))
        {
            LeadLegHeadKick();
            headKicking = false;
        }
        if (Input.GetKeyDown("space"))
        {
            isBlocking = true;
        }
        if (Input.GetKeyUp("space"))
        {
            isBlocking = false;
        }
    }

    public void Jab()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(fighterStatSheet.handsDamage);
        }
    }
    public void PowerHand()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(fighterStatSheet.handsDamage * 3);
        }
    }
    public void LeadLegHeadKick()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(fighterStatSheet.kickDamage * 2);
        }
    }

    public void Frontkick()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(fighterStatSheet.kickDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, attackRange);
    }
    public void TakeDamage(int damage)
    {
        if (isBlocking == true)
        {

        }
        else
        {
            fighterStatSheet.currentHealth -= damage;
        }


        if (enemy.headKicking == true)
        {

            anim.Play("KarateHeadKicked");
        }
        else if (enemy.heavyPunching == true)
        {
            //anim.Play("KarateHeavyPunchHit")
            anim.Play("KarateHeadHitLight");
        }
        else
        {
            anim.Play("KarateHeadHitLight");
        }

        if (fighterStatSheet.currentHealth <= 0)
        {
            Defeated();
        }
    }
    void Defeated()
    {
        //anim.Play("Knockout")
        Debug.Log("Defeated");

        anim.SetBool("playerDefeated", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}


   
