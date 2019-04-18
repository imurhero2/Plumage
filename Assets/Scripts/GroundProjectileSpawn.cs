using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProjectileSpawn : MonoBehaviour
{
    // Only works if you make a child of the enemy object and place it at a specific point for it to spawn.
    
    public float startTimeBtwShots;
    public Transform target;
    public GameObject projectile;

    private float fireRate;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireRate <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            fireRate = startTimeBtwShots;
        }

        else
        {
            fireRate -= Time.deltaTime;
        }
    }
}
