using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


public class GameManagerScript : MonoBehaviour {
	
	private GenerationAleatoireTerrainScript terre;
	public GenerationAleatoireTerrainScript Terre {
		get {
			return this.terre;
		}
		set {
			terre = value;
		}
	}

	private Player pl;
	public Player Pl {
		get {
			return this.pl;
		}
		set {
			pl = value;
		}
	}
	
	
	
	public static GameManagerScript instance;
	
   
	public GameObject[] player = new GameObject[3]; // tableau ou seront stockés les personnages sélectionnés joueur
	public GameObject[] enemy = new GameObject[3]; // idem mais ennemis
    public int k;
	public int nbjoueurs = 2; //nombres de joueurs
 	public int c = 0; //compteur pour l'ordre des joueurs
    private GameObject[] bombes;
	public int compt;
	
	[SerializeField]
	List<List<Player>> team = new List<List<Player>>(); //Liste contenant des listes de joueur servant à définir les équipes sur la map
	List<Player> active = new List<Player>(); //liste servant à définir l'équipe active
	
	public int curplayerind = 0; // variable définissant le joueur actif

	public GameObject[] Bombes {
		get {
			return this.bombes;
		}
		set {
			bombes = value;
		}
	}
	public List<List<Player>> Team {
		get {
			return this.team;
		}
		set {
			team = value;
		}
	}
	public List<Player> Active {
		get {
			return this.active;
		}
		set {
			active = value;
		}
	}	
	
	void Awake()
	{
		instance = this;
	}
	
	void Start () {
		
		Terre = GetComponent<GenerationAleatoireTerrainScript>();
		while(Terre.Finished != true)
		{}
        CreationJoueur(); // fonction de création des joueurs
		compt = 0;
		Terre.Finished = false;
	}
	

	void Update () 
	{
        Bombes = GameObject.FindGameObjectsWithTag("Bombe"); // récupération des bombes sur le terrain
		Active = Team[curplayerind]; // équipe active
		Pl = Active[c];
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			if(c!=0)
			{
				if(Active[c-1].Active == true)
				{
					Active[c-1].Active = false; // désactivation du personnage sélectionné precedemment
				}
			}
			

			Pl.Active = true; // activation du personnage sélectionné
			c++;
		}
		
		for(int h = 0; h <Active.Count; h++)
		{
			
			if(Active[h] == null)
			{
				compt +=1;
			}
		}
		
		if(compt == 3)
		{
			Debug.Log ("Fin de partie");
            Application.Quit();
		}
		else
		{
			compt = 0;
		}
		
		
		
		if (c == 3)
		{
            Active[c - 1].Active = false;
			NextTurn(); // Passage au tour suivant
			c = 0;
		}
	}
	
	public void NextTurn() // fonction pour définir le tour des joueurs
	{
		if(curplayerind < nbjoueurs)
		{
			curplayerind += 1;
		}
	
        if(curplayerind == nbjoueurs)
		{
			curplayerind = 0;
            for (int b = 0; b < bombes.Length; b++)
            {
                Bombes[b].GetComponent<Bombes>().tour += 1; //a la fin du tour de tout les joueurs le compteur des bombes augmente de un
            }
		}

        
        
	}
	
	


    void CreationJoueur() // fonction de création des équipes
    {
		Team = new List<List<Player>>(); //liste des équipe
		int currentPosition = 0;
		List<List<int>> startPositions = getPositionLibre(nbjoueurs);
		int coordonneeZ = 0;
		int coordonneeX = 0;
		int aleat = 0;
		for(int l = 0; l < nbjoueurs; l++)
		{
		       Vector3 pos =  player[0].transform.position = new Vector3(0.0f, 1.5f, 0.0f); // position de base
				List<Player> pers = new List<Player>(); // liste définissante une équipe
				if(nbjoueurs == 1)
					coordonneeZ = 0;
				if(nbjoueurs == 2)
					coordonneeZ = Terre.Y;
		        for ( k = 0; k < 3; k++) // boucle servant à instantier les joueurs et les placer dans une équipe
		        {	
					if(l == 0)
					{
						aleat = Random.Range(0, startPositions[0].Count);
						coordonneeX = startPositions[0][aleat];
		           		 Player joue = ((GameObject)Instantiate(player[k], new Vector3(currentPosition, 1f, coordonneeZ), Quaternion.Euler(new Vector3()))).GetComponent<Player>(); // instantiation des personnages en haut a gauche du terrain
						 pers.Add(joue); // ajout ds joueurs dans la liste
					}
				
					if(l == 1)
					{
						 Player joue2 = ((GameObject)Instantiate(enemy[k], new Vector3(currentPosition, 1f, coordonneeZ), Quaternion.Euler(new Vector3()))).GetComponent<Player>(); // idem mais a droite
						 pers.Add(joue2);
					}
		        }
			Team.Add(pers); //ajoutes des équipes dans la liste*/
		}	
    }
	
	List<List<int>> getPositionLibre(int nbJoueurs)
	{	
		switch(nbJoueurs)
		{
		case 1:
			List<int> casesLibres = new List<int>();
			break;
		case 2:
			List<int> casesLibres2P1 = new List<int>();
			List<int> casesLibres2P2 = new List<int>();
			List<List<int>> listeFinal = new List<List<int>>();
			for(int i = 0; i<Terre.X ; i++)
			{
				if(Terre.A.getValue(i,0) == "  " )
					casesLibres2P1.Add(i);
				if(Terre.A.getValue(i,Terre.Y-1) == "  " )
					casesLibres2P2.Add(i);
			}
			listeFinal.Add(casesLibres2P1);
			listeFinal.Add(casesLibres2P2);
			return listeFinal;
		case 3:
			List<int> casesLibres3P1 = new List<int>();
			List<int> casesLibres3P2 = new List<int>();
			List<int> casesLibres3P3 = new List<int>();
			break;
		case 4:
			List<int> casesLibres4P1 = new List<int>();
			List<int> casesLibres4P2 = new List<int>();
			List<int> casesLibres4P3 = new List<int>();
			List<int> casesLibres4P4 = new List<int>();
			break;
		}
		return null;
	}
}
