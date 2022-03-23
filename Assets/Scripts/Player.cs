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
    private bool hasKey;
    private int ladderTouches;
    // Start is called before the first frame update
    void Start()
    {
        ccollider = this.GetComponent<CapsuleCollider2D>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.gravityScale = jumpSpeed;
        isClimbing = false;
        hasKey = false;
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
            transform.localScale = new Vector3(-0.2f, transform.localScale.y, transform.localScale.z);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);
        }

        // Animator
        animator.SetFloat("Speed", Mathf.Abs(speed * movementHorizontal));
        animator.SetBool("Grounded", IsGrounded());
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }

    public void RemoveKey()
    {
        hasKey = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Death();
        }
        if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Slime>().alive)
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
        if (collision.gameObject.tag == "Key" && !hasKey && !isShadow)
        {
            hasKey = true;
            transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);
            
            collision.gameObject.transform.SetParent(transform);
            collision.gameObject.transform.localPosition = new Vector3(2f, 1, -1);
            collision.gameObject.transform.localScale *= 0.66f;

            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-0.2f, transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(0.2f, transform.localScale.y, transform.localScale.z);
            }

        }
        if (collision.gameObject.name == "HeadDetect" && isShadow)
        {
            Slime slime = collision.gameObject.transform.parent.GetComponent<Slime>();
            if (slime.alive)
            {
                slime.Death();
                rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y * slime.bounce);
            }
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
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        Utilities.HideObject(this.gameObject);
        GetComponent<AudioSource>().Play();
        UIManager.GetInstance().GameOver();
    }
}
