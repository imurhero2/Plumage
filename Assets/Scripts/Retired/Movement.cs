using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    public float movementSpeed = 2f;
    private float moveHorizontal;
    private float moveVertical;

    void FixedUpdate()
    {
        moveHorizontal = (Input.GetAxis("Horizontal") * Time.deltaTime * movementSpeed);
        moveVertical = (Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);
        transform.Translate(moveHorizontal, 0, moveVertical);
    }
}
