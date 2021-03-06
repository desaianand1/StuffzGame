﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{

    public float moveSpeed = 5f;
    public int current_axis = 0;
    public Rigidbody2D rigidBody;
	public Animator animator;
    Vector2 movementVector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame (don't put anything physics related here)
    void Update()
    {
        // Input
        //if (current_axis == 1)
        //{
            /*if (movementVector.x != Input.GetAxisRaw("Horizontal"))
            {
                movementVector.y = 0;
                movementVector.x = Input.GetAxisRaw("Horizontal");
                current_axis = 0;
            }
            if (movementVector.y != Input.GetAxisRaw("Vertical"))
            {
                movementVector.x = 0;
                movementVector.y = Input.GetAxisRaw("Vertical");
                current_axis = 1;
            }
			*/
	
        /*}
        if (current_axis == 0)
        {
            if (movementVector.y != Input.GetAxisRaw("Vertical"))
            {
                movementVector.x = 0;
                movementVector.y = Input.GetAxisRaw("Vertical");
                current_axis = 1;
            }
            if (movementVector.x != Input.GetAxisRaw("Horizontal"))
            {
                movementVector.y = 0;
                movementVector.x = Input.GetAxisRaw("Horizontal");
                current_axis = 0;
            }
        }*/
			movementVector.x = Input.GetAxisRaw("Horizontal");
			movementVector.y = Input.GetAxisRaw("Vertical");
			 if (movementVector != Vector2.zero)
			{
				animator.SetFloat("Horizontal", movementVector.x);
				animator.SetFloat("Vertical", movementVector.y);
			}
			animator.SetFloat("Speed", movementVector.sqrMagnitude);
    }

    // Like Update() but executed on fixed timer (independent of framerate)
    void FixedUpdate()
    {
        // Movement
        rigidBody.MovePosition(rigidBody.position + movementVector * moveSpeed * Time.fixedDeltaTime);

    }
}
