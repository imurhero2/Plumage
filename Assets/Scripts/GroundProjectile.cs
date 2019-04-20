using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProjectile : MonoBehaviour
{
    public float speed;
    public float hangTime;
    public float fieldOfView;

    private float distance;
    public Transform target;

    Vector3 moveDirection;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        moveDirection = (target.transform.position - transform.position).normalized * speed;
    }

    // Update is called once per frame
    private void Update()
    {
        PlayerEnter();
        Tidy();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    // projectile deletes itself based on what the hangtime is set to
    void Tidy()
    {
        Destroy(gameObject, hangTime);
    }

    void PlayerEnter()
    {
        if (Vector3.Distance(target.transform.position, transform.position) < fieldOfView)
        {

            rb.velocity = new Vector3(moveDirection.x, moveDirection.y, moveDirection.z);
        }
        
    }
}
