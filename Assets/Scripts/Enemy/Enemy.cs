using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private string currentState = "Idle";
    private Transform target;
    
    public float chaseRange = 7;
    public Animator animator;
    public float speed = 6;
    public float attackRange = 1;
    public int HP;
    public int maxHP;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        HP = maxHP;
    }


    void Update() 
    {
        float distance = Vector3.Distance(transform.position, target.position); 

        if(currentState == "Idle")
        {
            if (distance < chaseRange)
                currentState = "Chase";
        }

        else if(currentState == "Chase")
        {
            animator.SetTrigger("chase");
            animator.SetBool("isAttacking", false);

            if(distance < attackRange)
            {
                currentState = "Attack";
            }

            if(target.position.x < transform.position.x) //idi prema desno
            {
                
                transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, -90, 0);
            }

            else //lijevo
            {
                transform.Translate(transform.right * -1 * speed * Time.deltaTime); //-transform.right je isto sto i transform.left
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }

        else if(currentState == "Attack")
        {
            animator.SetBool("isAttacking", true);

            if (distance > attackRange)
            {
                currentState = "Chase";
            }
        }
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;

        currentState = "Chase";

        if(HP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("isDead");

        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;
    }
}
