using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO; 

public class GenerationAleatoireTerrainScript : MonoBehaviour {
	
	#region Variables privées
	private int x;
	private int y;
	private float demiCube = 0.5f;
	private int chanceCaisseArene = 30;
	private int taillemap = 40;
	private CaseClass caseClass;

	public CaseClass CaseClass {
		get {
			return this.caseClass;
		}
		set {
			caseClass = value;
		}
	}

	public int Taillemap {
		get {
			return this.taillemap;
		}
		set {
			taillemap = value;
		}
	}
	
	public int ChanceCaisseArene {
		get {
			return this.chanceCaisseArene;
		}
		set {
			chanceCaisseArene = value;
		}
	}

	public float DemiCube {
		get {
			return this.demiCube;
		}
		set {
			demiCube = value;
		}
	}

	public int X {
		get {
			return this.x;
		}
		set {
			x = value;
		}
	}

	public int Y {
		get {
			return this.y;
		}
		set {
			y = value;
		}
	}

	private LabyClass labyClass;
	
	public LabyClass LabyClass {
		get {
			return this.labyClass;
		}
		set {
			labyClass = value;
		}
	}
	#endregion
	
	void Start(){
		CaseClass = GetComponent<CaseClass>();
		LabyClass = GetComponent<LabyClass>();
		X = taillemap/2;
		Y = taillemap/2;
		LabyClass.Laby a = new LabyClass.Laby(X, Y);
		a.generer();
		GameObject ground = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Ground.prefab", typeof(GameObject)) ) as GameObject;
		ground.transform.localScale = new Vector3(2,2,2);
		ground.transform.position = new Vector3(X/2,0,Y/2);
		
		if(generateArena(a, X, Y))
			Debug.Log("Generation du terrain terminée");
		else
			Debug.Log("Error");
	}
	
	bool generateArena(LabyClass.Laby a, int longueur, int largeur)
	{
		for(int i = 0; i<longueur; i++)
		{
			GameObject limite1 = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Cube_Indestructible.prefab", typeof(GameObject)) ) as GameObject;
			GameObject limite2 = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Cube_Indestructible.prefab", typeof(GameObject)) ) as GameObject;
			GameObject limite3 = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Cube_Indestructible.prefab", typeof(GameObject)) ) as GameObject;
			GameObject limite4 = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Cube_Indestructible.prefab", typeof(GameObject)) ) as GameObject;
			limite1.transform.Translate(i+DemiCube,DemiCube,0-DemiCube);
			limite2.transform.Translate(i+DemiCube,DemiCube,20+DemiCube);
			limite3.transform.Translate(0-DemiCube,DemiCube,i+DemiCube);
			limite4.transform.Translate(20+DemiCube,DemiCube,i+DemiCube);
			
			for(int j = 0; j<largeur; j++)
			{
				switch(a.getValue(i,j))
				{
					case "##":
						if(Random.Range(0,100)>20)
						{	
							GameObject caisse = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Cube_Indestructible.prefab", typeof(GameObject)) ) as GameObject;
							caisse.transform.Translate(i+DemiCube,DemiCube,j+DemiCube);
						}
						else
						{
							GameObject caisse = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Cube.prefab", typeof(GameObject)) ) as GameObject;
							caisse.transform.Translate(i+DemiCube,DemiCube,j+DemiCube);
						}
						break;
					case "  ":
						if(Random.Range(0,100)<chanceCaisseArene)
						{	
							GameObject caisse = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Cube.prefab", typeof(GameObject)) ) as GameObject;
							caisse.transform.Translate(i+DemiCube,DemiCube,j+DemiCube);
						}
						break;
				}
			}
		}
		return true;
	}
}