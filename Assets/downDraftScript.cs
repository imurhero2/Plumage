using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class downDraftScript : MonoBehaviour {

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && PlayerMovement.isGliding)
        {
            PlayerMovement.inDownDraft = true;
            other.attachedRigidbody.AddForce(Vector3.down * 20);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.inDownDraft = false;
        }
    }
}
