using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingAIController : MonoBehaviour
{
    public float speed;
    public float width;
    public float height;
    public float length;
    public float coolDown;
    public float waitTime = 0;
    public float lookRadius = 10f;

    public Transform target;

    private bool isAttacking;
    private bool backToOrigin;
    private bool timeBeforeAttack;

    private Vector3 startPos;
    private Vector3 attackPos;

    private float timeCounter = 0;
    private float x, y, z;
    private float distance;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        timeBeforeAttack = false;
        isAttacking = false;
        backToOrigin = false;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (!isAttacking && !backToOrigin)
        {
            timeCounter += Time.deltaTime;

            x = Mathf.Cos(timeCounter) * width;
            y = Mathf.Sin(timeCounter) * height;
            z = Mathf.Sin(timeCounter) * length;

            transform.position = startPos + new Vector3(x, y, z);
        }

        else if (isAttacking && !backToOrigin)
        {
            if (transform.position != attackPos)
                transform.position = Vector3.MoveTowards(transform.position, attackPos, speed * 8 * Time.deltaTime);

            else
            {
                isAttacking = false;
                backToOrigin = false;
            }

            
        }

        else if (backToOrigin && !isAttacking)
        {
            if (transform.position != startPos)
                transform.position = Vector3.MoveTowards(transform.position, startPos, speed * 8 * Time.deltaTime);

            else
            {
                timeBeforeAttack = false;
                backToOrigin = false;
            }
        }

        if (timeBeforeAttack)
        {
            if (waitTime < coolDown)
                waitTime += Time.deltaTime;

            else
            {
                timeBeforeAttack = false;
                waitTime = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!timeBeforeAttack && !isAttacking && !backToOrigin && target)
        {
            attackPos = other.transform.position;
            isAttacking = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isAttacking && !collision.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
            backToOrigin = true;
        }
    }

    
}
