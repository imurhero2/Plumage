using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyTest : MonoBehaviour
{
    public float speed;
    public float width;
    public float height;
    public float length;
    public float lookRadius = 10f;

    public Transform target;

    private float distance;
    private float x, y, z;
    private float counter;

    private bool attack = false;

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if (attack == false)
        {
            counter += Time.deltaTime;
            x = Mathf.Cos(counter) * width;
            y = Mathf.Sin(counter) * height;
            z = Mathf.Sin(counter) * length;

            transform.position = startPos + new Vector3(x, y, z);
        }

        if (distance <= lookRadius)
        {
            attack = true;
            // Debug.Log(attack);
            StartCoroutine("lookAt");
        }
    }

    IEnumerator lookAt()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        // ^ Value never used
        speed = 30;
        transform.rotation = lookRotation;
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
            Debug.Log("Should kill me");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
