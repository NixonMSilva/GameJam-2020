using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MeditationController : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject rechargeTooltip;

    private EnergyController energy;
    private InputController playerInput;
    private Rigidbody2D playerBody;

    private AudioManager audioManager;

    private SpriteRenderer sprite;

    private void Awake ()
    {
        energy = player.GetComponent<EnergyController>();
        playerInput = player.GetComponent<InputController>();
        playerBody = player.GetComponent<Rigidbody2D>();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        sprite = GetComponent<SpriteRenderer>();
    }

    IEnumerator Start ()
    {
        yield return new WaitForSeconds(1f);
        Sequence s = DOTween.Sequence();
        s.Append(sprite.DOColor(new Color(255, 255, 255, 255), 2f));
        s.Append(sprite.DOColor(new Color(0, 255, 255, 255), 2f));
        s.SetLoops(-1, LoopType.Restart);
    }

    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            rechargeTooltip.SetActive(true);
    }

    private void OnTriggerExit2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            rechargeTooltip.SetActive(false);
    }


    private void OnTriggerStay2D (Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("Gotcha");
                if (energy.GetEnergy() < 100f)
                {
                    Meditate();
                }

            }
        }
    }

    private void Meditate ()
    {
        player.GetComponent<Rigidbody2D>().MovePosition(this.gameObject.transform.position);
        StartCoroutine(HealEnergy());
    }

    IEnumerator HealEnergy ()
    {
        playerInput.SetHealing(true);
        playerBody.gravityScale = 0f;
        playerBody.velocity = Vector2.zero;
        while (energy.GetEnergy() < 100f)
        {
            Debug.Log(energy.GetEnergy());
            yield return new WaitForSeconds(0.5f);
            audioManager.PlaySound("RestoreMana");
            energy.ChangeEnergy(25f);
        }
        playerBody.gravityScale = 4f;
        playerInput.SetHealing(false);
    }
}
