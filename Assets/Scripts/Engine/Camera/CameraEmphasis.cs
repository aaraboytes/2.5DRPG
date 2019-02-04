using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEmphasis : MonoBehaviour
{
    public Transform emphasisCam;
    Transform player;
    GameObject cam;
    float distance;
    float emphasis;
    bool move = false;
    Quaternion currentCamRot;
    private void Start()
    {
        cam = Camera.main.gameObject;
        currentCamRot = cam.transform.rotation;
        distance =  GetComponent<SphereCollider>().radius;
    }
    private void Update()
    {
        if (move)
        {
            float distWithPlayer = Mathf.Sqrt((Mathf.Pow(player.position.x - transform.position.x, 2)) + (Mathf.Pow(player.position.z - transform.position.z, 2)));
            cam.transform.position = Vector3.Lerp(cam.transform.position, emphasisCam.position, 0.05f);
            cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, emphasisCam.rotation, 0.05f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        player = other.transform;
        move = true;
        cam.GetComponent<FollowCamera>().enabled = false;
    }
    private void OnTriggerExit(Collider other)
    {
        move = false;
        cam.GetComponent<FollowCamera>().enabled = true;
        cam.transform.rotation = currentCamRot;
    }
}
