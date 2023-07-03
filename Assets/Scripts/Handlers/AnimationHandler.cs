using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public List<AttackSO> attacks;
    public Animator anim;
    
    public int comboCount = 0;         // Current combo count
    public bool isAttacking = false;   // Flag to prevent input during attack animation

    public float comboResetTime = 1f;   // Time window to reset combo count
    public float attackDuration = 0.5f; // Duration of each attack animation

    public float comboTimer = 0f;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (isAttacking)
            return;

        // Detect player input
        if (Input.GetButtonUp("Fire1"))
        {
            // Increase combo count and perform attack based on current combo count
            comboCount++;

            if (comboCount > 3)
            {
                comboCount = 0;
            }
                

            PerformAttack(comboCount);

            // Start the combo reset timer
            comboTimer = comboResetTime;
        }

        // Combo reset timer
        if (comboTimer > 0)
        {
            comboTimer -= Time.deltaTime;

            if (comboTimer <= 0)
                comboCount = 0;
        }
    }

    void PerformAttack(int combo)
    {
        isAttacking = true;
        anim.runtimeAnimatorController = attacks[comboCount].animatorOV;
        anim.Play("Attack", 0, 0);
        /*// Perform attack animation based on the combo
        switch (combo)
        {
            case 1:
                // Attack animation for combo 1
                anim.SetBool("LightAttack1", true);
                break;
            case 2:
                // Attack animation for combo 2
                anim.SetBool("LightAttack2", true) ;
                break;
            case 3:
                // Attack animation for combo 3
                anim.SetBool("LightAttack3", true);
                break;
        }*/

        // Delay the end of the attack animation
        Invoke("EndAttack", attackDuration);
    }

    void EndAttack()
    {
        isAttacking = false;
    }
    /*public float lastClickTime;
    public float spamBuffer = .5f;
    public float clickIntervalTime = .2f;
    public float lastComboEnd;
    public int comboCounter;
    public bool isAttacking;
    public float animTime;


    Animator anim;
    //public WeaponHandler weapon;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .9 && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Debug.Log(Time.time);
            //Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            isAttacking = false;
        }
        else
        {
            isAttacking = false;
        }
        if (Input.GetButtonDown("Fire1"))
        {

            PerformAttack(comboCounter);
        }

    }
    void Attack()
    {

        if (Time.time - lastComboEnd > spamBuffer && comboCounter <= attacks.Count)
        {

            CancelInvoke("EndCombo");

            if (Time.time - lastClickTime > clickIntervalTime)
            {
                anim.runtimeAnimatorController = attacks[comboCounter].animatorOV;
                anim.Play("Attack", 0, 0);
                //weapon.damage = combo[comboCounter].damage;

                //All other attack logic (VFX, SFX, etc.) goes here

                comboCounter++;
                lastClickTime = Time.time;

                if (comboCounter > attacks.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void PerformAttack(int combo)
    {

        if (comboCounter < attacks.Count)
        {

            isAttacking = true;

            anim.runtimeAnimatorController = attacks[comboCounter].animatorOV;
            anim.Play("Attack", 0, 0);
            comboCounter++;
        }
        else
        {
            comboCounter = 0;
        }
        // Perform attack animation based on the combo
        switch (combo)
        {
            case 1:
                // Attack animation for combo 1
                anim.SetBool("LightAttack1", true);
                break;
            case 2:
                // Attack animation for combo 2
                anim.SetBool("LightAttack2", true);
                break;
            case 3:
                // Attack animation for combo 3
                anim.SetBool("LightAttack3", true);
                break;
        }


    }


    void ExitAttack()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1f);
        }
    }
    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }*/
}
