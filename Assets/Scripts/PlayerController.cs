using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [Header("Movement Settings")]
    public float moveSpeed = 9f;
    public float acceleration = 12f;
    public float deceleration = 16f;
    public float airControl = 0.5f;
    private float horizontalInput;
    private bool isFacingRight = true;

    [Header("Jump Settings")]
    public float jumpPower = 14f;
    public float fallMultiplier = 2.5f; // Daha hızlı düşme efekti
    public float lowJumpMultiplier = 2f;
    public float coyoteTime = 0.15f;
    public float jumpBufferTime = 0.1f;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    private Rigidbody2D rb;
    private bool isJumping;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInput();
        HandleJump();
        FlipSprite();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        ApplyGravity();
        CheckGrounded();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Daha hızlı tepki verir

        // Zıplama buffer süresi: Tuşa erken basılırsa kaydedilir
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void HandleJump()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Yere bastığında sıfırlanır
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        // Eğer coyote time veya jump buffer içindeyse zıplamaya izin ver
        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isJumping = true;
            jumpBufferCounter = 0; // Buffer kullanıldı
        }

        // Zıplama tuşu bırakıldığında yükselmeyi yumuşak bırak
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    private void HandleMovement()
    {
        float targetSpeed = horizontalInput * moveSpeed;
        float speedDifference = targetSpeed - rb.velocity.x;
        float accelRate = (isGrounded ? acceleration : acceleration * airControl);

        if (horizontalInput == 0)
            accelRate = deceleration; // Daha hızlı durma

        float movement = speedDifference * accelRate * Time.fixedDeltaTime;
        rb.velocity = new Vector2(rb.velocity.x + movement, rb.velocity.y);
    }

    private void ApplyGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    private void FlipSprite()
    {
        if ((isFacingRight && horizontalInput < 0) || (!isFacingRight && horizontalInput > 0))
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && rb.velocity.y <= 0)
        {
            coyoteTimeCounter = coyoteTime;
            isJumping = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}