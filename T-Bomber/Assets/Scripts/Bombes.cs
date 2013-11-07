using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bombes : MonoBehaviour {
	
	public int tour;
	public bool valid;
    public GameObject[] player;
    public GameObject[] bloc;
    public Vector3[] positions;
	public ParticleSystem pe;
    

	// Use this for initialization
	void Start () {

        player = GameObject.FindGameObjectsWithTag("Player");
        bloc = GameObject.FindGameObjectsWithTag("CubesD");
		valid = false;
		tour = 0;
        positions = new Vector3[12];
		
	
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < 3; i++)
        {
            positions[0 + i] = new Vector3(this.transform.position.x + 1 + i, 1f, this.transform.position.z);
            positions[3 + i] = new Vector3(this.transform.position.x - 1 - i, 1f, this.transform.position.z);
            positions[6 + i] = new Vector3(this.transform.position.x, 1f, this.transform.position.z + 1 + i);
            positions[9 + i] = new Vector3(this.transform.position.x, 1f, this.transform.position.z - 1 - i);

        }
		
		if(valid == true)
		{
            if (tour == 2)
            {
                Explosion();
            }
		}
	
	}
	
	
	void Explosion()
	{
		
        

        foreach(GameObject pl in player)
        {
            for(int l = 0; l < 12; l++)
            {
                if (pl.transform.position == positions[l])
                {
                    Destroy(pl);
                }
            }
        }
	
		foreach(GameObject bl in bloc)
        {
            for(int k = 0; k < 12; k++)
            {
                if (bl.transform.position == positions[k])
                {
                    Destroy(bl);
                }
            }
        }
		
		
		Destroy(this.gameObject);
		
		
	}
}
