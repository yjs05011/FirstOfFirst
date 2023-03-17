using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackHitBox : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Untagged")
        {
            Debug.Log("hit da hit");
            // other.GetComponent<Monster>().OnDamage();
        }
    }
}
