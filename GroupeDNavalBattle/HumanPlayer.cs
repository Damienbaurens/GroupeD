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
        public override Board shoot(Player opponent, Board boardOpponent)
        {
            List<SeaElement> elementPlayer = boardOpponent.SeaElementList;

            SeaElement shootable = ButtonBuffer.getPressedSeaElement();
            if (shootable == null) return null;
            if (shootable.button.Name[1] != '2') return null;

            if (opponent.id == 2)
            {
                ButtonBuffer.setPressedSeaElement(null);
                shootable.known = true;
                if (shootable.state == State.Water)
                {
                    shootable.state = State.Plouf;
                }
                if (shootable.state == State.Boat)
                {
                    for (int nboat = 0; nboat < opponent.boatList.Length; nboat++)
                    {
                        {
                            Boat currentBoat = opponent.boatList[nboat];
                            for (int element = 0; element < currentBoat.size; element++)
                            {
                                if ((shootable.posX == currentBoat.position[element].posX) && (shootable.posY == currentBoat.position[element].posY))
                                {
                                    int pv = currentBoat.Life();
                                    if (pv > 1)
                                    {
                                        shootable.state = State.Touched;
                                    }
                                    else
                                    {
                                        for (int element2 = 0; element2 < currentBoat.size; element2++)
                                        {
                                            currentBoat.position[element2].state = State.Sunk;
                                        }
                                        shootable.state = State.Sunk;
                                    }

                                }
                            }
                            opponent.boatList[nboat] = currentBoat;
                        }
                    }
                }
            }
            return boardOpponent;
        }

        public override Board place(Board board)
        {
            int nboat = GameManager.getnBoatsToPlace()-1;
            int element = GameManager.getElement();

            Boat currentBoat = this.boatList[nboat];
            SeaElement target = ButtonBuffer.getPressedSeaElement();
            if (target == null) return null;
            if (target.button.Name[1] != '1') return null;
            if (target.state != State.Water) return null;

            int[,] listInterdit= new int[10,10];// liste d'élément inclickable strictement
            for (int ligne = 0; ligne < 10; ligne++)
            {
                for (int colonne = 0; colonne < 10; colonne++)
                {
                    listInterdit[ligne, colonne] = 0;
                }
            }
            for (int ligne=0; ligne<10; ligne++)
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
                                if ((0 < ligne + i) && (11 > ligne + i) && (0 < colonne + j) && (11 > colonne + j))
                                {
                                    listInterdit[ligne+ i-1, colonne + j-1] = 1;
                                }
                            }
                        }
                    }
                }
            }

            int x = target.posX - 1;
            int y = target.posY - 1;

            if (listInterdit[x, y] == 1) return null;

            // on compte le nombre de cases valides à droite
            Boolean isValid=true;
            int validRight=0;
            int checkX=x+1;
            while( checkX <10 && isValid)
            {
                if(listInterdit[checkX,y]==1)
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
            checkX=x-1;
            while( checkX >=0 && isValid)
            {
                if(listInterdit[checkX,y]==1)
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
            int checkY=y-1;
            while( checkY >=0 && isValid)
            {
                if(listInterdit[x,checkY]==1)
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
            checkY=y+1;
            while( checkY<10 && isValid)
            {
                if(listInterdit[x,checkY]==1)
                {
                    isValid=false;
                }
                else
                {
                    checkY++;
                    validDown++;
                }
            }

            Boolean validHorizontal = (validLeft + validRight + 1) >= currentBoat.size;
            Boolean validVertical = (validUp + validDown + 1) >= currentBoat.size;

            // s'il y a assez de place pour placer le bateau, on valide la case cliquée
            if (!validHorizontal && !validVertical)
            {
                return null;
            }
            ButtonBuffer.setPressedSeaElement(null);
            currentBoat.position[element] = target;
            board.SeaElementList.ElementAt(x * 10 + y).state = State.Sunk;

            if (element == 0)
            {
                //rendre la case inclicable liste
                for (int ligne = 0; ligne < 10; ligne++)
                {
                    for (int colonne = 0; colonne < 10; colonne++)
                    {
                        //rendre tous les élements de non proche en proche non clicable
                        if (listInterdit[colonne,ligne]==1)
                        {
                            board.SeaElementList.ElementAt(colonne * 10 + ligne).clickable = false;
                        }
                        else if (ligne == y + 1 && colonne == x && validVertical)
                        {
                            board.SeaElementList.ElementAt(colonne*10 + ligne).clickable = true;
                        }
                        else if (ligne == y - 1 && colonne == x && validVertical)
                        {
                            board.SeaElementList.ElementAt(colonne*10 + ligne).clickable = true;
                        }
                        else if (ligne == y && colonne == x + 1 && validHorizontal)
                        {
                            board.SeaElementList.ElementAt(colonne* 10 +ligne).clickable = true;
                        }
                        else if (ligne == y && colonne == x - 1 && validHorizontal)
                        {
                            board.SeaElementList.ElementAt(colonne* 10 +ligne).clickable = true;
                        }
                        else if (ligne == y && colonne == x)
                        {
                            board.SeaElementList.ElementAt(colonne * 10 + ligne).clickable = true;
                        }
                        else
                        {
                            board.SeaElementList.ElementAt(colonne * 10 + ligne).clickable = false;
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
                    if (x > 0)
                    {
                        if (listInterdit[x-1, y] != 1)
                        {
                            board.SeaElementList.ElementAt((x-1) * 10 + y).clickable = true;
                        }
                    }
                    if (x < 9)
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
                    if (y > 0)
                    {
                        if (listInterdit[x, y-1] != 1)
                        {
                            board.SeaElementList.ElementAt(x * 10 + (y-1)).clickable = true;
                        }
                    }
                    if (y < 9)
                    {
                        if (listInterdit[x , y+1] != 1)
                        {
                            board.SeaElementList.ElementAt(x * 10 + (y+1)).clickable = true;
                        }
                    }

                }
            }
            this.boatList[nboat] = currentBoat;
            return board;
        }
    }
}
