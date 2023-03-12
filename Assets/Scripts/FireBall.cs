using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public GameObject damagaeEffect;

   private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Instantiate(damagaeEffect, transform.position, damagaeEffect.transform.rotation);
            Destroy(gameObject);
        }
    }
}
