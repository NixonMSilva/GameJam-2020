﻿using System.Collections;
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

    private float horizontalMove = 0f;
    private float runSpeed = 40f;

    private bool isJumping = false;

    private Vector2 mousePosition;
    private float mouseRotation;

    private void Awake ()
    {
        controller = GetComponent<CharacterController2D>();
        weapon = GetComponent<WeaponController>();

        crosshairBody = crosshair.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Handles horizontal input
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        
        // Handles jump input
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
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
                mouseRotation =  - mouseRotation + 180f;
            }
        }
        else
        {
            // If the player is facing left
            if (mouseRotation > 270f)
            {
                mouseRotation =  - mouseRotation + 180f;
            }
            else if (mouseRotation < 90f)
            {
                mouseRotation = 180f - mouseRotation;
            }
        }

        crosshairBody.MovePosition(mousePosition);
        aimIndicator.transform.rotation = Quaternion.Euler(0f, 0f, mouseRotation);

        /* Debug.Log("Player Position: " + this.transform.position);
        Debug.Log("Mouse Position:" + mousePosition);
        Debug.Log("Rotation: " + mouseRotation + "°"); */

        // Handles the firing routine
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Rotation: " + mouseRotation);
            weapon.Fire(mouseRotation);
        }
    }

    private void FixedUpdate ()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, isJumping);
        isJumping = false;
    }
}