using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }
    private float horizontalInput;
    public float moveSpeed = 9f;
    private bool isFacingRight = true;
    public float jumpPower = 10f;
    private bool isGrounded = false;
    public float knockbackForce = 6f;
    private float knockbackCounter = 0f; // Baþlangýçta 0 olarak ayarlandý
    public float knockbackTime = 0.2f;
    public bool knockFromRight;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private Rigidbody2D rb;
    // private Animator animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleInput();
        HandleJump();
        FlipSprite();
        //UpdateAnimator();
    }

    private void FixedUpdate()
    {
        HandleMovement();
        CheckGrounded();
    }

    private void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            isGrounded = false;
        }
    }

    private void HandleMovement()
    {
        if (knockbackCounter <= 0)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            ApplyKnockback();
            knockbackCounter -= Time.deltaTime;
        }
    }

    private void ApplyKnockback()
    {
        float knockbackDirection = knockFromRight ? -knockbackForce : knockbackForce;
        rb.velocity = new Vector2(knockbackDirection, rb.velocity.y);
    }

    private void FlipSprite()
    {
        if ((isFacingRight && horizontalInput < 0f) || (!isFacingRight && horizontalInput > 0f))
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    /*  private void UpdateAnimator()
       {
           animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
           animator.SetBool("isGrounded", isGrounded);
       }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
