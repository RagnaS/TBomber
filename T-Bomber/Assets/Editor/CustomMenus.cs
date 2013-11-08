using UnityEngine;
using System.Collections;
using UnityEditor;

public class CustomMenuScript : EditorWindow 
{	
	
	
	[MenuItem("CustomButtons/Queueleuleu")]
	static public void Queueleuleu()
	{
		int i=0;
		foreach (var obj in Selection.gameObjects)
		{
			obj.transform.position =  Vector3.right * i++;
		}
	}
	
	[MenuItem("CustomButtons/cote")]
	static public void Cote()
	{
		int i=0;
		foreach (var obj in Selection.gameObjects)
		{
			obj.transform.position =  Vector3.forward * i++;
		}
	}
	
	[MenuItem("CustomButtons/Showwindow")]
	static public void Showwindow()
	{
		var window = new CustomMenuScript();
		window.Show();
	}
	
	void OnEditorGui()
	{
		EditorGUILayout.BeginHorizontal();
	}
	
}
