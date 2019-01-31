using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mob"))
        {
            other.GetComponent<Mob>().MoveToTheNextPoint();
        }
    }
}
