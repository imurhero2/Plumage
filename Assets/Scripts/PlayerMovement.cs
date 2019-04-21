using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static bool isGliding;
    public static bool inDownDraft;

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
    private bool jumpUsed;
    private bool grounded;
    private Camera cam;

    private bool playingSound;

    public AudioClip walkingSound;
    private AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }


    void Start()
    {
        defaultSpeed = moveSpeed;
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
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

        if (Input.GetButtonDown("Jump") && grounded && !jumpUsed)
        {
            Debug.Log("Jumping");
            Jump();
        }

        if (Input.GetButton("Jump") && !grounded)
        {
            Glide();
            isGliding = true;
            Debug.Log("Gliding");
        }
        else
        {
            isGliding = false;
        }
        if (!grounded)
        {
            moveSpeed = glideSpeed;
        }
        else
        {
            moveSpeed = defaultSpeed;
            
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
        transform.Translate(Vector3.forward * h_move * moveSpeed * Time.deltaTime);
        transform.Translate(Vector3.left * v_move * moveSpeed * Time.deltaTime);
        // Matches the player's y rotation with the camera's y rotation
        transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y + 90, 0);

        if ((h_move > 0 || v_move > 0) && grounded)
        {
            if (!playingSound)
            {
                StartCoroutine("WalkingSFXDelay");
            }
        }
    }

    IEnumerator WalkingSFXDelay()
    {
        playingSound = true;
        source.PlayOneShot(walkingSound, source.volume);
        yield return new WaitForSeconds(0.5f);
        playingSound = false;
    }

    private void Jump()
    {
        jumpUsed = true;
        StartCoroutine("JumpDelay");
        rb.AddForce(transform.up * jumpForce);
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(0.5f);
        jumpUsed = false;
    }

    private void Glide()
    {
        // Limits the player's velocity to no less than minGlideVelocity and no more than maxGlideVelocity
        if (!inDownDraft)
        {
            rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, minGlideVelocity, maxGlideVelocity), rb.velocity.z);
        }
    }
}
