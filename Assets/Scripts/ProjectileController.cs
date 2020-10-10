using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    void Start ()
    {
        Destroy(this.gameObject, 3f);
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        // Destroy the projectile object if it isn't the player
        if (!collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject, 0.05f);
            }
            Destroy(this.gameObject, 0.05f);
        }
    }
}
