using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;

public class IKTester : MonoBehaviour
{
    public PlayerInput playerInput;
    public InputAction attack;
    public InputAction block;
    public Animator anim;
    public Transform dummyPosition;
    public Transform target1;
    public Transform target2;
    public Rigidbody rb;
    public float strafeSpeed;
    public float horizontalInput;
    public float verticalInput;
    public bool canStrafe;
    public bool canFace;
    public bool canAttack;
    public TwoBoneIKConstraint leftHandConstraint;
    public Transform leftHand;
     
    private void Awake()
    {
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        attack = playerInput.Player.Fire;
        attack.Enable();
        attack.performed += Attack1;

        block = playerInput.Player.Block;
        block.Enable();
        block.performed += Attack2;
        
    }

    private void OnDisable()
    {
        attack.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        leftHandConstraint.weight = 0.0f;
    }
    public void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        AnimationMovement();
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            /*if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .2f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .8f)
            {
                leftHandConstraint.weight = 1f;
            }
            else
            {
                leftHandConstraint.weight = 0f;
            }*/
            if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.7f)
            {
                leftHandConstraint.data.targetPositionWeight = anim.GetCurrentAnimatorStateInfo(0).normalizedTime * 2f;
            }
            else
            {
                leftHandConstraint.data.targetPositionWeight = 0.0f;
            }
        }
        else
        {
            return;
        }
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        FaceTarget();
        Strafe();
    }
    public void Attack1(InputAction.CallbackContext context)
    {
        if (canAttack)
        {
            leftHand.position = target1.position;
            //leftHandConstraint.data.target = target1;
            anim.SetTrigger("Attack");
        }
        else
        {
            return;
        }

    }
    public void Attack2(InputAction.CallbackContext context)
    {
        if (canAttack)
        {
            //leftHandConstraint.data.target = target2;
            leftHand.position = target2.position;
            anim.SetTrigger("Attack");
        }
        else
        {
            return;
        }

    }
    private void FaceTarget()
    {
        if(canFace)
        {
            Vector3 direction = (dummyPosition.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(targetRotation);
        }        
    }

    private void Strafe()
    {
        if (canStrafe)
        {
            Vector3 distance = dummyPosition.GetComponent<Rigidbody>().position - rb.position;
            Vector3 strafeDirection = transform.right * horizontalInput + transform.forward * verticalInput;
            rb.MovePosition(rb.position + strafeDirection * strafeSpeed * Time.deltaTime);
        }        
    }
    public void AnimationMovement()
    {
        anim.SetFloat("Horizontal", horizontalInput);
        anim.SetFloat("Vertical", verticalInput);
    }
}
