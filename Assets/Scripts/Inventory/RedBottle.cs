using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBottle : MonoBehaviour
{
    public GameObject hitBox;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            GameObject hb = Instantiate(hitBox);
            hb.transform.position = transform.position;
            Destroy(hb, 1.0f);
            Destroy(gameObject);
        }
    }
}
