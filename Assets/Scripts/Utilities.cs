using UnityEngine;

// TODO: Better jumps
//  - Mario style jumps (faster fall)
//  - Off edge security (last second jump)

// TODO: Add "Game Over" screen.
// TODO: Fix errors after player dies.

// TODO: Fix weird microjumps (and then remove edge radius from player)

// TODO: Kolce mają ciut mniejszy hitbox, aby nie zabijać gracza który opiera się o przeszkodę na której są kolce

public class Utilities : MonoBehaviour
{
    public static bool GetInput(string name, bool onlyInGameplay = true)
    {
        if (onlyInGameplay)
        {
            return Input.GetButtonDown(name) && UIManager.uiStatus == UIManager.UIStatus.Gameplay;
        }
        else
        {
            if (Input.GetButtonDown(name)) 
            {
                Debug.Log(name);
                Debug.Log(Time.timeScale);
            }
            return Input.GetButtonDown(name) && Time.timeScale != 0;
        }
    }
}