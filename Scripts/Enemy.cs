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
    public bool jabbing;
    public bool isAttacking;
    public float cooldown = 2f;
    private float lastAttackedAt = -9999f;

    public Transform player;
    
    public float distanceToMaintain;
    public float minDistance = 1.8f;
    public float maxDistance = 5.0f;
    public float changeDistanceInterval = 5.0f;
    public float responseDelay = 2.0f;
    public bool playerIsMoving;
    public float jabAttackChance = 0.5f;
    public float powerAttackChance = 0.35f;
    public float kickAttackChance = 0.15f;
    public bool isMoving;
    
    

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
        

    }

    public void Update()
  {

        headKicking = false;
        heavyPunching = false;
        jabbing = false;

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

            // Move the AI towards the desired position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, enemyStatSheet.speed * Time.deltaTime);

            // Optionally, you can also flip the AI's sprite to face the player
            if (direction.x < 0)
            {
                // AI is to the left of the player, flip the sprite to face right
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x > 0)
            {
                // AI is to the right of the player, flip the sprite to face left
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            if (playerIsMoving == true)
            {
                // Apply the delay before AI starts following
                StartCoroutine(DelayedMove(targetPosition, responseDelay));
            }
            if (transform.position != targetPosition)
            {
                isMoving = true; // AI is moving
            }
            else
            {
                isMoving = false; // AI is not moving
            }
            if (isMoving == true)
            {
                anim.Play("KarateWalk");
            } 
        }
        //Distance from player
        float actualDistance = Vector3.Distance(player.position, transform.position);
        
        if (actualDistance < 3.0f && Random.value <= jabAttackChance)
        {
            while (Time.time > lastAttackedAt + cooldown)
            {
                jabbing = true;
                Jab();
                anim.Play("KarateJab");
                lastAttackedAt += cooldown;
                isAttacking = true;
                
            }
        }
        if (actualDistance < 3.0f && Random.value <= powerAttackChance)
        {
            heavyPunching = true;
            while (Time.time > lastAttackedAt + cooldown)
            {
                
                PowerHand();
                anim.Play("KaratePowerHand");
                lastAttackedAt += cooldown;
                isAttacking = true;
               
            }
        }
        if (actualDistance < 3.0f && Random.value <= kickAttackChance)
        {
            headKicking = true;
            while (Time.time > lastAttackedAt + cooldown)
            {
                
                LeadLegHeadKick();
                anim.Play("KarateLeadLegHeadKick");
                lastAttackedAt += cooldown;
                isAttacking = true;
                
            }
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
        }
    }
    public void PowerHand()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(EnemyAttackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(enemyStatSheet.handsDamage * 3);
        }
    }
    public void LeadLegHeadKick()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(EnemyAttackPoint.position, attackRange, playerLayers);

        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController>().TakeDamage(enemyStatSheet.kickDamage * 2);
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
        //calculate damage
        if(isBlocking == true)
        {
            damage = 0;
        } else
        {
            enemyStatSheet.currentHealth -= damage;
        }
        
        //Play animations
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
        anim.Play("KarateKnockedOut");
        Debug.Log("Opponet defeated");

        anim.SetBool("isDefeated", true);

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
