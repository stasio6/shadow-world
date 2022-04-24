using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSlime : Slime
{
    public Vector3[] destinations;
    int kills = 0;
    public override void Death()
    {
        GetComponent<AudioSource>().Play();
        alive = false;
        if (kills < destinations.Length)
        {
            StartCoroutine(Respawn());
            transform.position = destinations[kills];
            kills++;
        }
        else
        {
            transform.Find("HeadDetect").gameObject.SetActive(false);
            Utilities.HideObject(gameObject);
            Utilities.HideObject(GameObject.Find("SlimeDoor"));
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1);
        alive = true;
    }
}
