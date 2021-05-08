﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovementController : MonoBehaviour
{
    public string horizontalAxis = "Horizontal";
    public string verticalAxis = "Vertical";
    public float movementSpeed = 2f;
    public float runMovementSpeed = 8f;
    public string jumpButton = "Jump";
    public float jumpForce = 10f;
    public ForceMode jumpForceMode = ForceMode.VelocityChange;
    public string attackButton = "Fire1";
    public string attackTriggerName = "Attack";
    public Rigidbody myRigidbody;
    private float horizontalInput;
    private Vector3 inputVector;
    public Animator animator;
    public string movementSpeedParameterName = "movementSpeed";
    [SerializeField]
    private bool canMove = true;
    [SerializeField]
    private Transform cameraTransform;

    //[SerializeField] private bool isGrounded;

    // control de si toco el salto antes de llegar al piso para volver a saltar la animacion
    public LayerMask groundLayer;
    [SerializeField] private float jumpCommandGracePeriod = 0.3f;
    private float currentTimerJumpCommand = 0.3f;


    void Update()
    {


        if (canMove)
        {
            inputVector = Input.GetAxis(horizontalAxis) * cameraTransform.right;

            Vector3 fixedCameraForward = cameraTransform.forward;
            fixedCameraForward.y = 0;
            fixedCameraForward.Normalize();

            inputVector += Input.GetAxis(verticalAxis) * fixedCameraForward;

            if (inputVector.magnitude > 1f)
            {
                inputVector.Normalize();
            }

            animator.SetFloat(movementSpeedParameterName, inputVector.magnitude);

            currentTimerJumpCommand -= Time.deltaTime;

            if (Input.GetButtonDown(jumpButton))
            {
                currentTimerJumpCommand = jumpCommandGracePeriod;
                var pendingJump = currentTimerJumpCommand > 0;
                var grounded = Physics.OverlapBox(transform.position, transform.localScale*0.5f, Quaternion.identity, groundLayer);
                Debug.Log("grounded " + grounded.Length);
                
                if (grounded.Length > 0 && pendingJump)
                {
                    myRigidbody.AddForce(jumpForce * Vector3.up, jumpForceMode);
                    currentTimerJumpCommand = 0;
                }
            }


            if (Input.GetButtonDown(attackButton))
            {
                animator.SetTrigger(attackTriggerName);
            }
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            if (inputVector.sqrMagnitude > 0f)
            {
                transform.LookAt(transform.position + inputVector);
            }

            myRigidbody.MovePosition(transform.position + inputVector * (movementSpeed * Time.deltaTime));
        }
    }
}
