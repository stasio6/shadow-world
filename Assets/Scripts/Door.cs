using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float distanceToInteract;
    public bool isShadow;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isShadow)
        {
            // TODO: Fix errors after player dies.
            Player player = GameObject.Find("Player").GetComponent<Player>();
            float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

            // Shadow distance
            Player shadowPlayer = GameObject.Find("Shadow Player").GetComponent<Player>();
            Door   shadowDoor   = GameObject.Find("Shadow Door").GetComponent<Door>();
            float distanceFromShadowPlayer = Vector2.Distance(shadowPlayer.transform.position, shadowDoor.transform.position);

            if (distanceFromPlayer <= distanceToInteract && distanceFromShadowPlayer <= distanceToInteract)
            {
                transform.Find("InteractKey").gameObject.SetActive(true);
                if (Utilities.GetInput("Interact"))
                {
                    Interact();
                }
            }
            else
            {
                transform.Find("InteractKey").gameObject.SetActive(false);
            }
        }
    }

    void Interact()
    {
        GetComponent<AudioSource>().Play();
        UIManager.GetInstance().Victory();
    }
}
