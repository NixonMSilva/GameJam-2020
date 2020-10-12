using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyController : EnemyController
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float projectileCooldown = 2f;

    private bool canFire = true;

    private void Update ()
    {
        // Ranged enemy attack routine
        if (CanSeePlayer())
        {
            // If the player is at the right of the enemy and the enemy is facing right
            // or if the player is at the left of the enemy and the enemy is facing left
            if ((player.transform.position.x > this.transform.position.x && isFacingRight)
                || (player.transform.position.x < this.transform.position.x && !isFacingRight))
            {
                if (canFire)
                    FireProjectile();
            }
        }
    }

    private void FireProjectile ()
    {
        canFire = false;
        playerDirection = player.transform.position;
        float projectileRotation = (Mathf.Atan2((playerDirection.x - this.transform.position.x), (playerDirection.y - this.transform.position.y)) * Mathf.Rad2Deg - 90f);
        GameObject currentProjectile = Instantiate(projectile, this.transform.position, Quaternion.identity * Quaternion.Euler(0f, 0f, projectileRotation - 90f));
        Rigidbody2D projectileBody = currentProjectile.GetComponent<Rigidbody2D>();
        projectileBody.AddForce(currentProjectile.transform.up * 10f, ForceMode2D.Impulse);
        StartCoroutine(Cooldown(projectileCooldown));
    }

    IEnumerator Cooldown (float time)
    {
        yield return new WaitForSeconds(time);
        canFire = true;
    }
}
