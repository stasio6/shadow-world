using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public enum UIStatus
    {
        Gameplay,
        PauseMenu,
        SignText,
        GameOver,
        Victory
    }

    public static UIStatus uiStatus;
    public static UIManager _instance;
    public static UIManager GetInstance()
    {
        return _instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        uiStatus = UIStatus.Gameplay;
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Make pause "eat" inputs.
        // TODO: Make state enum.
        switch (uiStatus)
        {
            case UIStatus.Gameplay:
                {
                    if (Input.GetButtonDown("Restart"))
                    {
                        Restart();
                    }
                    if (Input.GetButtonDown("Menu"))
                    {
                        Pause();
                    }
                    break;
                }
            case UIStatus.PauseMenu:
                {
                    if (Input.GetButtonDown("Menu"))
                    {
                        Resume();
                    }
                    break;
                }
            case UIStatus.SignText:
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        Resume();
                    }
                    break;
                }
            case UIStatus.GameOver:
                {
                    if (Input.GetButtonDown("Restart"))
                    {
                        Restart();
                    }
                    break;
                }
            case UIStatus.Victory:
                {
                    if (Input.GetButtonDown("Jump"))
                    {
                        Resume();
                        SceneManager.LoadScene("SampleScene");
                    }
                    break;
                }
        }
    }

    Component UIComponent(string name)
    {
        return transform.Find("Canvas").transform.Find(name);
    }

    void Pause()
    {
        Time.timeScale = 0;
        uiStatus = UIStatus.PauseMenu;
        UIComponent("Pause").gameObject.SetActive(true);
    }

    public void ShowSignText(string text)
    {
        Time.timeScale = 0;
        uiStatus = UIStatus.SignText;
        UIComponent("SignText").gameObject.SetActive(true);
        UIComponent("SignText").transform.Find("SignText").GetComponent<Text>().text = text;
    }

    public void Victory()
    {
        Time.timeScale = 0;
        uiStatus = UIStatus.Victory;
        UIComponent("VictoryText").gameObject.SetActive(true);
    }

    public void GameOver()
    {
        uiStatus = UIStatus.GameOver;
        UIComponent("DeathText").gameObject.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        uiStatus = UIStatus.Gameplay;
        Input.ResetInputAxes();
        UIComponent("Pause").gameObject.SetActive(false);
        UIComponent("SignText").gameObject.SetActive(false);
        UIComponent("VictoryText").gameObject.SetActive(false);
        UIComponent("DeathText").gameObject.SetActive(false);
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
