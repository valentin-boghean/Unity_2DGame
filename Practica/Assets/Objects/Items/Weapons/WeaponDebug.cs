using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDebug : MonoBehaviour
{
    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8) //8 e pamant
        {
            Rigidbody2D acesta = this.GetComponent<Rigidbody2D>();
            acesta.isKinematic = true;
            acesta.velocity = Vector2.zero;  
        }
    }
}
