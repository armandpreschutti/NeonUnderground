 using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeHandler : MonoBehaviour
{
    public Rigidbody rb;
    //public List<AttackSO> style;
    public PlayerInput playerInput;
    public InputAction attack;
    public InputAction block;
    public InputAction equip;
    public Animator anim;
    public bool isAttacking;
    public bool isBlocking;
    public bool isEquiped;
    public float movementSpeed;
    public GameObject weapon;
    public RuntimeAnimatorController[] styles;
    public GameObject dummy;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        attack = playerInput.Player.Fire;
        attack.Enable();
        attack.performed += Attack;

        block = playerInput.Player.Block;
        block.Enable();
        block.started += StartBlock;
        block.canceled += EndBlock;

        equip = playerInput.Player.Equip;
        equip.Enable();
        equip.performed += Equip;
    }

    private void OnDisable()
    {
        attack.Disable();
        block.Disable();
        equip.Disable();
    }
    public void Start()
    {
        anim = GetComponent<Animator>();      
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        DetectAttack();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!isAttacking && !isBlocking) 
        {             
            anim.SetTrigger("Attack");
            AttackMovement();
        }
    }
    public void StartBlock(InputAction.CallbackContext context)
    {
        isBlocking= true;
        if (!isAttacking)
        {
            anim.SetBool("IsBlocking", true);
        }
        else
        {
            return;
        }
    }
    public void EndBlock(InputAction.CallbackContext context)
    {
        isBlocking = false;
        if (!isAttacking)
        {
            anim.SetBool("IsBlocking", false);
        }
        else
        {
            return;
        }
    }

    public void Equip(InputAction.CallbackContext context)
    {
        if (!isEquiped)
        {
            //weapon.SetActive(true);
            anim.runtimeAnimatorController = styles[1];
            isEquiped= true;
        }
        else if(isEquiped)
        {
            //weapon.SetActive(false);
            anim.runtimeAnimatorController = styles[0];
            isEquiped = false;
        }
        else
        {
            return;
        }
    }

    public void DetectAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .2f)
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }
    
    public void AttackMovement()
    {
        Vector3 direction = (dummy.gameObject.transform.position - transform.position).normalized;
        rb.AddForce(direction * movementSpeed, ForceMode.VelocityChange);
    }

}
