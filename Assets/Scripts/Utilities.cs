using UnityEngine;

// TODO: Na spadkach przez capsulecollider się rozjeżdżają symetrie, ehh :/

// TODO: 1-4, gracz po wyskoczeniu z drabiny i pójściu w lewo najwyżej jak się da może się zablokować. Obv to bug, naprawić.
// TODO: 2-4, gracz tnie się w wąskiej drabinie

// TODO: Click sound on buttons don't work in pause for level select and main menu
// TODO: Nie działa też zawsze

// TODO: Complete credentials

// TODO: Uwagi Ewy
// TODO: Level 3 kolce śmiesznie są w ścianie na górze po lewej

// TODO: Nicer attached key

// TODO: Better door unlock audio (shorter beggining, faster sound play speed (x1.5))

// TODO: Brzydkie animacje gdy jest nisko i się duże skacze w bardzo niskim

// TODO: Jeśli złapiemy klucz idąc w lewo, jego kierunek się psuje...

// TODO: Level 2-4 bardzo zyska na shadow wallach. Można zdecydowanie dużo dobudować.
// Można otworzyć dźwignię dla dolnego, który przechodzi level przez shadow walle, by dojść do dźwigni, wtedy swap i powrót po klucz
// TODO: Dodaj tam shadow wall, zmień na level 2-5/6

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

    static int lastFrameCount = 0;
    public static bool GetInteractOnce()
    {
        if (Time.frameCount == lastFrameCount)
        {
            return false;
        }
        bool result = GetInput("Interact");
        if (result)
        {
            lastFrameCount = Time.frameCount;
        }
        return result;
    }

    public static void HideObject(GameObject gameObject)
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -100);
        gameObject.GetComponent<Collider2D>().enabled = false;
    }
}

// Credits
// Door close: https://freesound.org/people/angelkunev/sounds/519065/
// Door squeak: https://freesound.org/people/InspectorJ/sounds/346212/
// Paper: https://freesound.org/people/InspectorJ/sounds/415766/
// Death: https://freesound.org/people/ThatGuyWithTheBeard/sounds/253478/
// Restart: https://freesound.org/people/michorvath/sounds/269503/
// Click: https://freesound.org/people/LittleRobotSoundFactory/sounds/288953/
// Magia: https://freesound.org/people/sound_designer_from_Turkey/sounds/613163/
// Kłódka: https://stock.pixlr.com/creator/stockunlimited
// Dziurka od klucza: https://icon-icons.com/icon/keyhole-shape/53963
// Klucz: https://www.pngitem.com/middle/oRbibi_key-vector-key-graphics-image-clipart-clipart-key/
// Klucz2: https://www.pngitem.com/middle/hJTbimx_vector-key-transparent-key-vector-png-png-download/
// Magiczna kula: https://pngpart.com/images/bt/crystal-ball-13.png
// Dźwignia: https://www.clipartmax.com/middle/m2H7H7i8N4N4A0N4_lever-free-icon-scalable-vector-graphics/
// Spirala: https://www.vecteezy.com/vector-art/370395-spring-vector-illustration-in-3d-metal-and-black-line-drawing
// OST: Stickman golf