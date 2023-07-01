using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            anim.SetBool("IsBlocking", true);
        }
        else
        {
            anim.SetBool("IsBlocking", false);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("IsLightAttacking");
        }
    }
}
