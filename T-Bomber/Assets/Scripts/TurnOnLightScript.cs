using UnityEngine;
using System.Collections;

public class TurnOnLightScript : MonoBehaviour {
	
	private Light myLight;
	
	// Use this for initialization
	void Start () {
		myLight = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyUp(KeyCode.Space))
		{
			myLight.enabled = !myLight.enabled;
		}
	}
}
