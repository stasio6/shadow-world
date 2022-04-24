using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowWall : MonoBehaviour
{
    public float timeToClose;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Shadow Player")
        {
            OpenWall(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == "Shadow Player")
        {
            OpenWall(collider.gameObject);
        }
    }

    private void OpenWall(GameObject shadowPlayer)
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().Play();
        }
        GetComponent<BoxCollider2D>().enabled = false;
        StartCoroutine(CloseWall(shadowPlayer));
    }

    IEnumerator CloseWall(GameObject shadowPlayer)
    {
        yield return new WaitForSeconds(timeToClose);
        BoxCollider2D wallCollider = GetComponent<BoxCollider2D>();
        wallCollider.isTrigger = true;
        wallCollider.enabled = true;
        StartCoroutine(EnableCollider());
    }

    IEnumerator EnableCollider()
    {
        yield return new WaitForSeconds(0.1f);
        BoxCollider2D wallCollider = GetComponent<BoxCollider2D>();
        if (wallCollider.enabled)
        {
            wallCollider.isTrigger = false;
        }
    }
}
