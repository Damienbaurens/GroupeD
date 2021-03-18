using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    public abstract class Player
    {
        //Attributs
        public int id { get; set; } // identifiant ( 1 pour humain et 2 pour ordinateur )
        public Boat[] boatList { get; set; } // liste des bateaux du joueur
        //Méthodes
        public abstract Board shoot( Player opponent, Board boardOpponent); // méthode permettant de tirer un un bateau ennemi
        public abstract Board place(Board board); // méthode permettant de placer ses bateaux en mer
       
    }
}
