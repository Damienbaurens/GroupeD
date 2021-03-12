﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    internal class HumanPlayer : Player
    {
        public HumanPlayer(int id, Boat[] boatList) 
        {
            this.id = id;
            this.boatList = boatList;
        }
        public override void shoot(Player opponent, Board boardOpponent)
        {
            List<SeaElement> elementPlayer = boardOpponent.SeaElementList;
            for (int index = 0; index < elementPlayer.Count; index++)
            {
                SeaElement test = elementPlayer[index];
                test.clickable = false;
            }

            SeaElement shootable = ButtonBuffer.getPressedSeaElement();
            while (shootable != null)
            {
                shootable = ButtonBuffer.getPressedSeaElement();
            }

            if (opponent.id == 2 && shootable.known == false)
            {
                ButtonBuffer.setPressedSeaElement(null);
                shootable.known = true;
                if (shootable.state == State.Water)
                {
                    shootable.state = State.Plouf;
                    shootable.clickable = false;
                }
                if (shootable.state == State.Boat)
                {
                    for (int nboat = 0; nboat < opponent.boatList.Length; nboat++)
                    {
                        {
                            Boat currentBoat = opponent.boatList[nboat];
                            for (int element = 0; element < currentBoat.size; element++)
                            {
                                if ((shootable.posX == currentBoat.position[element].posX - 1) && (shootable.posY == currentBoat.position[element].posY - 1))
                                {
                                    int pv = currentBoat.Life();
                                    if (pv > 1)
                                    {
                                        shootable.state = State.Touched;
                                        shootable.clickable = false;
                                    }
                                    else
                                    {
                                        for (int element2 = 0; element2 < currentBoat.size; element2++)
                                        {
                                            currentBoat.position[element2].state = State.Sunk;
                                        }
                                        shootable.state = State.Sunk;
                                        shootable.clickable = false;
                                    }

                                }
                            }
                            opponent.boatList[nboat] = currentBoat;
                        }


                    }
                }
            }
        }

        public override void place(Board board)
        {
            int nboat = 0;
            int[,] listInterdit= new int[10,10];// liste d'élément inclickable strictement
            for(int i=0; i<10; i++)
            {
                for(int j=0; j<10; j++)
                {
                    listInterdit[i, j] = 0;
                }
            }
            while (nboat < this.boatList.Length)
            {
                Boat currentBoat = this.boatList[nboat];
                Boolean Horizontal = true;
                for (int element = 0; element < currentBoat.size; element++)
                {
                    SeaElement target = ButtonBuffer.getPressedSeaElement();
                    Boolean validTarget = false;
                    while (!validTarget)
                    {
                        target = ButtonBuffer.getPressedSeaElement();
                        if(target != null)
                        {
                            // on compte le nombre de cases valides à droite
                            Boolean isValid=true;
                            int validRight=0;
                            int checkX=target.posX+1;
                            while( checkX <10 && isValid)
                            {
                                if(listInterdit[checkX,target.posY-1]==1)
                                {
                                    isValid=false;
                                }
                                else
                                {
                                    checkX++;
                                    validRight++;
                                }
                            }

                            // on compte le nombre de cases valides à gauche
                            isValid=true;
                            int validLeft=0;
                            checkX=target.posX-1;
                            while( checkX >=0 && isValid)
                            {
                                if(listInterdit[checkX,target.posY-1]==1)
                                {
                                    isValid=false;
                                }
                                else
                                {
                                    checkX--;
                                    validLeft++;
                                }
                            }

                            // on compte le nombre de cases valides en haut
                            isValid=true;
                            int validUp=0;
                            int checkY=target.posY+1;
                            while( checkY <10 && isValid)
                            {
                                if(listInterdit[target.posX-1,checkY]==1)
                                {
                                    isValid=false;
                                }
                                else
                                {
                                    checkY++;
                                    validUp++;
                                }
                            }

                            // on compte le nombre de cases valides en bas
                            isValid=true;
                            int validDown=0;
                            checkY=target.posY-1;
                            while( checkY>=0 && isValid)
                            {
                                if(listInterdit[target.posX-1,checkY]==1)
                                {
                                    isValid=false;
                                }
                                else
                                {
                                    checkY--;
                                    validDown++;
                                }
                            }

                            // s'il y a assez de place pour placer le bateau, on valide la case cliquée
                            if((validDown+validUp+1)<=currentBoat.size || (validLeft+validRight+1)<=currentBoat.size)
                            {
                                validTarget=true;
                            }

                        }
                    }
                    ButtonBuffer.setPressedSeaElement(null);
                    currentBoat.position[element] = target;
                   
                    if (element == 0)
                    {
                        for (int ligne = 1; ligne < 11; ligne++)
                        {
                            for (int colonne = 1 ; colonne < 11; colonne++)
                            {
                                if ((ligne== target.posY+1 && colonne==target.posX)|| (ligne == target.posY-1 && colonne == target.posX)||(ligne == target.posY && colonne == target.posX+1)|| (ligne == target.posY && colonne == target.posX - 1))
                                {
                                    board.SeaElementList.ElementAt((colonne - 1) * 10 + (ligne - 1)).clickable= false;
                                    //rendre tous les élements de non proche en proche non clicable
                                    board.SeaElementList.ElementAt((colonne - 1) * 10 + (ligne - 1)).state = State.Boat;
                                    listInterdit[target.posX-1, target.posY-1] = 1;
                                    //rendre la case inclicable liste
                                }

                            }
                        }
                    }
                    else
                    {
                        int x0 = currentBoat.position[0].posX - 1;
                        int y0 = currentBoat.position[0].posY - 1;
                        int x = currentBoat.position[element].posX - 1;
                        int y = currentBoat.position[element].posY - 1;
                        if (element == 1)
                        {
                            if (currentBoat.position[0].posX == target.posX)
                            {
                                Horizontal = false;
                            }
                        }
                        if (Horizontal)
                        {
                            //quand on pose le 2nd élement on enlève les case clicable de l'autre sens 
                            if (element == 1){
                                if (y0 > 0)
                                {
                                    board.SeaElementList.ElementAt((x0) * 10 + (y0 - 1)).clickable = false;
                                }
                                if (y0 < 9)
                                {
                                    board.SeaElementList.ElementAt((x0) * 10 + (y0 + 1)).clickable = false;
                                }
                            }
                            // après avoir verifié que les prochaines cases existaient et qu'on pouvait y mettre un élément ont les rends clicable
                            if (y > 0)
                            {
                                if(listInterdit[x, y-1] != 1)
                                {
                                    board.SeaElementList.ElementAt((x) * 10 + (y - 1)).clickable = true;
                                }
                            }
                            if (y < 9)
                            {
                                if (listInterdit[x, y + 1] != 1)
                                {
                                    board.SeaElementList.ElementAt((x) * 10 + (y + 1)).clickable = true;
                                }
                            }
                            
                        }
                        else
                        {
                            //quand on pose le 2nd élement on enlève les case clicable de l'autre sens 
                            if (element == 1)
                            {
                                if (x0 > 0)
                                {
                                    board.SeaElementList.ElementAt((x0-1) * 10 + (y0)).clickable = false;
                                }
                                if (x0 < 9)
                                {
                                    board.SeaElementList.ElementAt((x0+1) * 10 + (y0)).clickable = false;
                                }
                            }
                            // après avoir verifié que les prochaines cases existaient et qu'on pouvait y mettre un élément ont les rends clicable
                            if (x > 0)
                            {
                                if (listInterdit[x-1, y] != 1)
                                {
                                    board.SeaElementList.ElementAt((x-1) * 10 + (y)).clickable = true;
                                }
                            }
                            if (x < 9)
                            {
                                if (listInterdit[x+1, y] != 1)
                                {
                                    board.SeaElementList.ElementAt((x+1) * 10 + (y)).clickable = true;
                                }
                            }

                        }
                        //rend l'élément interdit et inclickable
                        listInterdit[x, y] = 1;
                        board.SeaElementList.ElementAt((x) * 10 + (y)).clickable = false;
                        board.SeaElementList.ElementAt((x) * 10 + (y)).state = State.Boat;
                    }
                }
                //ajout des cases de bord des bateaux dans la liste d'éléments interdits
                for (int element=0; element< currentBoat.size; element++)
                {
                    int x = currentBoat.position[element].posX - 1;
                    int y = currentBoat.position[element].posY - 1;
                    for (int i=0; i<3; i++)
                    {
                        for (int j=0; j<3; j++)
                        {
                            if((0 < x + i) && (9>x+i) && (0 < y + i) && (9 > y + i))
                            {
                                listInterdit[x - 1 + i, y - 1 + j] = 1;
                            }
                            
                        }
                    }
                }
                // rendre tous les éléments non totalement interdit clickable pour le prochain bateau
                for(int c=0; c<10; c++)
                {
                    for(int l = 0; l < 10; l++)
                    {
                        if (listInterdit[c, l] != 1)
                        {
                            board.SeaElementList.ElementAt((c - 1) * 10 + (l - 1)).clickable = true;
                        }
                    }
                }
                this.boatList[nboat] = currentBoat;
                nboat++;
            }
        }
    }
}
