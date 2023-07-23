 using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

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
    public float horizontalInput;
    public float verticalInput;

    public Vector3 combatDistance;

    public GameObject weapon;
    public RuntimeAnimatorController[] styles;
    public GameObject dummy;
    public Animator cameraAnim;

    // test var
    public Transform target;
    public float rotationSpeed = 5f;
    public float strafeSpeed = 3f;
    public bool isLockedOn = false;

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
        equip.performed += Aim;
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
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        DetectAttack();
        anim.SetFloat("Horizontal", horizontalInput);
        anim.SetFloat("Vertical", verticalInput);
        
        


    }
    public void FixedUpdate()
    {
        if (isLockedOn && target != null)
        {
            //FaceTarget();
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("Block") && !anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                
                Strafe();
            }
            else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
            {
                AttackMovement();
            }
            else
            {

                return;
            }
            Vector3 distance = target.GetComponent<Rigidbody>().position - rb.position;
        }
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (!isAttacking && !isBlocking) 
        {             
            anim.SetTrigger("Attack");
           
        }
    }
    public void StartBlock(InputAction.CallbackContext context)
    {
        isBlocking= true;
        strafeSpeed = strafeSpeed / 2;
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
        strafeSpeed = strafeSpeed * 2;
        if (!isAttacking)
        {
            anim.SetBool("IsBlocking", false);
        }
        else
        {
            return;
        }
    }

    public void Aim(InputAction.CallbackContext context)
    {
        cameraAnim.SetTrigger("SwitchCamera");

        isLockedOn = !isLockedOn;
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
    
    public void ChargeMovement()
    {
        Vector3 direction = (dummy.gameObject.transform.position - transform.position).normalized;
        rb.AddForce(direction * movementSpeed, ForceMode.VelocityChange);
    }
    public void ToggleStandard()
    {
        weapon.SetActive(false);
        anim.runtimeAnimatorController = styles[0];
        isEquiped = false;
    }
    public void ToggleWeapon()
    {
        weapon.SetActive(true);
        anim.runtimeAnimatorController = styles[1];
        isEquiped = true;
    }
    public void ToggleMagic()
    {
        weapon.SetActive(false);
        anim.runtimeAnimatorController = styles[2];
        isEquiped = true;
    }
    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        rb.MoveRotation(targetRotation);
    }

    private void Strafe()
    {
        Vector3 distance = target.GetComponent<Rigidbody>().position - rb.position;
        Vector3 strafeDirection = transform.right * horizontalInput + transform.forward * verticalInput;
        rb.MovePosition(rb.position + strafeDirection * strafeSpeed * Time.deltaTime);
            
        
    }
    public void AttackMovement()
    {
        Vector3 distance = target.GetComponent<Rigidbody>().position - rb.position;
        Vector3 newPosition = Vector3.Lerp(rb.position, target.GetComponent<Rigidbody>().position, movementSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

}
