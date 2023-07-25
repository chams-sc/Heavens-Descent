using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 4f;             // Base movement speed
    [SerializeField]
    private float acceleration = 7f;          // Acceleration factor
    [SerializeField]
    private float maxSpeed = 8f;
    [SerializeField]
    private float dashSpeed = 20f;
    [SerializeField]
    private float dashLength = 5f;            // Dash length (distance)
    [SerializeField]
    private float dashCooldown = 2f;          // Cooldown time between dashes

    public Rigidbody2D rb;
    private Camera cam;
    private Animator animator;

    private bool isDashing = false;            // Flag to track if the player is currently dashing
    private float dashLengthCounter = 0f;      // Counter to track the remaining dash length
    private float dashCooldownCounter = 0f;    // Counter to track the remaining cooldown time

    Vector2 movement;                          // Player's movement input
    Vector2 mousePos;

    [SerializeField]
    private AudioSource playerFootsteps;
    [SerializeField]
    private AudioSource dashSound;

    void Start()
    {
        cam = Camera.main;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

        animator.SetFloat("movement_x", movement.x);
        animator.SetFloat("movement_y", movement.y);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDashing && dashCooldownCounter <= 0)
            {
                StartCoroutine(Dash());
                dashCooldownCounter = dashCooldown;
            }
        }

        if (dashCooldownCounter > 0)
        {
            dashCooldownCounter -= Time.deltaTime;
        }

        if (movement.magnitude > 0 && !isDashing && !playerFootsteps.isPlaying)
        {
            playerFootsteps.Play();
        }
        else if (movement.magnitude == 0 || isDashing)
        {
            playerFootsteps.Stop();
        }
    }

    void FixedUpdate()
    {
        float currentMoveSpeed = moveSpeed;

        if (!isDashing)
        {
            // Apply acceleration and limit speed
            Vector2 desiredVelocity = movement * currentMoveSpeed;
            Vector2 currentVelocity = rb.velocity;
            Vector2 accelerationVector = (desiredVelocity - currentVelocity).normalized * acceleration;
            Vector2 newVelocity = currentVelocity + accelerationVector * Time.fixedDeltaTime;
            rb.velocity = Vector2.ClampMagnitude(newVelocity, maxSpeed);
        }

        // Debug log for current movement speed
        Debug.Log("Current Movement Speed: " + rb.velocity.magnitude);
    }

    IEnumerator Dash()
    {
        isDashing = true;
        rb.velocity = movement.normalized * dashSpeed;
        dashLengthCounter = dashLength;

        dashSound.Play();           //Play dash
        while (dashLengthCounter > 0)
        {
            dashLengthCounter -= Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        isDashing = false;
    }
}
