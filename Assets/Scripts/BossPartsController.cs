using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossPartsController : MonoBehaviour
{
    [SerializeField] private float health = 200f;

    [SerializeField] private GameObject projectile;

    [SerializeField] private GameObject player;

    private bool canTakeDamage = false;
    private bool canAttack = false;

    private SpriteRenderer partSprite;

    private Quaternion targetRotation;
    private float strength = .5f;

    private void Awake ()
    {
        partSprite = GetComponent<SpriteRenderer>();
    }

    public void Activate ()
    {
        canTakeDamage = true;
        canAttack = true;
    }

    private void Update ()
    {
        if (health <= 0f)
        {
            Die();
        }

        LookAtPlayer ();
    }

    public void Die ()
    {
        Destroy(this.gameObject, 0.2f);
    }

    public void TakeDamage ()
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            health -= 40f;
            StartCoroutine(DamageCooldown(2.0f));
        }
    }

    IEnumerator DamageCooldown (float duration)
    {
        partSprite.DOColor(new Color(1f, 1f, 1f, 1f), 2f).From(0f); 
        yield return new WaitForSeconds(duration);
        canTakeDamage = true;
    }

    IEnumerator FireCooldown (float duration)
    {
        yield return new WaitForSeconds(duration);
        canAttack = true;
    }

    private void LookAtPlayer ()
    {
        float mouseRotation = (Mathf.Atan2(player.transform.position.x - this.transform.position.x, player.transform.position.y - this.transform.position.y) * Mathf.Rad2Deg) * -1f;
        transform.rotation = Quaternion.Euler(0f, 0f, mouseRotation);

        // Vector3 playerDirection = player.transform.position;
        // float newRotation = (Mathf.Atan2((playerDirection.x - this.transform.position.x), (playerDirection.y - this.transform.position.y)));
        //targetRotation = Quaternion.FromToRotation(transform.position, player.transform.position);
        // transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
        // transform.LookAt(player.transform);
        // strength = Mathf.Min(strength * Time.deltaTime, 1);
        // transform.rotation = targetRotation;
        // transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, strength);

        if (canAttack)
            FireProjectile();

    }

    private void FireProjectile ()
    {
        canAttack = false;
        Vector3 playerDirection = player.transform.position;
        GameObject currentProjectile = Instantiate(projectile, this.transform.position, this.transform.rotation);
        Rigidbody2D projectileBody = currentProjectile.GetComponent<Rigidbody2D>();
        projectileBody.AddForce(currentProjectile.transform.up * 10f, ForceMode2D.Impulse);
        StartCoroutine(FireCooldown(Random.Range(0.2f, 0.4f)));
    }
}
