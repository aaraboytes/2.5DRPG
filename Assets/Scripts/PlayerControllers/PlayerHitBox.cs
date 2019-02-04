using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mob"))
        {
            other.GetComponent<Mob>().MakeDamage();
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
