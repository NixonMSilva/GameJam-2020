using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    protected AudioManager audioManager;

    private void Awake ()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    private void Start ()
    {
        Destroy(this.gameObject, 3f);
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        // Destroy the projectile object if it isn't the player
        if (!collision.gameObject.CompareTag("Player"))
        {
            audioManager.PlaySound("DmgEnemy");
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject, 0.05f);
            }
            if (collision.gameObject.CompareTag("Block"))
            {
                Destroy(this.gameObject, 0.05f);
            }
            if (collision.gameObject.CompareTag("BossPart"))
            {
                BossPartsController parts = collision.GetComponent<BossPartsController>();
                if (parts != null)
                    parts.TakeDamage();
            }
        }
    }
}
