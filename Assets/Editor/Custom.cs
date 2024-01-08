using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Custom : EditorWindow
{
    [MenuItem("OpenScene/Main",false)]
    public static void Menu()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
    }
    [MenuItem("OpenScene/Start", false)]
    public static void Game()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scenes/StartGame.unity");
    }
    
}
