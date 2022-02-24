using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Restart"))
        {
            Restart();
        }
        if (Input.GetButtonDown("Menu"))
        {
            if (Time.timeScale == 0)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0;
        transform.Find("Pause").GetComponent<Pause>().gameObject.SetActive(true);
    }

    public void Resume()
    {
        Debug.Log("click");
        Time.timeScale = 1;
        transform.Find("Pause").GetComponent<Pause>().gameObject.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
    }

    public void LevelSelect()
    {
    }

    public void MainMenu()
    {
    }
}
