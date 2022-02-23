using UnityEditor;
using UnityEngine;

// TODO: Better jumps
//  - Mario style jumps (faster fall)
//  - Off edge security (last second jump)

namespace Assets.Scripts
{
    public class Todos : ScriptableObject
    {
        [MenuItem("Tools/MyTool/Do It in C#")]
        static void DoIt()
        {
            EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
        }
    }
}