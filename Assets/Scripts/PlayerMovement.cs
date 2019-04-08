using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool isGliding;

    public float moveSpeed;
    public float glideSpeed;
    public float jumpForce;
    public float minGlideVelocity;
    public float maxGlideVelocity;

    private Rigidbody rb;
    private float defaultSpeed;
    private float h_move;
    private float v_move;
    private float strafeSpeed;
    private bool grounded;
    private Camera cam;


    void Start()
    {
        defaultSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        strafeSpeed = moveSpeed;
    }

    private void Update()
    {
        h_move = Input.GetAxis("Horizontal");
        v_move = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Move();
        GroundCheck();

        if (Input.GetButtonDown("Jump") && grounded)
        {
            Debug.Log("Jumping");
            Jump();
        }

        if (Input.GetButton("Jump") && !grounded)
        {
            Glide();
            isGliding = true;
        }
        else
        {
            isGliding = false;
        }
        if (!grounded)
        {
            strafeSpeed = 0;
            moveSpeed = glideSpeed;
        }
        else
        {
            moveSpeed = defaultSpeed;
            strafeSpeed = defaultSpeed;
            
        }

    }

    public bool GroundCheck()
    {
        // Raycasts below player collider to detect ground.
        if (Physics.Raycast(transform.position, -Vector3.up, GetComponent<Collider>().bounds.extents.y + 0.1f))
        {
            return grounded = true;
        }
        else
        {
           return grounded = false;
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.forward * h_move * strafeSpeed * Time.deltaTime);
        transform.Translate(Vector3.left * v_move * moveSpeed * Time.deltaTime);
        // Matches the player's y rotation with the camera's y rotation
        transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y + 90, 0);
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce);
    }

    private void Glide()
    {
        // Limits the player's velocity to no less than minGlideVelocity and no more than maxGlideVelocity
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, minGlideVelocity, maxGlideVelocity), rb.velocity.z);

        /* Possible additions: 
         *      1. Increase movement speed slightly while gliding for a better feel.
         *      2. Remove ability to strafe mid-air
         *      3. Make player always move forwards while gliding
         */
    }
}
