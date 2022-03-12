using System.Collections;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public float distanceToInteract;
    public float timeToUntoggle;
    public Lift[] controlledPlatforms;

    private bool isToggle;
    private bool toggled;
    // Start is called before the first frame update
    void Start()
    {
        isToggle = true;
        if (controlledPlatforms.Length == 0)
        {
            Debug.LogWarning("No Platforms assigned to this level");
        }
        else
        {
            isToggle = controlledPlatforms[0].isToggle;
            foreach (Lift controlledPlatform in controlledPlatforms)
            {
                if (controlledPlatform.isToggle != isToggle)
                {
                    Debug.LogError("Different Platforms have different toggle versions, but they use the same lever!");
                }
            }
        }
        toggled = false;
    }

    // Update is called once per frame
    void Update()
    {
        Player player = GameObject.Find("Player").GetComponent<Player>();
        float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (distanceFromPlayer <= distanceToInteract && IsInteractable())
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

    bool IsInteractable()
    {
        return isToggle || !toggled;
    }

    IEnumerator Untoggle()
    {
        yield return new WaitForSeconds(timeToUntoggle);
        Mirror(gameObject);
        Mirror(transform.Find("InteractKey").gameObject);
        toggled = false;
    }

    void Mirror(GameObject gameObject)
    {
        gameObject.transform.localScale = new Vector3(-gameObject.transform.localScale.x,
                                                      gameObject.transform.localScale.y,
                                                      gameObject.transform.localScale.z);
    }

    void Interact()
    {
        toggled = !toggled;
        foreach (Lift controlledPlatform in controlledPlatforms)
        {
            controlledPlatform.LeverInteraction(toggled);
        }
        Mirror(gameObject);
        Mirror(transform.Find("InteractKey").gameObject);
        if (!isToggle)
        {
            StartCoroutine(Untoggle());
        }
    }
}
