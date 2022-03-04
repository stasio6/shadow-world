using UnityEngine;

// TODO: Better jumps
//  - Mario style jumps (faster fall)
//  - Off edge security (last second jump)

// TODO: Add "Game Over" screen.
// TODO: Fix errors after player dies.

// TODO: Fix weird microjumps (and then remove edge radius from player)

// TODO: Kolce mają ciut mniejszy hitbox, aby nie zabijać gracza który opiera się o przeszkodę na której są kolce

// TODO: Naprawić infinite jump przy ścianie
// TODO: Naprawić jump będąc w dropdown platformie (a nie na niej)

// TODO: Na spadkach przez capsulecollider się rozjeżdżają symetrie, ehh :/

// TODO: 1-4, gracz po wyskoczeniu z drabiny i pójściu w lewo najwyżej jak się da może się zablokować. Obv to bug, naprawić.

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
            return Input.GetButtonDown(name) && Time.timeScale != 0;
        }
    }
}