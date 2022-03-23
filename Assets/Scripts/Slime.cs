using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float speed;
    public float bounce;
    public bool alive;
    // Start is called before the first frame update
    Rigidbody2D rb;
    GameObject key;
    int right;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed/10, 0);
        right = 1;
        alive = true;
        key = null;
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemies"), LayerMask.NameToLayer("Spikes"), true);

        // Key management
        if (transform.Find("Key"))
        {
            key = transform.Find("Key").gameObject;
            key.transform.localScale *= 0.5f;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        right *= -1;
        rb.velocity = new Vector2(right * speed / 10, 0);
    }

    public virtual void Death()
    {
        GetComponent<AudioSource>().Play();
        if (key)
        {
            key.transform.localScale *= 2f;
            key.transform.parent = transform.parent;
        }
        transform.Find("HeadDetect").gameObject.SetActive(false);
        Utilities.HideObject(gameObject);
        alive = false;
        
    }
}
