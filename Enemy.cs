using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int enemyHealth = 3;
    public bool drop;
    public GameObject coinLoot;

    

 public void TakeDamage (int damage)
    {
    
        enemyHealth -= damage;
        if (enemyHealth <=0)

        {
            Die();
        }
    }


    void Die ()
    {

       Destroy(gameObject);
        if(drop==true)
        {
            spawnCoin();
        }
        
    }

    void spawnCoin()
    {


        Instantiate(coinLoot, transform.position, transform.rotation);
     }
}

