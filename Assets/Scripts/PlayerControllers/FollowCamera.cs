using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    Transform player;
    public Vector3 offset;
    public float speed;
	void Start () {
        player = Transform.FindObjectOfType<PlayerControllerSuperTwoD>().transform;
	}

	void Update () {
        transform.position = Vector3.Lerp(transform.position, player.position + offset, speed);
	}
}
