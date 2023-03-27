using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private string currentState = "Idle";
    private Transform target;
    public float chaseRange = 5;
    public Animator animator;
    public float speed = 3;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
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

            if(target.position.x > transform.position.x) //idi prema desno
            {
                transform.Translate(transform.right * speed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            else //lijevo
            {
                transform.Translate(transform.right * -1 * speed * Time.deltaTime); //-transform.right je isto sto i transform.left
                transform.rotation = Quaternion.identity;
            }
        }
    }
}
