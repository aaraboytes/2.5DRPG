using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float speed;
    Sprite sprite;
    Vector2 movement;
    Animator anim;
    Rigidbody2D body;

	void Start () {
        sprite = GetComponent<SpriteRenderer>().sprite;
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
        //Input
        movement = (Input.GetAxisRaw("Horizontal") * Vector2.right) + (Input.GetAxisRaw("Vertical")*Vector2.up);
        //View Direction
        anim.SetFloat("velocity", (Mathf.Abs(movement.x) + Mathf.Abs(movement.y)));
        anim.SetFloat("right", movement.x);
        anim.SetFloat("up", movement.y);
        //Movement
        movement *= speed;
        body.velocity = movement;
	}
}
