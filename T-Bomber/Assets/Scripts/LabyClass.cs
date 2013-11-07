using UnityEngine;
using System.Collections;

public class LabyClass : MonoBehaviour {
	
	private CaseClass caseClass;
	
	public CaseClass CaseClass {
		get {
			return this.caseClass;
		}
		set {
			caseClass = value;
		}
	}
	
	/**
	* la class de gestion du labyrinthe
	*/
	public class Laby{
		
		#region Variables privées
		private CaseClass.Case_ [][] cases;
		private int dimX;
		private int dimY;
		#endregion
		
		/**
		* @brief le constructeur
		* @param dimX int
		* @param dimY int
		*/
		public Laby(int dimX, int dimY){
			int i, j;
			this.dimX=dimX;this.dimY=dimY;
			this.cases=new CaseClass.Case_[dimX][];
			for (i=0;i<dimX;i++){
				this.cases[i]=new CaseClass.Case_[dimY];
				for (j=0;j<dimY;j++){
					this.cases[i][j]=new CaseClass.Case_(true, i, j);
				}
			}
			for (i=0;i<dimX-1;i++){
				for (j=0;j<dimY;j++){
					this.cases[i][j].setDroite(this.cases[i+1][j]);
				}
			}
			for (i=0;i<dimX;i++){
				for (j=0;j<dimY-1;j++){
					this.cases[i][j].setBas(this.cases[i][j+1]);
				}
			}
		}
		/**
		* @brief renvoie une direction au hazard
		* @return Direction
		*/
		private CaseClass.Direction getRandomDirection(){;
			int d=Random.Range(0,4);
			switch(d){
				case 0: return CaseClass.Direction.Gauche;
				case 1: return CaseClass.Direction.Droite;
				case 2: return CaseClass.Direction.Haut;
				case 3: return CaseClass.Direction.Bas;
			}
			return CaseClass.Direction.Gauche;
		}
		//! genere le labyrinthe de facon aleatoire
		public void generer(){
			generer(cases[0][0]);
		}
		/**
		* @brief genere le labyrinthe de facon aleatoire
		* @param c1 Case_ la case qui va etre liberee
		*/
		public void generer(CaseClass.Case_ c1){
			generer(c1, 0);
		}
		/**
		* @brief genere le labyrinthe de facon aleatoire
		* @param c1 Case_ la case qui va etre liberee
		* @param dist la distance entre c1 et la case de depart
		*/
		public void generer(CaseClass.Case_ c1, int dist){
			c1.setEmpty();
			c1.D=dist; dist++;
			while (!c1.cantOpen()){
				CaseClass.Case_ c2;
				CaseClass.Direction d;
				do{
					d=getRandomDirection();
					c2=c1.getNextCase(d);
				}while(c2==null || c2.isOpen(c1));
				generer(c2, dist);
			}
		}
		
		public string getValue(int i, int j)
		{
			return this.cases[i][j].ToString();
		}
	}
	// Use this for initialization
	void Start () {
		caseClass = GetComponent<CaseClass>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
