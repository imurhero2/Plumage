using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundProjectileSpawn : MonoBehaviour
{
    // Only works if you make a child of the enemy object and place it at a specific point for it to spawn.

    public Transform player;
    public float range = 50.0f;
    public float bulletImpulse = 20.0f;

    private bool onRange = false;

    public Rigidbody projectile;

    public AudioClip GroundEnemyFireSound;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        float rand = Random.Range(1.0f, 2.0f);
        InvokeRepeating("Shoot", 2, rand);
    }

    void Shoot()
    {
        if (onRange)
        {
            source.PlayOneShot(GroundEnemyFireSound, source.volume);
            Rigidbody bullet = (Rigidbody)Instantiate(projectile, transform.position + transform.forward, transform.rotation);
            bullet.AddForce(transform.forward * bulletImpulse, ForceMode.Impulse);

            Destroy(bullet.gameObject, 2);
        }

    }

    void Update()
    {

        onRange = Vector3.Distance(transform.position, player.position) < range;

        if (onRange)
            transform.LookAt(player);
    }


}

