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
    Animator anim;

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
        anim = GetComponent<Animator>();
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

        float distaceToPlayer = Vector3.Distance(transform.position, player.position);
        
        //Set direction vector
        Vector3 dir;
        if (chasing)
            dir = player.position - transform.position;
        else
            dir = currentPatrolPoint.position - transform.position;
        dir = dir.normalized;
        //Looking for player

        if (distaceToPlayer<= minDistance)
        {
            RaycastHit hit;
            Vector3 dirToPlayer = player.position - transform.position;
            dirToPlayer.Normalize();
            if (Physics.Raycast(transform.position, transform.TransformDirection(dirToPlayer) * 50.0f, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                    chasing = true;
            }
            else
            {
                if (chasing)
                {
                    chasing = false;
                    ResetPoint();
                }
            }
        }
        else
        {
            if (chasing)
            {
                chasing = false;
                ResetPoint();
            }
        }

        //Move
        body.velocity = dir * speed;
        //Set anim
        anim.SetFloat("right", dir.x);
        anim.SetFloat("up", dir.z);
        //Attack
        if (chasing && distaceToPlayer <= attackDistance)
        {
            GameObject hb = Instantiate(hitBox,transform);
            Vector3 hbPos = transform.position + dir * attackDistance;
            hb.transform.position = hbPos;
            isAttacking = true;
        }
        Debug.Log(distaceToPlayer);
    }

    public void MoveToTheNextPoint()
    {
        Debug.Log("Point changed");
        currentIndex++;
        if (currentIndex >= patrolPoints.Length)
            currentIndex = 0;
        currentPatrolPoint = patrolPoints[currentIndex];
    }

    public void ResetPoint()
    {
        int minDistIndex = 0;
        float currentDist = 0;
        for(int i = 0; i < patrolPoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, patrolPoints[i].position);
            if (distance < currentDist)
            {
                currentDist = distance;
                minDistIndex = i;
            }
        }
        currentPatrolPoint = patrolPoints[minDistIndex];
    }

    public void MakeDamage()
    {
        life--;
        if (life <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        anim.SetTrigger("Die");
        this.enabled = false;
    }
    Vector3 CalculateAttackDirection()
    {
        Vector3 attackDirection;
        if (player.position.x > transform.position.x)
        {
            attackDirection = Vector3.right;
            float rightDistance = Mathf.Abs(player.position.x - transform.position.x);
            if (player.position.z > transform.position.z)
            {
                float upDistance = Mathf.Abs(player.position.z - transform.position.z);
                if (upDistance > rightDistance)
                    attackDirection = Vector3.forward;
            }
            else
            {
                float downDistance = Mathf.Abs(player.position.z - transform.position.z);
                if (downDistance > rightDistance)
                    attackDirection = Vector3.back;
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
                    attackDirection = Vector3.back;
            }
        }
        return attackDirection;
    }
}
