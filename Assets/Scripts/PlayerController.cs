using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    private Animator characterAnimator;
    private Vector3 direction;
    private bool isWalking;

    [Header("Configurações do jogador")]
    public float movementSpeed = 3f;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        characterAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate() {
        CharacterMovement();
    }

    private void Update()
    {
        CharacterAnimations();
    }

    void CharacterMovement(){
        direction = Vector3.zero;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0, vertical).normalized;

        if(direction.magnitude > 0.1f)
        {
            isWalking = true;

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        }
        else
        {
            isWalking = false;
        }

        characterController.Move(direction * movementSpeed * Time.deltaTime);
    }

    void CharacterAnimations()
    {
        characterAnimator.SetBool("isWalking", isWalking);

        if (Input.GetButtonDown("Fire1"))
        {
            characterAnimator.SetTrigger("Attack");
        }
    }

}
