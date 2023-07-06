using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public float damage;
    public Animator anim;
    public Collider triggerBox;

    // Start is called before the first frame update
    void Start()
    {
        triggerBox= GetComponent<BoxCollider>();
        anim= GetComponentInParent<Animator>();
    }
    public void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            EnableTriggerBox();
        }
        else
        {
            DisableTriggerBox();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        /*var enemy = other.gameObject.GetComponent<HealthHandler>();

        if (enemy != null)
        {
            enemy.health -= damage;
            Debug.Log(enemy.health);

            if(enemy.health <= 0)
            {
                Destroy(enemy.gameObject);  
            }
        }*/
        if(other.tag == "Enemy")
        {
            Debug.Log("Hit " + other.name.ToString() + "!");
        }

    }
    
    public void EnableTriggerBox()
    {
        triggerBox.enabled= true;
    }
    public void DisableTriggerBox()
    {
        triggerBox.enabled= false;
    }
}
