using UnityEngine;
using System.Collections;

public class ChangeColorCubeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetKeyDown(KeyCode.R))
		{
			gameObject.renderer.material.color = Color.red;
		}
		
		if(Input.GetKeyDown(KeyCode.G))
		{
			gameObject.renderer.material.color = Color.green;
		}
		
		if(Input.GetKeyDown(KeyCode.B))
		{
			gameObject.renderer.material.color = Color.blue;
		}
		
		
	}
}
