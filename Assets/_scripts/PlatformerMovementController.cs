using System.Collections;
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

    public Rigidbody myRigidbody;
    private float horizontalInput;
    private Vector3 inputVector;
    public Animator animator;
    public string movementSpeedParameterName = "movementSpeed";
    public string animatorJumpParameterName = "jumping";
    [SerializeField]
    private bool canMove = true;
    [SerializeField]
    private Transform cameraTransform;

    //[SerializeField] private bool isGrounded;

    // control de si toco el salto antes de llegar al piso para volver a saltar la animacion
    public LayerMask groundLayer;
    [SerializeField] private float jumpCommandGracePeriod = 0.3f;
    private float currentTimerJumpCommand = 0.3f;

    [SerializeField] Entities.Player _player;

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
            var grounded = Physics.OverlapBox(transform.position, transform.localScale * 0.5f, Quaternion.identity, groundLayer);
            bool isJumpFrame = false;

            if (Input.GetButtonDown(jumpButton))
            {
                _player.SetAnimState(animatorJumpParameterName, true);
                currentTimerJumpCommand = jumpCommandGracePeriod;
                var pendingJump = currentTimerJumpCommand > 0;

                //Debug.Log("grounded " + grounded.Length);

                if (grounded.Length > 0 && pendingJump)
                {
                    myRigidbody.AddForce(jumpForce * Vector3.up, jumpForceMode);
                    currentTimerJumpCommand = 0;
                    isJumpFrame = true;
                }
            }

            if (grounded.Length > 0 && !isJumpFrame)
            { StopJumping(); }
        }
    }

    public void StopJumping()
    {
        _player.SetAnimState(animatorJumpParameterName, false);
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
