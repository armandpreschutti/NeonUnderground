 using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeHandler : MonoBehaviour
{
    public PlayerInput playerInput;
    //public List<AttackSO> attacks;
    public Animator anim;
    public int comboCounter;
    public bool isAttacking;
    public InputAction fire;
    public InputAction block;
    public AnimatorClipInfo[] clipInfo;
    public bool isBlocking;

    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        fire = playerInput.Player.Fire;
        fire.Enable();
        fire.performed += Attack;

        block = playerInput.Player.Block;
        block.Enable();
        block.started += StartBlock;
        block.canceled += EndBlock;
    }

    private void OnDisable()
    {
        fire.Disable();
        block.Disable();
    }
    public void Start()
    {
        anim = GetComponent<Animator>();

       
        
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
    
    public void DebugAnimation()
    {
        //Get the animator clip information from the Animator Controller
        clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        //Output the name of the starting clip
        Debug.Log("Starting clip : " + clipInfo[0].clip);      
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
    

}
