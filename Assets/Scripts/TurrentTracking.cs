using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrentTracking : MonoBehaviour
{
    public float speed = 3.0f;
    public GameObject playerTarget = null;
    Vector3 lastKnownPosition = Vector3.zero;
    Quaternion lookRotation;

    // Update is called once per frame
    void Update()
    {
        if (playerTarget)
        {
            if (lastKnownPosition != playerTarget.transform.position)
            {
                lastKnownPosition = playerTarget.transform.position;
                lookRotation = Quaternion.LookRotation(lastKnownPosition - transform.position);
            }

            if (transform.rotation != lookRotation)
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, speed * Time.deltaTime);

        }
    }

    bool SetTarget(GameObject target)
    {
        if (target)
        {
            return false;
        }

        playerTarget = target;

        return true;
    }
}
