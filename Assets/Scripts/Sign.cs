using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : MonoBehaviour
{
    public float distanceToInteract;
    public string message;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Fix errors after player dies.
        Player player = GameObject.Find("Player").GetComponent<Player>();
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceFromPlayer <= distanceToInteract)
        {
            transform.Find("InteractKey").gameObject.SetActive(true);
            if (Input.GetButtonDown("Interact"))
            {
                Interact();
            }
        }
        else
        {
            transform.Find("InteractKey").gameObject.SetActive(false);
        }
    }

    void Interact()
    {
        Debug.Log("Sign interacted");
        Debug.Log(message);
    }
}
