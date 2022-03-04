using UnityEditor;
using UnityEngine;
using System.IO;
using System.Linq;
using System.Collections;

/*[ExecuteInEditMode]
public class PlayFromScene : EditorWindow
{
    [SerializeField] string lastScene = "";
    [SerializeField] int targetScene = 0;
    [SerializeField] string waitScene = null;
    [SerializeField] bool hasPlayed = false;
    [MenuItem("Edit/Play-Stop, But From Prelaunch Scene %0")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        EditorApplication.SaveCurrentSceneIfUserWantsTo();
        EditorApplication.OpenScene("Assets/whatever/YourPrepScene.unity");
        EditorApplication.isPlaying = true;
    }
}*/

// TODO: Make it work!!!