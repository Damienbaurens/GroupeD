﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    static class GameManager
    {
        private static String gameState;
        private static HumanPlayer J1;
        private static Computer J2;
        private static Player winner;
        private static Board boardJ1;
        private static Board boardJ2;
        private static int nBoatsToPlace;
        private static int element;
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
            for (int i = 0; i < J1.boatList.Length; i++)
            {
                if (J1.boatList[i].Life() > 0 && J2.boatList[i].Life() > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public static void OnClick(SeaElement clicked)
        {
            if (gameState == "place")
            {
                if (J1.place(boardJ1) != null)
                { 
                    element++;
                    if (element == J1.boatList[nBoatsToPlace-1].size)
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
                if(nBoatsToPlace == 0) gameState = "shoot";
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
