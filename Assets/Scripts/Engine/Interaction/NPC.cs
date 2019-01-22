using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {
    public float speed;
    int direction;
    bool clockDir; 

    public Vector2 timesToChangePos;
    public Vector2 walkTime;
    float timer = 0;
    float walkTimer = 0;
    float selectedTime;
    float selectedWalkTime;
    bool isMoving = false;
    bool isTalking = false;
    Rigidbody body;
    Animator anim;
    private void Start()
    {
        direction = (int)Random.RandomRange(0, 4);
        clockDir = Random.RandomRange(0, 1) > 0.5 ? true : false;
        selectedTime = SelectRandomTime(timesToChangePos);
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isMoving)
        {
            if(!isTalking)
                timer += Time.deltaTime;
            body.velocity = Vector3.zero;
        }
        if (timer > selectedTime){
            selectedWalkTime = SelectRandomTime(walkTime);
            if (clockDir)
            {
                direction++;
                if (direction >= 4) direction = 0;
            }
            else
            {
                direction--;
                if (direction <= -1) direction = 3;
            }
                timer = 0;
            StartCoroutine(Walk(direction));
        }
        anim.SetBool("moving", (Mathf.Abs(body.velocity.x)+Mathf.Abs(body.velocity.z))!=0);
        anim.SetFloat("xVel", body.velocity.x);
        anim.SetFloat("yVel", body.velocity.z);
    }
    float SelectRandomTime(Vector2 range)
    {
        return Random.RandomRange(range.x, range.y);
    }
    IEnumerator Walk(int direction) {
        while (walkTimer < selectedWalkTime)
        {
            walkTimer += Time.deltaTime;
            //Random direction
            Vector3 dir = new Vector3();
            switch (direction)
            {
                case 0:
                    dir = Vector3.forward;
                    break;
                case 1:
                    dir = -Vector3.forward;
                    break;
                case 2:
                    dir = Vector3.left;
                    break;
                case 3:
                    dir = Vector3.right;
                    break;
            }
            //direction movement
            Vector3 move = dir * speed;
            //If the npc is talking, it wont move
            if (isTalking)
                move = Vector3.zero;
            //Applying movement
            body.velocity = move;
            yield return null;
        }

        selectedTime = SelectRandomTime(timesToChangePos);
        isMoving = false;
        timer = 0;
        walkTimer = 0;
    }
    public void StartToTalk()
    {
        isTalking = true;
        body.velocity = Vector3.zero;
    }
    public void EndTalking()
    {
        isTalking = false;
    }
}
