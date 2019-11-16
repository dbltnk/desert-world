using UnityEngine;
using System.Collections;
using UnityEditor;

public class EditorSceneSwitcher : MonoBehaviour {
	[MenuItem("Scenes/0_menu")]
	static void Open0()
	{
		EditorApplication.OpenScene("Assets/Scenes/0_menu.unity");
	}

	[MenuItem("Scenes/1_game")]
	static void Open1()
	{
		EditorApplication.OpenScene("Assets/Scenes/1_game.unity");
	}

}
