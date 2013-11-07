using UnityEngine;
using System.Collections;

public class CaseClass : MonoBehaviour {
	/**
	* @brief  les directions possibles de parcours dans le laby
	*/
	public enum Direction {Gauche, Droite, Haut, Bas};
	
	/**
	* @brief la class de gestion des cases
	*/
	public class Case_{
		
		#region variables privées
		private bool isMur;
		private Case_ gauche;
		private Case_ droite;
		private Case_ haut;
		private Case_ bas;
		private int x;
		public int X{
			get{ return x;}
			set{ x=value;}
		}

		private int y;
		public int Y{
			get{ return y;}
			set{ y=value;}
		}

		private int d;
		public int D{
			get{ return d;}
			set{ d=value;}
		}
		#endregion
		
		public bool isEmpty(){
			return !isMur;
		}
		/**
		* @brief fonction qui definit le fait que la case soit un mur
		*/
		public void setMur(){
			isMur=true;
		}
		/**
		* @brief fonction qui definit le fait que la case soit vide
		*/
		public void setEmpty(){
			isMur=false;
		}
		/**
		* @brief constructeur
		* @param mur bool (dit si la case est un mur)
		* @param x int (definit la position en X de la case)
		* @param y int (definit la position en Y de la case)
		*/
		public Case_(bool mur, int x, int y){
			this.isMur=mur;
			this.x=x;
			this.y=y;
		}
		/**
		* @brief definit la case du haut
		* @param h Case_
		*/
		private void setHaut(Case_ h){ haut=h; }
		/**
		* @brief definit la case du bas
		* @param b Case_
		*/
		public void setBas(Case_ b){ bas=b; b.setHaut(this);}
		/**
		* @brief definit la case de gauche
		* @param g Case_
		*/
		private void setGauche(Case_ g){ gauche=g; }
		/**
		* @brief definit la case de droite
		* @param d Case_
		*/
		public void setDroite(Case_ d){ droite=d; d.setGauche(this);}
		/**
		* @brief cette fonction renvoie true si la case est accessible, false sinon (accessible veut dire qu'un chemin permet d'aller devant, pas dessus)
		* @param c la case qui "mene" a (0,0)
		*/
		public bool isOpen(Case_ c){
			if (!isMur) return true;
			if (haut!=null && haut!=c && haut.isEmpty()) return true;
			if (bas!=null && bas!=c && bas.isEmpty()) return true;
			if (gauche!=null && gauche!=c && gauche.isEmpty()) return true;
			if (droite!=null && droite!=c && droite.isEmpty()) return true;
			return false; 
		}
		/**
		* @brief cette fonction renvoie true si la case peut liberer une nouvelle case, false sinon
		* @param c la case qui "mene" a (0,0)
		*/
		public bool cantOpen(){
			if (haut!=null && !haut.isOpen(this)){ return false; }
			if (bas!=null && !bas.isOpen(this)){ return false; }
			if (gauche!=null && !gauche.isOpen(this)){ return false; }
			if (droite!=null && !droite.isOpen(this)){ return false; }
			return true;
		}
		/**
		* @brief renvoie l'affichage de la case
		* @return String
		*/
		public override string ToString(){
			if (isMur)
				return "##";
			return "  ";
		}
		/**
		* @brief renvoie la case proche suivant la direction passee en parametre
		* @param d Direction
		* @return Case_
		*/
		public Case_ getNextCase(Direction d){
			switch(d){
				case Direction.Gauche:
					return gauche;
				case Direction.Droite:
					return droite;
				case Direction.Haut:
					return haut;
				case Direction.Bas:
					return bas;
			}
			return null;
		}
	}
}
