using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float jumpHeight;
    public bool isShadow;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.gravityScale = jumpSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Make player not jump when paused
        // Vertical move
        
        if (Utilities.GetInput("Jump", false) && IsGrounded()) {
            Jump();
        }

        // Horizontal move
        float movementHorizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(speed * movementHorizontal, rb.velocity.y);
        //Debug.Log(movementHorizontal);
        //Debug.Log(rb.velocity);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Spikes")
        {
            Death();
        }
    }

    void Jump() {
        float jumpForce = Mathf.Sqrt(jumpHeight * -2 * (Physics2D.gravity.y * jumpSpeed));
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    bool IsGrounded()
    {
        return rb.velocity.y == 0;
    }

    void Death()
    {
        gameObject.SetActive(false);
        UIManager.GetInstance().GameOver();
    }
}
