using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float acceleration;
    [SerializeField] private float maxMoveVelocity;
    [SerializeField] private float airControlMultiplier;
    [SerializeField] private float drag;

    [Header("Jump Settings")]
    [SerializeField] private float maxJumpHeight;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpBufferTime;
    [SerializeField, Range(0f, 90f)] private float jumpAngle = 45f;

    [Header("Ceiling Adjustment Settings")]
    [SerializeField] private float ceilingThreshold = 1f;
    [SerializeField] private float ceilingAdjustmentRange = 5f;


    [SerializeField] private Animator anim;

    private float moveInput;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private bool isJumping;

    private Rigidbody2D rb;
    private float defGravityValue;

    private bool isPlayerFinishedTheLevel;
    private EnvironmentScript envScript;
    private PlayerColliderChecks playerColliderChecksScript;

    private void Start()
    {
        envScript = GameObject.FindObjectOfType<EnvironmentScript>();
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        playerColliderChecksScript = GetComponent<PlayerColliderChecks>();
        defGravityValue = rb.gravityScale;
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("endPoint"))
        {
            isPlayerFinishedTheLevel = true;
            DoesPlayerHaveMobility(false);
            envScript.PrepareForNextLevel();
        }

        if (other.CompareTag("trap") || other.CompareTag("fireSpot") || other.CompareTag("outOfBox")/* || other.CompareTag("FireBubble")*/)
        {
            envScript.RestartLevel();
        }
    }

	public void DoesPlayerHaveMobility(bool checkBool)
    {
        if (checkBool)
        {
            rb.gravityScale = defGravityValue;
            rb.isKinematic = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        else
        {
            rb.gravityScale = 0f;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            rb.bodyType = RigidbodyType2D.Static;
        }
    }

    public void MoveToTheNewSpawnPoint(Transform selectedStartingPoint)
    {
        this.gameObject.transform.position = new Vector3(selectedStartingPoint.position.x, selectedStartingPoint.position.y, this.gameObject.transform.position.z);
        DoesPlayerHaveMobility(true);
        isPlayerFinishedTheLevel = false;
    }

    private void HandleMovement()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("xx", moveInput);
        anim.SetBool("isGrounded", playerColliderChecksScript.isGrounded);


        float targetVelocityX = moveInput * maxMoveVelocity;

        if (playerColliderChecksScript.isGrounded)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetVelocityX, acceleration * Time.deltaTime), rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, targetVelocityX * airControlMultiplier, acceleration * Time.deltaTime), rb.velocity.y);
        }

        ApplyDrag();
    }

    private void ApplyDrag()
    {
        if (Mathf.Abs(moveInput) < 0.01f && playerColliderChecksScript.isGrounded)
        {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, drag * Time.deltaTime), rb.velocity.y);
        }
    }

    private void HandleJump()
    {
        if (playerColliderChecksScript.isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0 && coyoteTimeCounter > 0 && !isJumping)
        {
            float maxVerticalVelocity = Mathf.Sqrt(2 * maxJumpHeight * Physics2D.gravity.magnitude * defGravityValue);
            float ceilingDistance = CheckCeilingDistance();

            float adjustedJumpAngle = jumpAngle;

            if (ceilingDistance <= ceilingThreshold)
            {
                adjustedJumpAngle = 0f;
            }
            else if (ceilingDistance < ceilingAdjustmentRange)
            {
                adjustedJumpAngle = Mathf.Lerp(0f, jumpAngle, (ceilingDistance - ceilingThreshold) / (ceilingAdjustmentRange - ceilingThreshold));
            }

            if (Mathf.Abs(moveInput) < 0.01f)
            {
                float verticalVelocity = maxVerticalVelocity * Mathf.Sin(adjustedJumpAngle * Mathf.Deg2Rad);
                rb.velocity = new Vector2(rb.velocity.x, verticalVelocity);
            }
            else
            {
                float direction = Mathf.Sign(moveInput);
                float verticalForce = maxVerticalVelocity * Mathf.Sin(adjustedJumpAngle * Mathf.Deg2Rad);
                float horizontalForce = maxVerticalVelocity * Mathf.Cos(adjustedJumpAngle * Mathf.Deg2Rad);
                rb.velocity = new Vector2(rb.velocity.x + horizontalForce * direction, verticalForce);
            }

            isJumping = true;
            jumpBufferCounter = 0;
        }

        if (playerColliderChecksScript.isGrounded && isJumping)
        {
            isJumping = false;
        }
    }
    private float CheckCeilingDistance()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, ceilingAdjustmentRange, LayerMask.GetMask("Ground"));
        if (hit.collider != null)
        {
            return hit.distance;
        }
        return ceilingAdjustmentRange;
    }


    private void Update()
    {
        if (!isPlayerFinishedTheLevel)
        {
            HandleMovement();
            HandleJump();

            SetRotation();
        }

		if (Input.GetKeyDown(KeyCode.R))
		{
            envScript.RestartLevel();
        }
    }

    private void OnDrawGizmos()
    {
        if (rb != null)
        {
            Gizmos.color = Color.green;
            Vector2 jumpDirection = new Vector2(Mathf.Cos(jumpAngle * Mathf.Deg2Rad), Mathf.Sin(jumpAngle * Mathf.Deg2Rad)).normalized;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + jumpDirection * 2f);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + Vector3.up * maxJumpHeight);
        }
    }

    [SerializeField] private Gun gun;
    private bool isFacingRight = true;

    private void SetRotation()
    {
        if (gun.transform.position.x < transform.position.x && isFacingRight)
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            isFacingRight = false;

        }
        else if (gun.transform.position.x > transform.position.x && !isFacingRight)
        {
            transform.localScale = new Vector2(-1 * transform.localScale.x, transform.localScale.y);
            isFacingRight = true;
        }
    }
}
