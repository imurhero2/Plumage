using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealHover : MonoBehaviour
{
    public float speed = 9f;
    public float turnSpeed = 5f;
    public float hoverForce = 65f;
    public float hoverHeight = 3.5f;
    public Camera cam;

    private float powerInput;
    private float turnInput;
    private Rigidbody playerRigid;

    // Start is called before the first frame update
    void Awake()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        powerInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        if (Input.GetButton("Jump"))
        {
            Ray ray = new Ray(transform.position, -transform.up);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, hoverHeight))
            {
                float proportionalHeight = (hoverHeight - hit.distance) / hoverHeight;
                Vector3 appliedHoverForce = Vector3.up * proportionalHeight * hoverForce;
                playerRigid.AddForce(appliedHoverForce, ForceMode.Acceleration);
            }
        }
        

        playerRigid.AddRelativeForce(0f, 0f, powerInput * speed);
        playerRigid.AddRelativeTorque(0f, 0f, turnInput * speed);
        //transform.eulerAngles = new Vector3(10, cam.transform.rotation.y, 0);
    }
}
