using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystallBall : MonoBehaviour
{
    public float distanceToInteract;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        Player shadowPlayer = GameObject.Find("Shadow Player").GetComponent<Player>();
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);

        if (distanceFromPlayer <= distanceToInteract)
        {
            transform.Find("InteractKey").gameObject.SetActive(true);
            if (Utilities.GetInteractOnce())
            {
                Interact(player, shadowPlayer);
            }
        }
        else
        {
            transform.Find("InteractKey").gameObject.SetActive(false);
        }
    }

    void Interact(Player player, Player shadowPlayer)
    {
        // TODO: Improve the sound (shorten it)
        GetComponent<AudioSource>().Play();
        SwapPositions(player.gameObject, shadowPlayer.gameObject);
        SwapPositions(GameObject.Find("Door"), GameObject.Find("Shadow Door"));
        // TODO: Chyba bardziej w klimacie bêdzie nie zmieniaæ kolorów œwiatów
        // GameObject.Find("Shadow").GetComponent<Shadow>();
    }

    void SwapPositions(GameObject a, GameObject b)
    {
        Vector3 aPosition = a.transform.position;
        a.transform.position = b.transform.position;
        b.transform.position = aPosition;
    }
}
