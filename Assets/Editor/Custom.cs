using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Custom : EditorWindow
{
    [MenuItem("OpenScene/MainMenu",false)]
    public static void Menu()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scenes/Main.unity");
    }
    [MenuItem("OpenScene/Game", false)]
    public static void Game()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scenes/Game.unity");
    }
    [MenuItem("OpenScene/Game1", false)]
    public static void Game1()
    {
        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
        EditorSceneManager.OpenScene("Assets/Scenes/GameMap1.unity");
    }
}
