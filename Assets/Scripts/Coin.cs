using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed = 100; //okretanje novèiæa u mjestu
   
    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) //skupljanje novèiæa
    {
        PlayerManager.numberOfCoins++;

        Destroy(gameObject);
    }
}
