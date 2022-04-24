/*using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

[ExecuteInEditMode]
public class PlayFromScene : EditorWindow
{
    [MenuItem("Edit/Play-Stop, But From Prelaunch Scene %&a")]
    public static void PlayFromPrelaunchScene()
    {
        if (EditorApplication.isPlaying == true)
        {
            EditorApplication.isPlaying = false;
            return;
        }

        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/Scenes/Menu/Main Menu.unity");
        EditorApplication.isPlaying = true;
    }
}*/