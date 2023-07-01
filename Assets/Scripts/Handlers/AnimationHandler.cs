using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;
    public bool isBlocking;
    public bool isAttacking;
    public bool canCombo;
    public bool canFinish;
    public bool LightAttack1;
    public bool LightAttack2;



    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    { 
        Blocking();
        LightAttack();
    }
    public void Blocking()
    {
        if (Input.GetButton("Fire2") && !isAttacking)
        {
            anim.SetBool("IsBlocking", true);
            isBlocking = true;
        }
        else
        {
            isBlocking = false;
            anim.SetBool("IsBlocking", false);
        }
    }
    public void LightAttack()
    {
        if (Input.GetButtonDown("Fire1") && !isAttacking)
        {
         
            anim.SetBool("LightAttack1", true);            
        }
        
        else if(Input.GetButtonDown("Fire1") && canCombo && isAttacking)
        {
            canCombo= false;
            anim.SetBool("LightAttack2", true);
        }
        else if (Input.GetButtonDown("Fire1") && canFinish && isAttacking)
        {
            canFinish= false;
            anim.SetBool("LightAttack3", true);
        }
        else
        {
            return;
        }
    }
    public void EndAttack()
    {
        isAttacking = false;
        canCombo= false;
        canFinish= false;
        anim.SetBool("IsAttacking", false);
    }
    public void StartAttack()
    {
        isAttacking = true;
        anim.SetBool("IsAttacking", true);
    }
    public void DetectCombo()
    {
        canCombo= true;
    }
    public void DetectFinisher()
    {
        canFinish= true;
    }
    public void EndLightAttack1()
    {
        anim.SetBool("LightAttack1", false);
    }
    public void EndLightAttack2()
    {
        anim.SetBool("LightAttack2", false);
        EndLightAttack1();
    }
    public void EndLightAttack3()
    {
        anim.SetBool("LightAttack3", false);
       EndLightAttack2();
    }

}
