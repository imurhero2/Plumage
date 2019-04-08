using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingEnemy : MonoBehaviour
{
    public float lookRadius = 10f;
    public float height = 30f;
    public float tooLow = 22f;
    public float force = 16f;
    public float speed = 15f;

    public Transform target;
    public Transform[] points;

    bool found = false;

    Rigidbody rb;
    CapsuleCollider caps;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
        caps = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        float targetDistance = Vector3.Distance(target.position, transform.position);

        if (Physics.Raycast(ray, out hit, height) && found == false)
        {
            float proportionalHeight = (height - hit.distance) / height;
            Vector3 appliedHoverForce = Vector3.up * proportionalHeight * force;
            rb.AddForce(appliedHoverForce, ForceMode.Acceleration);

            if (transform.position.y == tooLow)
            {
                rb.AddForce(appliedHoverForce, ForceMode.Acceleration);
            }
        }

        if (targetDistance <= lookRadius)
        {
            found = true;
            StartCoroutine("lookAt");
        }                
    }

    IEnumerator lookAt()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            Destroy(gameObject);

        Debug.Log("Should kill me");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
