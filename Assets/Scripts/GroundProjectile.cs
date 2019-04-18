using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProjectile : MonoBehaviour
{
    public float speed;
    public float hangTime;
    public float fieldOfView;

    private float distance;
    private Transform player;
    private Vector3 target;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector3(player.position.x, player.position.y);
    }

    // Update is called once per frame
    // projectile moves towards player and then deletes itself after whatever hangtime is
    void Update()
    {
        distance = Vector3.Distance(player.position, transform.position);

        if (distance <= fieldOfView)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.x == target.x && transform.position.y == target.y && transform.position.z == target.z)
            {
                Destroy(gameObject);
            }
        }
     
        Tidy();
    }

    // projectile deletes itself based on what the hangtime is set to
    void Tidy()
    {
        Destroy(gameObject, hangTime);
    }
}
