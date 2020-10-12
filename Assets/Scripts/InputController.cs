using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] private Camera cameraObj;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject aimIndicator;
    private Rigidbody2D crosshairBody;

    private CharacterController2D controller;
    private WeaponController weapon;
    private EnergyController energy;

    private float horizontalMove = 0f;
    private float runSpeed = 40f;

    private bool isJumping = false;
    private bool isHealing = false;

    private Vector2 mousePosition;
    private float mouseRotation;

    private Animator anim;

    private AudioManager audioManager;

    private void Awake ()
    {
        controller = GetComponent<CharacterController2D>();
        weapon = GetComponent<WeaponController>();
        energy = GetComponent<EnergyController>();
        anim = GetComponent<Animator>();

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        crosshairBody = crosshair.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Prevent from moving if player is healing
        if (!isHealing)
        {
            // Handles horizontal input
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            anim.SetFloat("speed", Mathf.Abs(horizontalMove));

            // Handles jump input
            if (Input.GetButtonDown("Jump"))
            {
                isJumping = true;
                audioManager.PlaySound("Jump");
            }

            // Handles mouse movement and crosshair movement
            mousePosition = cameraObj.ScreenToWorldPoint(Input.mousePosition);
            mouseRotation = (Mathf.Atan2(mousePosition.x - this.transform.position.x, mousePosition.y - this.transform.position.y) * Mathf.Rad2Deg - 90f) * -1f;
            if (mouseRotation < 0f)
            {
                mouseRotation += 360f;
            }

            // Normalize the mouse movement to the position the player is facing
            if (this.transform.localScale.x > 0f)
            {
                // If the player is facing right
                if ((mouseRotation > 90f) && (mouseRotation <= 180f))
                {
                    mouseRotation = 180f - mouseRotation;
                }
                else if ((mouseRotation > 180f) && (mouseRotation < 270f))
                {
                    mouseRotation = -mouseRotation + 180f;
                }
                aimIndicator.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                // If the player is facing left
                if (mouseRotation > 270f)
                {
                    mouseRotation = -mouseRotation + 180f;
                }
                else if (mouseRotation < 90f)
                {
                    mouseRotation = 180f - mouseRotation;
                }
                aimIndicator.transform.localScale = new Vector3 (1f, -1f, 1f);
            }

            crosshairBody.MovePosition(mousePosition);
            aimIndicator.transform.rotation = Quaternion.Euler(0f, 0f, mouseRotation);

            // Handles the firing routine
            if ((energy.GetEnergy() >= 10f) && (Input.GetButtonDown("Fire1")))
            {
                weapon.Fire(mouseRotation);
                audioManager.PlaySound("EnemySuperBolt");
                energy.ChangeEnergy(-10f);
            }
        }
    }

    private void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }

    public void SetHealing (bool healing)
    {
        isHealing = healing;
    }
}
