using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float jumpHeight;

    private float upVelocity;
    private bool jumping;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.gravityScale = jumpSpeed;
        upVelocity = 0;
        jumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Vertical move
        if (Input.GetButtonDown("Jump")) {
            Jump();
        }

        // Horizontal move
        float movementHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * movementHorizontal, rb.velocity.y);
    }

    void Jump() {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * jumpSpeed));
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumping = true;
    }
}
