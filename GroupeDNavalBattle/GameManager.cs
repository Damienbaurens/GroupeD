using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    static class GameManager
    {
        private static String gameState; // l'état de la partie : placement des bateaux (place), tir (shoot) ou terminée (finished)
        private static HumanPlayer J1; // le joueur 1
        private static Computer J2; // le joueur 2
        private static Player winner; // le gagnant
        private static Board boardJ1; // la grille du joueur 1
        private static Board boardJ2; // la grille du joueur 1
        private static int nBoatsToPlace; // le nombre de bateaux restant à placer (pour le début de partie)
        private static int element; // l'indice de la case du bateau que l'on est en train de placer (pour un bateau de taille 5 il ira de 0 à 4, etc)
        public static String getGameState()
        {
            return gameState;
        }
        public static void setGameState(String value)
        {
            gameState = value;
        }
        public static HumanPlayer getJ1()
        {
            return J1;
        }
        public static void setJ1(HumanPlayer value)
        {
            J1 = value;
        }
        public static Computer getJ2()
        {
            return J2;
        }
        public static void setJ2(Computer value)
        {
            J2 = value;
        }
        public static Board getBoardJ1()
        {
            return boardJ1;
        }
        public static void setBoardJ1(Board value) 
        {
            boardJ1 = value;
        }
        public static Board getBoardJ2()
        {
            return boardJ2;
        }
        public static void setBoardJ2(Board value)
        {
            boardJ2 = value;
        }
        public static int getnBoatsToPlace()
        {
            return nBoatsToPlace;
        }
        public static void setnBoatsToPlace(int value)
        {
            nBoatsToPlace = value;
        }
        public static int getElement()
        {
            return element;
        }
        public static void setElement(int value)
        {
            element = value;
        }
        public static Boolean checkFinshed()
        {
            Boolean p1won = true; // vrai si le joueur 1 a gagné
            Boolean p2won = true; // vrai si le joueur 2 a gagné
            for (int i = 0; i < J1.boatList.Length; i++)
            {
                if (J1.boatList[i].Life() > 0) // on vérifie s'il reste des bateaux non coulés
                {
                    p1won = false; 
                }
            }
            for (int i = 0; i < J2.boatList.Length; i++)
            {
                if (J2.boatList[i].Life() > 0)
                {
                    p2won = false;
                }
            }
            if (p1won || p2won) { return true; }
            return false;
        }
        public static void OnClick(SeaElement clicked)
        {
            if (gameState == "place")
            {
                if (J1.place(boardJ1) != null)
                { 
                    element++;
                    if (element == J1.boatList[nBoatsToPlace-1].size) // si on a fini de placer le bateau
                    {
                        element = 0;
                        for (int e = 0; e < J1.boatList[nBoatsToPlace - 1].size; e++)
                        {
                            J1.boatList[nBoatsToPlace - 1].position[e].state = State.Boat;
                        }
                        while (J2.place(boardJ2) == null) { }
                        nBoatsToPlace--;
                    }
                }
                if(nBoatsToPlace == 0) gameState = "shoot"; // si on a placé tous les bateaux, que la partie commence !
            }
            else if (gameState == "shoot")
            {
                if (J1.shoot(J2, boardJ2) != null)
                {
                    if (checkFinshed()) // on regarde si le joueur 1 a fini la partie
                    {
                        gameState = "finished";
                        winner = J1;
                        
                    }
                    while (J2.shoot(J1, boardJ1) == null) { }
                    if (checkFinshed()) // on regarde si le joueur 2 a fini la partie
                    {
                        gameState = "finished";
                        winner = J2;
                    }
                }
            }
        }
    }
}
