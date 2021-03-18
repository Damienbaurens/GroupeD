using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    public class Boat
    {
        // Attributs
        public int size; // la taille du bateau
        public SeaElement [] position; // tableau contenant les cases où se trouve chaque élément du bateau
        // Constructeur
        public Boat(int size)
        {
            this.size = size;
            this.position = new SeaElement[size];
        }
        // Méthodes
        public int Life() // Méthode renvoyant les points de vie restants du bateau
        {
            int touch = 0;
            for (int element=0; element<position.Length; element++)
            {
                if (position[element].state == State.Sunk) { return 0; }
                if (position[element].state == State.Touched)
                {
                    touch += 1;
                };
                
            }
            return size - touch;
        }
    }
}
