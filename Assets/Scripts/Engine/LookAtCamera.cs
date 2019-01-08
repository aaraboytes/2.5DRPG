using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour {

	void Update () {
        Vector3 dir = Camera.main.transform.position - transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, 20.0f * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(0.0f,Mathf.Lerp(transform.rotation.y,Vector3.Angle(transform.position,Camera.main.transform.position) * Mathf.Deg2Rad,20.0f*Time.deltaTime), 0.0f); 
    }
}
