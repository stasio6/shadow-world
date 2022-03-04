using System.Collections;
using System.Collections.Generic;
using System;
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

    string[] levels = new string[] { "Level1-1", "Level1-2", "Level1-3", "Level1-4", "Level1-5", "Level1-6", "Level1-7", "Level1-8",
                                     "Level2-1", "Level2-2", "Level2-3", "Level2-4", "Level2-5", "Level2-6", "Level2-7", "Level2-8",
                                     "Level3-1", "Level3-2", "Level3-3", "Level3-4", "Level3-5", "Level3-6", "Level3-7", "Level3-8",
                                     "Level4-1", "Level4-2", "Level4-3", "Level4-4", "Level4-5", "Level4-6", "Level4-7", "Level4-8",
                                     "credits"
    };

    // Start is called before the first frame update
    void Start()
    {
        uiStatus = UIStatus.Gameplay;
        _instance = this;
        ScaleComponents(this.gameObject);
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
                        SceneManager.LoadScene(NextLevel(SceneManager.GetActiveScene().name));
                    }
                    break;
                }
        }
    }

    Component UIComponent(string name)
    {
        return transform.Find("Canvas").transform.Find(name);
    }

    void ScaleComponents(GameObject gameObject)
    {
        if (gameObject.name == "SignBackground")
        {
            Debug.Log(gameObject.transform.GetComponent<RectTransform>().localScale);
            Debug.Log(UnityEngine.Camera.main.orthographicSize);
            RectTransform rectTransform = gameObject.transform.GetComponent<RectTransform>();
            rectTransform.localScale *= UnityEngine.Camera.main.orthographicSize / 10;
            rectTransform.localScale.Set(rectTransform.localScale.x, rectTransform.localScale.y, 2);
        }
        foreach (Transform child in gameObject.transform)
        {
            ScaleComponents(child.gameObject);
        }
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

    public string NextLevel(string currentLevel)
    {
        Debug.Log(levels[Array.FindIndex(levels, element => element == currentLevel) + 1]);
        return levels[Array.FindIndex(levels, element => element == currentLevel) + 1];
    }
}
