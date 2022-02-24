using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Shadowize(GameObject.Find("Bottom"));
        Shadowize(GameObject.Find("Shadow Player"));
        Shadowize(GameObject.Find("Shadow Door"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Shadowize(GameObject gameObject)
    {
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.2f, 0.2f, 0.2f, 1);
        }
        foreach (Transform child in gameObject.transform)
        {
            Shadowize(child.gameObject);
        }
    }
}
