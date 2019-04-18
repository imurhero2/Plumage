using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour {

    private bool checkpointReached;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !checkpointReached)
        {
            checkpointReached = true;
            Debug.Log("Checkpoint Reached!");
        }
    }

}
