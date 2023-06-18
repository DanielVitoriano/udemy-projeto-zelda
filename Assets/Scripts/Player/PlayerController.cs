using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController characterController;
    private Animator characterAnimator;
    private Vector3 direction;

    [Header("Configurações do jogador")]
    public float movementSpeed = 3f;
    private bool isWalking;

    [Header("Configurações de ataque")]
    public ParticleSystem fxAttack;
    public Transform hitBox;
    [Range(0.2f, 1f)]
    public float hitRanger = 0.5f;
    private bool isAttacking;
    public LayerMask hitMask;
    public int hitDamage = 5;

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

        Attack();
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
            isAttacking = true;
            characterAnimator.SetTrigger("Attack");
            fxAttack.Emit(1);

            Collider[] hitInfo = Physics.OverlapSphere(hitBox.position, hitRanger, hitMask);

            foreach(Collider col in hitInfo)
            {
                col.gameObject.SendMessage("Hit", hitDamage, SendMessageOptions.DontRequireReceiver);
            }

        }
    }

    private void AttackIsDone()
    {
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        if (hitBox != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(hitBox.position, hitRanger);
        }
       
    }

}
