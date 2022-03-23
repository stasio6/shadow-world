using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outro : MonoBehaviour
{
    public float distanceToInteract;
    public float mergeSpeed;
    public float waitSeconds;
    public bool outro;
    bool cutsceneFinished;

    // Start is called before the first frame update
    void Start()
    {
        cutsceneFinished = false;
        Audio.Instance().Stop();
        if (!outro)
        {
            StartCoroutine(SetUIStatus());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.uiStatus == UIManager.UIStatus.CutScene)
        {
            if (outro)
            {
                OutroCutscene();
            }
            else
            {
                IntroCutscene();
            }
        }
        else if (!cutsceneFinished)
        {
            Player player = GameObject.Find("Player").GetComponent<Player>();
            float distanceFromPlayer = Vector2.Distance(player.transform.position, transform.position);
            if (distanceFromPlayer <= distanceToInteract)
            {
                transform.Find("InteractKey").gameObject.SetActive(true);
                if (Utilities.GetInteractOnce())
                {
                    Interact();
                    if (!outro)
                    {
                        GameObject.Find("Shadow Player").transform.position = GameObject.Find("Player").transform.position;
                    }
                }
            }
            else
            {
                transform.Find("InteractKey").gameObject.SetActive(false);
            }
        }
    }

    void IntroCutscene()
    {
        if (GameObject.Find("Top").transform.position.y >= 5 && !cutsceneFinished)
        {
            GameObject.Find("Top").transform.position = Vector3.up * 5;
            GameObject.Find("Bot").transform.position = Vector3.up * -5;
            if (!cutsceneFinished)
            {
                cutsceneFinished = true;
                Time.timeScale = 1;
                GameObject.Find("Shadow Player").transform.position = GameObject.Find("Shadow Player").transform.position;
                UIManager.uiStatus = UIManager.UIStatus.Gameplay;
                StartCoroutine(Victory());
            }
        }
        else if (GameObject.Find("Top").transform.position.y < 5)
        {
            GameObject.Find("Top").transform.position += Vector3.up * mergeSpeed / 1000;
            GameObject.Find("Bot").transform.position += Vector3.down * mergeSpeed / 1000;
        }
    }

    void OutroCutscene()
    {
        if (GameObject.Find("Top").transform.position.y <= 0 && !cutsceneFinished)
        {
            GameObject.Find("Top").transform.position = Vector3.zero;
            GameObject.Find("Bot").transform.position = Vector3.zero;
            if (!cutsceneFinished)
            {
                cutsceneFinished = true;
                Time.timeScale = 1;
                Utilities.HideObject(GameObject.Find("Shadow Player"));
                UIManager.uiStatus = UIManager.UIStatus.Gameplay;
                StartCoroutine(Victory());
            }
        }
        else if (GameObject.Find("Top").transform.position.y > 0)
        {
            GameObject.Find("Top").transform.position += Vector3.down * mergeSpeed / 1000;
            GameObject.Find("Bot").transform.position += Vector3.up * mergeSpeed / 1000;
        }
    }

    void Interact()
    {
        UIManager.uiStatus = UIManager.UIStatus.CutScene;
        Time.timeScale = 0;
        Utilities.HideObject(gameObject);
        Input.ResetInputAxes();
    }

    IEnumerator Victory()
    {
        yield return new WaitForSeconds(waitSeconds);
        Time.timeScale = 1;
        UIManager.uiStatus = UIManager.UIStatus.Victory;
        UIManager.GetInstance().Victory();
    }

    IEnumerator SetUIStatus()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0;
        UIManager.uiStatus = UIManager.UIStatus.SignText;
    }
}
