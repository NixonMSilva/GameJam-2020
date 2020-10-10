using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField] private GameObject firePoint;
    [SerializeField] private GameObject projectile;

    [SerializeField] private float projectileSpeed = 15.0f;

    public void Fire (float angle)
    {
        GameObject currentProjectile = Instantiate(projectile, firePoint.transform.position, Quaternion.identity * Quaternion.Euler(0f, 0f, angle - 90f));
        Rigidbody2D projectileBody = currentProjectile.GetComponent<Rigidbody2D>();
        projectileBody.AddForce(currentProjectile.transform.up * projectileSpeed, ForceMode2D.Impulse);
    }
}
