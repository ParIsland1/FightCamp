using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    EnemyStats enemyStatSheet;
    public LayerMask playerLayers;
    public Transform EnemyAttackPoint;
    public float attackRange = 0.5f;
    
    public Animator anim;
    PlayerController playerControl;
    
    public bool isBlocking;
    public bool headKicking;
    public bool heavyPunching;
    
    public Transform player;
    
    public float distanceToMaintain;
    public float minDistance = 1.8f;
    public float maxDistance = 5.0f;
    public float changeDistanceInterval = 5.0f;
    public float responseDelay = 2.0f;
    public bool playerIsMoving;
    public float attackChance = 0.5f;
    public bool isAttacking;

    // Start is called before the first frame update
    private void Start()
    {
        enemyStatSheet = GameObject.Find("Enemy").GetComponent<EnemyStats>();
        playerControl = GameObject.Find("Player").GetComponent<PlayerController>();

        anim = GetComponent<Animator>();
        enemyStatSheet.currentHealth = enemyStatSheet.maxHealth;
        distanceToMaintain = Random.Range(minDistance, maxDistance);
        StartCoroutine(ChangeDistanceRoutine());
        playerIsMoving = false;
        isAttacking = false;
    }

    public void Update()
  {
        if (Input.GetKeyDown("d"))
        {
            playerIsMoving = true;
        } else if (Input.GetKeyDown("a"))
        {
            playerIsMoving = true;
        }
        if (Input.GetKeyUp("d"))
        {
            playerIsMoving = false;
        }
        else if (Input.GetKeyUp("a"))
        {
            playerIsMoving = false;
        }
        //if(player.position= (18,0,0)) dont move back
        if (player != null) // Ensure the player reference is set
        {
            // Calculate the direction to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the AI only along the X-axis
            direction.y = 0; // Set the Y-component of the direction to 0

            // Calculate the desired position based on the desired distance
            Vector3 targetPosition = player.position - direction * distanceToMaintain;

         
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, enemyStatSheet.speed * Time.deltaTime);

          
            if (direction.x < 0)
            {
                
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x > 0)
            {
                
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (playerIsMoving == true)
            {
                // Apply the delay before AI starts following
                StartCoroutine(DelayedMove(targetPosition, responseDelay));
            }
        }
        if (distanceToMaintain < 3.0f && Random.value <= attackChance && isAttacking == false)
        {
            Jab();
            isAttacking = true;
        }
    }

    private IEnumerator ChangeDistanceRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(changeDistanceInterval);
            distanceToMaintain = Random.Range(minDistance, maxDistance);
        }
    }
    private IEnumerator DelayedMove(Vector3 targetPosition, float delay)
    {
        yield return new WaitForSeconds(delay);
        playerIsMoving = false;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, enemyStatSheet.speed * Time.deltaTime);
    }
    public void Jab()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(EnemyAttackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(enemyStatSheet.handsDamage);
            anim.Play("KarateJab");
        }
    }
    public void PowerHand()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(EnemyAttackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(enemyStatSheet.handsDamage);
        }
    }
    public void LeadLegHeadKick()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(EnemyAttackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(enemyStatSheet.kickDamage);
        }
    }

    public void Frontkick()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(EnemyAttackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(enemyStatSheet.kickDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (EnemyAttackPoint == null)
            return;

        Gizmos.DrawWireSphere(EnemyAttackPoint.position, attackRange);
    }



public void TakeDamage(int damage)
    {
        if(isBlocking == true)
        {
            
        } else
        {
            enemyStatSheet.currentHealth -= damage;
        }
        

        if (playerControl.headKicking == true)
        {
            
            anim.Play("KarateHeadKicked");
        } else if (playerControl.heavyPunching == true)
        {
            //anim.Play("KarateHeavyPunchHit")
            anim.Play("KarateHeadHitLight");
        } else 
        {
            anim.Play("KarateHeadHitLight");
        }

        if(enemyStatSheet.currentHealth <= 0)
        {    
            Defeated();
        }
    }
    void Defeated()
    {
        //anim.Play("Knockout")
        Debug.Log("Opponet defeated");

        anim.SetBool("isDefeated", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}