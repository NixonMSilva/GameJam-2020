using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] float maxHealth = 100f;

    private float health;
    private UIBarController barController;    

    private void Awake ()
    {
        health = maxHealth;
        barController = healthBar.GetComponent<UIBarController>();
    }

    private void Update()
    {
        barController.SetSlideValue(health);
        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die ()
    {
        gameOverPanel.SetActive(true);
        gameObject.SetActive(false);
        GameObject.Find("Aim").SetActive(false);
    }

    public void ChangeHealth (float amount)
    {
        health += amount;
        if (health > 100f)
            health = 100f;
    }
}
