//2013 Mickaël Braga
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	#region variables privées
	[SerializeField]
	private int move = 0; //variable de mouvement
	private string capacity; // variable de capacité	
	private string role; //variable de stockage du role
	private bool active = false; // booleen pour savoir si le personnage est seléctionné
	private bool selectionnable = true; // booleen pour savoir si le personnage peut etre selectionné
	private bool menu = false; // Booléen du menu permettant de déterminer son apparition
	private bool mouvement = false; //Booléen permettant de savoir si l'on va déplacer le personnage
	private bool poser = false; // Booléen permettant de savoir si l'on va placer une bombe
	private bool bouger = false;
	
	private Vector3 positionpotentiel; //variable de position potentielle ou l'on déplacera le personnage
	[SerializeField]
	private int compteuraction; //Variable des points d'actions
	private int cheminprec;
	private GameObject bombe;
	private GameObject cube;
	[SerializeField]
	private string[] caseprecmouv;
	#endregion
	
	
	List<GameObject> chemin = new List<GameObject>(); // Liste ou sera contenu le chemin de mouvement

	
	#region encapsulateurs

	public bool Bouger {
		get {
			return this.bouger;
		}
		set {
			bouger = value;
		}
	}

	public int Cheminprec {
		get {
			return this.cheminprec;
		}
		set {
			cheminprec = value;
		}
	}
	public string[] Caseprecmouv {
		get {
			return this.caseprecmouv;
		}
		set {
			caseprecmouv = value;
		}
	}
	public bool Menu {
		get {
			return this.menu;
		}
		set {
			menu = value;
		}
	}	
	public bool Mouvement {
		get {
			return this.mouvement;
		}
		set {
			mouvement = value;
		}
	}
	public bool Poser {
		get {
			return this.poser;
		}
		set {
			poser = value;
		}
	}
	public int Move {
			get {
				return this.move;
			}
			set {
				move = value;
			}
		}	
	public string Capacity {
			get {
				return this.capacity;
			}
			set {
				capacity = value;
			}
		}	
	public string Role {
			get {
				return this.role;
			}
			set {
				role = value;
			}
		}
	public bool Active {
		get {
			return this.active;
		}
		set {
			active = value;
		}
	}
	public bool Selectionnable {
		get {
			return this.selectionnable;
		}
		set {
			selectionnable = value;
		}
	}
	public Vector3 Positionpotentiel {
		get {
			return this.positionpotentiel;
		}
		set {
			positionpotentiel = value;
		}
	}
	public int Compteuraction {
		get {
			return this.compteuraction;
		}
		set {
			compteuraction = value;
		}
	}
	public GameObject Bombe {
		get {
			return this.bombe;
		}
		set {
			bombe = value;
		}
	}	
	public GameObject Cube {
		get {
			return this.cube;
		}
		set {
			cube = value;
		}
	}
	#endregion
	
	// Use this for initialization
	void Start () 
	{
			compteuraction = 2;
			Role = this.name.ToString(); // recupération du nom de l'objet joueur
			GameObject Cube = new GameObject();
			Cheminprec = 0;
			switch(role)// switch servant a déterminer les statistiques liés aux roles
			{
				case "Char_1(Clone)" : 
								Move = 3;
								Capacity = "Glue";break; 
				case "Char_2(Clone)" : 
								Move = 6;
								Capacity = "Nothing";break;//variable définie en tant que nothing si le personnage n'a pas de capacité spéciale
				case "Char_3(Clone)" : 
								Move = 3;
								Capacity = "Kick";break;
				case "Char_4(Clone)" : 
								Move = 2;
								Capacity = "Desamorce";break;
				case "Char_5(Clone)" : 
								Move = 3;
								Capacity = "Mine";break;
				case "Char_6(Clone)" : 
								Move = 3;
								Capacity = "Construire";break;
				case "Default": 
								Debug.LogError("Erreur dans le modèle joueur");break;
			}
			
			
			
			
			
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		
		if (Compteuraction > 0) 
		{
					if (Active == true) { //Si le personnage est actif sa couleur devient verte 
						menu = true;
						this.renderer.material.color = Color.green;
					} else {
						this.renderer.material.color = Color.white;
					}
				
					if (Mouvement == true) { // si le personnage est passer en mode mouvement les input réagissent en créant le chemin de mouvement
						if (Move != 0) {
							if (Input.GetKeyDown (KeyCode.LeftArrow)) {
								
								
									Gauche ();
									Caseprecmouv [Cheminprec] = "left";
									Cheminprec++;
									Move -= 1;
								
							}
							
							if (Input.GetKeyDown (KeyCode.RightArrow)) {
								Droite ();
								Caseprecmouv [Cheminprec] = "right";
								Cheminprec++;
								Move--;
							}
							
							if (Input.GetKeyDown (KeyCode.UpArrow)) {
								Haut ();
								Caseprecmouv [Cheminprec] = "up";
								Cheminprec++;
								Move -= 1;
							}
							
							if (Input.GetKeyDown (KeyCode.DownArrow)) {
								Bas ();
								Caseprecmouv [Cheminprec] = "down";
								Cheminprec++;
								Move -= 1;
							}
							
						
						
						}
					
						if (Input.GetKey(KeyCode.Z)) {
							
                            Bouger = true;
                            

                            
                            
							
						}
				
						if(Bouger == true)
						{
                            
							Bougerpers ();
                            
                           
						}


                        if (compteuraction != 0)
                        {
                            Menu = true; // tant que le compteur d'action n'est pas égal a  0 le menu est présent
                        }
                        
					
					
					}
				
					if (Poser == true) { // Si Poser est true alors les input servent a placer une bombe
						if (Input.GetKeyDown (KeyCode.LeftArrow)) {
							Bombe.SetActive (false); // La bombe est passer en false pour qu'elle ne soit pas apparente avant son instantiation au point voulu
							Placergauche ();
						}
						
						if (Input.GetKeyDown (KeyCode.RightArrow)) {
							Bombe.SetActive (false);
							Placerdroite ();
						}
						
						if (Input.GetKeyDown (KeyCode.UpArrow)) {
							Bombe.SetActive (false);
							Placerhaut ();
						}
						
						if (Input.GetKeyDown (KeyCode.DownArrow)) {
							Bombe.SetActive (false);
							Placerbas ();
						}
						
						if (Input.GetKeyDown(KeyCode.Z)) {
							Poser = false;
							verifcompteuraction();
                            Bombe.gameObject.GetComponent<Bombes>().valid = true; // activation de la bombe
						}
					}
				
					if (Input.GetKeyDown(KeyCode.Escape)) {
						Menu = false;
					}
		} 
		else 
		{
			Menu = false;
			selectionnable = false;
		}
	}
	
	
	#region Gui	
	void OnGUI()
	{
        //Creation du gui
        //Les gui button sont des boutons intégrés directement a unity
        //La box sers a montrer le nombre de points d'action du joueur
		
		if(Menu == true)
		{
			GUI.Box(new Rect(0,20,120,40),"Point d'actions :"+Compteuraction);
			
			if(GUI.Button(new Rect(130,20,100,30),"Deplacement"))
			{
				Deplacement();
				Poser = false;
				Destroy(Bombe);
				

			}
			
			if(GUI.Button(new Rect(130,60,100,30),"Placer Bombe"))
			{
				PlacerBombe();
				Mouvement = false;
				Bouger = false;
				for (int cl = 0; cl < chemin.Count; cl++) 
					{
						Destroy (chemin [cl]); //Destruction du chemin une fois qu'il a servi
									
					}
			}
			
			/*if(GUI.Button(new Rect(130,100,100,30),"Capacité"))
			{
				Capacite();
			}*/
		}
	}
	#endregion
	
	#region fonctions de déplacement
	void Deplacement()
	{
		Caseprecmouv = new string[6]; //Tableau qui servira a recueuillir le type de mouvement sélectionner
		Menu = false;
		positionpotentiel = this.gameObject.transform.position; // mise en positionpotentiel sur le joueur
		chemin = new List<GameObject>(); // création d'une nouvelle liste chemin
		Cube = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Marqueur_mouvement.prefab", typeof(GameObject)) ) as GameObject;
		Cube.transform.position = new Vector3(positionpotentiel.x, 0.1f, positionpotentiel.z-1);
		
		Mouvement = true;
		Destroy(Cube);

	}
	
	
	void Haut()
	{
		Cube = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Marqueur_mouvement.prefab", typeof(GameObject)) ) as GameObject;
		Cube.transform.position = new Vector3(positionpotentiel.x, 0.1f, positionpotentiel.z+1);
		positionpotentiel = Cube.transform.position;	// La position potentielle devient égale a la position du marqueur			
		chemin.Add(Cube);
	}
	
	void Bas()
	{
		Cube = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Marqueur_mouvement.prefab", typeof(GameObject)) ) as GameObject;
		Cube.transform.position = new Vector3(positionpotentiel.x, 0.1f, positionpotentiel.z-1);
		positionpotentiel = Cube.transform.position;				
		chemin.Add(Cube);
	}
	
	void Droite()
	{
		Cube = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Marqueur_mouvement.prefab", typeof(GameObject)) ) as GameObject;
		Cube.transform.position = new Vector3(positionpotentiel.x+1, 0.1f, positionpotentiel.z);
		positionpotentiel = Cube.transform.position;				
		chemin.Add(Cube);
	}
	
	void Gauche()
	{
		Cube = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Marqueur_mouvement.prefab", typeof(GameObject)) ) as GameObject;
		Cube.transform.position = new Vector3(positionpotentiel.x-1, 0.1f, positionpotentiel.z);
        positionpotentiel = Cube.transform.position;
		chemin.Add(Cube);
	}
	
	void Bougerpers()
	{
		if(this.gameObject.transform.position != positionpotentiel)
			{
				 this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position,new Vector3(Positionpotentiel.x,1f,Positionpotentiel.z), 3f * Time.deltaTime); //Déplacement du personnage
				if(this.gameObject.transform.position == Positionpotentiel)
				{

                    
					Bouger = false;	
					Mouvement = false;
                    verifcompteuraction();
				}

				for (int cl = 0; cl < chemin.Count; cl++) 
				{
								Destroy (chemin [cl]); //Destruction du chemin une fois qu'il a servi
									
				}
                
			}
		
			
		
	}
	#endregion
	
	#region Fonctions des bombes
	void PlacerBombe()
	{
		Bombe = GameObject.Instantiate( Resources.LoadAssetAtPath("Assets/Prefabs/Bombe_Basic.prefab", typeof(GameObject)) ) as GameObject;
		Bombe.SetActive(false);
		Positionpotentiel = this.gameObject.transform.position;
		Poser = true;

	}
	
	void Placerhaut()
	{

		Bombe.transform.position = new Vector3(Positionpotentiel.x,1f,Positionpotentiel.z+1);
		Bombe.SetActive(true);
	}
	
	void Placerbas()
	{;
		Bombe.transform.position = new Vector3(Positionpotentiel.x,1f,Positionpotentiel.z-1);
		Bombe.SetActive(true);
	}
	
	void Placerdroite()
	{
		Bombe.transform.position = new Vector3(Positionpotentiel.x+1,1f,Positionpotentiel.z);
		Bombe.SetActive(true);
	}
	
	void Placergauche()
	{
		Bombe.transform.position = new Vector3(Positionpotentiel.x-1,1f,Positionpotentiel.z);
		Bombe.SetActive(true);
	}
	
	#endregion
	

	void Capacite()
	{
		
	}

	void verifcompteuraction()
	{
		Compteuraction--;
	}
}
