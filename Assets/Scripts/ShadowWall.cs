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
            GetComponent<AudioSource>().Play();
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(CloseWall());
        }
    }

    IEnumerator CloseWall()
    {
        yield return new WaitForSeconds(timeToClose);
        // TODO: If still collides, don't activate (it will push player outside)
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
