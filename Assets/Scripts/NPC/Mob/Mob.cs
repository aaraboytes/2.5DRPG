using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob : MonoBehaviour
{
    public int life;
    public float speed;
    public float minDistance;
    public float attackDistance;
    public Transform[] patrolPoints;
    public GameObject hitBox;
    public float attackTime;

    int currentLife;
    Rigidbody body;
    Transform target;
    Transform player;
    [SerializeField]
    Transform currentPatrolPoint;
    int currentIndex = 1;
    bool isAttacking = false;
    float attackTimer;

    bool chasing = false;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        player = FindObjectOfType<PlayerControllerSuperTwoD>().transform;
        if(currentPatrolPoint == null)
            currentPatrolPoint = patrolPoints[currentIndex];
        else
        {
            for(int i = 0; i < patrolPoints.Length; i++)
            {
                if(patrolPoints[i] == currentPatrolPoint)
                    currentIndex = i;
            }
        }
    }

    void Update()
    {
        //Timer when attacks
        if (isAttacking)
        {
            body.velocity = Vector3.zero;
            attackTimer += Time.deltaTime;
            if (attackTimer > attackTime)
            {
                isAttacking = false;
                attackTimer = 0;
            }
            return;
        }
        //If player is too close
        float distaceToPlayer = Mathf.Sqrt(Mathf.Pow(player.position.x - transform.position.x, 2) + Mathf.Pow(player.position.z - transform.position.z, 2));
        if (distaceToPlayer <= minDistance)
            chasing = true;
        else
            chasing = false;
        //Set direction vector
        Vector3 dir;
        if (chasing)
            dir = player.position - transform.position;
        else
            dir = currentPatrolPoint.position - transform.position;
        //Move
        body.velocity = dir.normalized * speed;
        //Check attack
        if (chasing)
        {
            //Attack player
            if(distaceToPlayer <= attackDistance)
            {
                //Calculate attackDirection
                Vector3 attackDirection;
                if (player.position.x > transform.position.x)
                {
                    attackDirection = Vector3.right;
                    float rightDistance = Mathf.Abs(player.position.x - transform.position.x);
                    if(player.position.z > transform.position.z)
                    {
                        float upDistance = Mathf.Abs(player.position.z - transform.position.z);
                        if (upDistance > rightDistance)
                            attackDirection = Vector3.forward;
                    }
                    else
                    {
                        float downDistance = Mathf.Abs(player.position.z - transform.position.z);
                        if (downDistance > rightDistance)
                            attackDirection = -Vector3.forward;
                    }
                }
                else
                {
                    attackDirection = Vector3.left;
                    float leftDistance = Mathf.Abs(player.position.x - transform.position.x);
                    if (player.position.z > transform.position.z)
                    {
                        float upDistance = Mathf.Abs(player.position.z - transform.position.z);
                        if (upDistance > leftDistance)
                            attackDirection = Vector3.forward;
                    }
                    else
                    {
                        float downDistance = Mathf.Abs(player.position.z - transform.position.z);
                        if (downDistance > leftDistance)
                            attackDirection = -Vector3.forward;
                    }
                }
                //Create hitbox
                attackDirection *= attackDistance;
                attackDirection += transform.position;
                GameObject currentHitBox = Instantiate(hitBox,transform);
                currentHitBox.transform.position = attackDirection;
                Destroy(currentHitBox, 2.0f);
                //Set that is attacking
                isAttacking = true;
            }
        }
    }

    public void MoveToTheNextPoint()
    {
        Debug.Log("Point changed");
        currentIndex++;
        if (currentIndex >= patrolPoints.Length)
            currentIndex = 0;
        currentPatrolPoint = patrolPoints[currentIndex];
    }
}
