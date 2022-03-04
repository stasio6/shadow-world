using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float jumpHeight;
    public float climbSpeed;
    public bool isShadow;

    private Rigidbody2D rb;
    private CapsuleCollider2D ccollider;
    private Animator animator;
    private bool isClimbing;
    private int ladderTouches;
    // Start is called before the first frame update
    void Start()
    {
        ccollider = this.GetComponent<CapsuleCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.gravityScale = jumpSpeed;
        isClimbing = false;
        ladderTouches = 0;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("DropdownPlatforms"), true);
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Horizontal move
        float movementHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * movementHorizontal, rb.velocity.y);

        // Jump
        if (Utilities.GetInput("Jump", false) && IsGrounded() && !isClimbing)
        {
            Jump();
            animator.SetTrigger("Jump");
            // TODO: czy trzeba resetowaæ?
        }

        // Ladders

        if (isClimbing)
        {
            float verticalSpeed = Input.GetAxis("Jump");
            if (verticalSpeed > 0)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, verticalSpeed * climbSpeed);
                animator.SetBool("Climbing", true);
            }
            else
            {
                rb.gravityScale = jumpSpeed;
                animator.SetBool("Climbing", false);
            }
        }
        else
        {
            animator.SetBool("Climbing", false);
        }

        // Dropdown
        // TODO: Trochê podskakuje na platformach -> wywalone, platformy s¹ ma³e i krótkie
        if (Input.GetButton("Drop") || rb.velocity.y > 0)
        {
            transform.Find("DropdownPlatformCollider").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("DropdownPlatformCollider").gameObject.SetActive(true);
        }

        // Orientation
        if (rb.velocity.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // Animator
        animator.SetFloat("Speed", Math.Abs(speed * movementHorizontal));
        animator.SetBool("Grounded", IsGrounded());
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Death();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            isClimbing = true;
            ladderTouches++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            ladderTouches--;
            if (ladderTouches == 0)
            {
                isClimbing = false;
                rb.gravityScale = jumpSpeed;
            }
        }
    }

    void Jump() {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * jumpSpeed));
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    bool IsGrounded()
    {
        float extraHeight = 0.1f;
        float sidesMargin = 0.1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(new Vector2(ccollider.bounds.center.x, ccollider.bounds.center.y - ccollider.bounds.extents.y),
                                                    new Vector2(ccollider.bounds.size.x - 2 * sidesMargin, 0.1f), 
                                                    0f, Vector2.down, extraHeight,
                                                    LayerMask.GetMask("Platforms") | LayerMask.GetMask("DropdownPlatforms"));
        bool result = (raycastHit.collider != null);
        return result;
    }

    void Death()
    {
        gameObject.SetActive(false);
        UIManager.GetInstance().GameOver();
    }
}
