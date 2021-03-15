using System;
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

        public override Board place(Board board)
        {
            int nboat = GameManager.getnBoatsToPlace()-1;
            int element = GameManager.getElement();
            int[,] listInterdit= new int[10,10];// liste d'élément inclickable strictement
            for(int ligne=0; ligne<10; ligne++)
            {
                for(int colonne=0; colonne<10; colonne++)
                {
                    if (board.SeaElementList.ElementAt(ligne * 10 + colonne).state == State.Boat)
                    {
                        listInterdit[ligne, colonne] = 1;
                        //ajout des cases de bord des bateaux dans la liste d'éléments interdits
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                if ((0 < ligne + i) && (9 > ligne + i) && (0 < colonne + j) && (9 > colonne + j))
                                {
                                    listInterdit[ligne - 1 + i, colonne - 1 + j] = 1;
                                }

                            }
                        }
                    }
                    else
                    {
                        listInterdit[ligne, colonne] = 0;
                    }
                }
            }
            Boat currentBoat = this.boatList[nboat];
            SeaElement target = ButtonBuffer.getPressedSeaElement();
            if (target == null) return null;



            int x = target.posX - 1;
            int y = target.posY - 1;

            for (int k = 0; k < element; k++)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if ((0 < currentBoat.position[k].posY + i) && (9 > currentBoat.position[k].posY + i) && (0 < currentBoat.position[k].posX + j) && (9 > currentBoat.position[k].posX + j))
                        {
                            listInterdit[currentBoat.position[k].posY - 1 + i, currentBoat.position[k].posX - 1 + j] = 0;
                        }

                    }
                }
            }

            // on compte le nombre de cases valides à droite
            Boolean isValid=true;
            int validRight=0;
            int checkX=target.posX;
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
            checkX=target.posX-2;
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
            int checkY=target.posY-2;
            while( checkY >=0 && isValid)
            {
                if(listInterdit[target.posX-1,checkY]==1)
                {
                    isValid=false;
                }
                else
                {
                    checkY--;
                    validUp++;
                }
            }

            // on compte le nombre de cases valides en bas
            isValid=true;
            int validDown=0;
            checkY=target.posY;
            while( checkY<10 && isValid)
            {
                if(listInterdit[target.posX-1,checkY]==1)
                {
                    isValid=false;
                }
                else
                {
                    checkY++;
                    validDown++;
                }
            }

            // s'il y a assez de place pour placer le bateau, on valide la case cliquée
            if((validDown+validUp+1)<currentBoat.size && (validLeft+validRight+1)<currentBoat.size)
            {
                return null;
            }
            ButtonBuffer.setPressedSeaElement(null);
            currentBoat.position[element] = target;
            board.SeaElementList.ElementAt(x * 10 + y).state = State.Boat;

            if (element == 0)
            {
                //rendre la case inclicable liste
                for (int ligne = 1; ligne < 11; ligne++)
                {
                    for (int colonne = 1; colonne < 11; colonne++)
                    {
                        //rendre tous les élements de non proche en proche non clicable
                        if (ligne == target.posY + 1 && colonne == target.posX)
                        {
                            board.SeaElementList.ElementAt((colonne - 1) * 10 + (ligne - 1)).clickable = true;
                        }
                        else if (ligne == target.posY - 1 && colonne == target.posX)
                        {
                            board.SeaElementList.ElementAt((colonne - 1) * 10 + (ligne - 1)).clickable = true;
                        }
                        else if (ligne == target.posY && colonne == target.posX + 1)
                        {
                            board.SeaElementList.ElementAt((colonne - 1) * 10 + (ligne - 1)).clickable = true;
                        }
                        else if (ligne == target.posY && colonne == target.posX - 1)
                        {
                            board.SeaElementList.ElementAt((colonne - 1) * 10 + (ligne - 1)).clickable = true;
                        }
                        else if (ligne == y + 1 && colonne == x + 1)
                        {
                            board.SeaElementList.ElementAt((colonne - 1) * 10 + (ligne - 1)).clickable = true;
                        }
                        else
                        {
                            board.SeaElementList.ElementAt((colonne - 1) * 10 + (ligne - 1)).clickable = false;
                        }
                    }
                }
            }
            else if (element == currentBoat.size - 1)
            {
                for (int ligne = 1; ligne < 11; ligne++)
                {
                    for (int colonne = 1; colonne < 11; colonne++)
                    {
                        //rendre tous les élements clickables
                        board.SeaElementList.ElementAt((colonne - 1) * 10 + (ligne - 1)).clickable = true;
                    }
                }
            }
            else
            {
                Boolean Horizontal = true;
                int x0 = currentBoat.position[0].posX - 1;
                int y0 = currentBoat.position[0].posY - 1;
                if (x0 == x)
                {
                    Horizontal = false;
                }
                if (Horizontal)
                {
                    //quand on pose le 2nd élement on enlève les case clicable de l'autre sens 
                    if (element == 1)
                    {
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
                        if (listInterdit[x-1, y] != 1)
                        {
                            board.SeaElementList.ElementAt((x-1) * 10 + y).clickable = true;
                        }
                    }
                    if (y < 9)
                    {
                        if (listInterdit[x+1, y] != 1)
                        {
                            board.SeaElementList.ElementAt((x+1) * 10 +y).clickable = true;
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
                            board.SeaElementList.ElementAt((x0 - 1) * 10 + (y0)).clickable = false;
                        }
                        if (x0 < 9)
                        {
                            board.SeaElementList.ElementAt((x0 + 1) * 10 + (y0)).clickable = false;
                        }
                    }
                    // après avoir verifié que les prochaines cases existaient et qu'on pouvait y mettre un élément ont les rends clicable
                    if (x > 0)
                    {
                        if (listInterdit[x, y-1] != 1)
                        {
                            board.SeaElementList.ElementAt(x * 10 + (y-1)).clickable = true;
                        }
                    }
                    if (x < 9)
                    {
                        if (listInterdit[x , y+1] != 1)
                        {
                            board.SeaElementList.ElementAt(x * 10 + (y+1)).clickable = true;
                        }
                    }

                }
                board.SeaElementList.ElementAt(x * 10 + y).state = State.Boat;
            }
            this.boatList[nboat] = currentBoat;
            return board;
        }
    }
}
