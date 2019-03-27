using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upDraftScript : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && PlayerMovement.isGliding)
        {
            other.attachedRigidbody.AddForce(Vector3.up * 20);
        }
    }

}
