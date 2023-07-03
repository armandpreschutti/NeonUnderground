using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public float damage;
     Collider triggerBox;

    // Start is called before the first frame update
    void Start()
    {
        triggerBox= GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var enemy = other.gameObject.GetComponent<HealthHandler>();

        if (enemy != null)
        {
            enemy.health -= damage;
            if(enemy.health <= 0)
            {
                Destroy(enemy.gameObject);  
            }
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
