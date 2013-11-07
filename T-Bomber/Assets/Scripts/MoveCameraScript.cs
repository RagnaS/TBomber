using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveCameraScript : MonoBehaviour {

	#region Variables privées
	private GameObject checkPoint1;
	private GameObject checkPoint2;
	private GameObject checkPoint3;

	public GameObject CheckPoint1 {
		get {
			return this.checkPoint1;
		}
		set {
			checkPoint1 = value;
		}
	}

	public GameObject CheckPoint2 {
		get {
			return this.checkPoint2;
		}
		set {
			checkPoint2 = value;
		}
	}

	public GameObject CheckPoint3 {
		get {
			return this.checkPoint3;
		}
		set {
			checkPoint3 = value;
		}
	}

	public GameObject CheckPoint4 {
		get {
			return this.checkPoint4;
		}
		set {
			checkPoint4 = value;
		}
	}

	private GameObject checkPoint4;
	private GenerationAleatoireTerrainScript terrainScript;
	public GenerationAleatoireTerrainScript TerrainScript {
		get {
			return this.terrainScript;
		}
		set {
			terrainScript = value;
		}
	}
	
	private int largeurTerrain;
	public int LargeurTerrain {
		get {
			return this.largeurTerrain;
		}
		set {
			largeurTerrain = value;
		}
	}
	
	private int longueurTerrain;
	public int LongueurTerrain {
		get {
			return this.longueurTerrain;
		}
		set {
			longueurTerrain = value;
		}
	}	

	public int Step {
		get {
			return this.step;
		}
		set {
			step = value;
		}
	}	
	private int step;
	private List<Vector3> positions;
	private bool begin = true;
	#endregion
	
	#region Variables publiques
	public Transform target = null;
	public float moveSpeed = 0.2f;
	#endregion
	
	// Use this for initialization
	void Start () {
		TerrainScript = GetComponent<GenerationAleatoireTerrainScript>();
		LongueurTerrain = TerrainScript.X;
		LargeurTerrain = TerrainScript.Y;
		positions = new List<Vector3>();
		
		CheckPoint1 = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/CameraCheckpoint.prefab", typeof(GameObject)) ) as GameObject;
		positions.Add(CheckPoint1.transform.position = new Vector3(0,10,0));
		CheckPoint2 = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/CameraCheckpoint.prefab", typeof(GameObject)) ) as GameObject;
		positions.Add(CheckPoint2.transform.position = new Vector3(LongueurTerrain,10,0));
		CheckPoint3 = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/CameraCheckpoint.prefab", typeof(GameObject)) ) as GameObject;
		positions.Add(CheckPoint3.transform.position = new Vector3(LongueurTerrain,10,LargeurTerrain));
		CheckPoint4 = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/CameraCheckpoint.prefab", typeof(GameObject)) ) as GameObject;
		positions.Add(CheckPoint4.transform.position = new Vector3(0,10,LargeurTerrain));
		
		transform.position = CheckPoint1.transform.position;
		transform.LookAt(new Vector3(LongueurTerrain/2, 0, LargeurTerrain/2));
		Step = 1;
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("fonction Update");
		if(target==null)
		{
			transform.LookAt(new Vector3(10,0,10));
			Debug.Log("if begin");
			if(positions[Step-1].x-1<=transform.position.x && 
				transform.position.x<=positions[Step-1].x+1 && 
				positions[Step-1].z-1<=transform.position.z && 
				transform.position.z<=positions[Step-1].z+1)
			{
				if(Step == 4)
				{
					Step = 1;
					//transform.position = Vector3.Lerp(transform.position, positions[Step-1], Time.deltaTime*moveSpeed);
				}
				else
				{
					Step++;
					//transform.position = Vector3.Lerp(transform.position, positions[Step-1], Time.deltaTime*moveSpeed);
				}
			}
			transform.position = Vector3.Lerp(transform.position, positions[Step-1], Time.deltaTime*moveSpeed);
		}
		else
		{
			transform.LookAt(target);
			transform.position = target.position + new Vector3(0,3,2);
		}
	}
}
