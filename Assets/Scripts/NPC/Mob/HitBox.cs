using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float force;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 attackDirection;
            if (other.transform.position.x > transform.position.x)
            {
                attackDirection = Vector3.left;
                float rightDistance = Mathf.Abs(other.transform.position.x - transform.position.x);
                if (other.transform.position.z > transform.position.z)
                {
                    float upDistance = Mathf.Abs(other.transform.position.z - transform.position.z);
                    if (upDistance > rightDistance)
                        attackDirection = -Vector3.forward;
                }
                else
                {
                    float downDistance = Mathf.Abs(other.transform.position.z - transform.position.z);
                    if (downDistance > rightDistance)
                        attackDirection = Vector3.forward;
                }
            }
            else
            {
                attackDirection = Vector3.right;
                float leftDistance = Mathf.Abs(other.transform.position.x - transform.position.x);
                if (other.transform.position.z > transform.position.z)
                {
                    float upDistance = Mathf.Abs(other.transform.position.z - transform.position.z);
                    if (upDistance > leftDistance)
                        attackDirection = -Vector3.forward;
                }
                else
                {
                    float downDistance = Mathf.Abs(other.transform.position.z - transform.position.z);
                    if (downDistance > leftDistance)
                        attackDirection = Vector3.forward;
                }
            }
            other.GetComponent<PlayerControllerSuperTwoD>().MakeDamage(attackDirection,force);
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}
