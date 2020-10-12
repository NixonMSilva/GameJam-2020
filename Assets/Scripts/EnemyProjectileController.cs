using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : ProjectileController
{
    [SerializeField] private float projectileDamage = 50f;

    private void Awake ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        // Damages the player if hits him
        if (collision.gameObject.CompareTag("Player"))
        {
            audioManager.PlaySound("DmgPlayer");
            collision.GetComponent<HealthController>().ChangeHealth(-projectileDamage);
            Destroy(this.gameObject, 0.05f);
        }
        else if (collision.gameObject.CompareTag("Block"))
        {    
            audioManager.PlaySound("DmgEnemy");
            Destroy(this.gameObject, 0.05f);
        }
    }
}
