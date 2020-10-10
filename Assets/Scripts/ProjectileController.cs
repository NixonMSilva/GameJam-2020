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
        // Destroy the object if it isn't the player
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
