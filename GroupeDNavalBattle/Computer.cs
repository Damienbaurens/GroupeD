using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    class Computer : Player
    {
        public Computer(int id, Boat[] boatList)
        {
            this.id = id;
            this.boatList = boatList;
        }
        public override Board shoot(Player opponent, Board boardOpponent)
        {
            List<int> targetable = new List<int> { }; // liste des éléments sur lesquels on n'a pas encore tiré
            for (int index = 0; index < boardOpponent.SeaElementList.Count; index++)
            {
                if (boardOpponent.SeaElementList[index].state != State.Plouf && boardOpponent.SeaElementList[index].state != State.Touched && boardOpponent.SeaElementList[index].state != State.Sunk)
                {
                    targetable.Add(index);
                }
            }
            Random random = new Random();
            int targetIndex = targetable[random.Next(targetable.Count)]; // on tire au hasard sur une case encore inconnue
            SeaElement target = boardOpponent.SeaElementList[targetIndex];
            if (target.button.Name[1] != '1') return null;

            if (target.state == State.Water) target.state = State.Plouf; // raté !
            else if (target.state == State.Boat)
            {
                for (int nboat=0; nboat < opponent.boatList.Length; nboat++)
                {
                    Boat currentBoat = opponent.boatList[nboat]; // on recherche le bateau à qui appartient la case que l'on a touchée
                    for (int element = 0; element < currentBoat.size; element++)
                    {
                        if ((target.posX == currentBoat.position[element].posX) && (target.posY == currentBoat.position[element].posY))
                        {
                            int pv = currentBoat.Life();
                            if (pv > 1)
                            {
                                target.state = State.Touched; // touché !
                            }
                            else
                            {
                                for (int element2 = 0; element2 < currentBoat.size; element2++)
                                {
                                    currentBoat.position[element2].state = State.Sunk; // si le bateau est coulé, on change tous ses éléments en "Sunk"
                                }
                                target.state = State.Sunk; // coulé !
                            }

                        }
                    }
                }
                boardOpponent.SeaElementList[targetIndex] = target;
            }
            return boardOpponent;
        }

        public override Board place(Board board)
        {
            int nboat = GameManager.getnBoatsToPlace() - 1; ;
            int[,] listInterdit = new int[10, 10];// liste d'élément incliquables
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    listInterdit[i, j] = 0; // au début tout est cliquable
                }
            }

            for (int ligne = 0; ligne < 10; ligne++)
            {
                for (int colonne = 0; colonne < 10; colonne++)
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
                                    listInterdit[ligne + i - 1, colonne + j - 1] = 1;
                                }
                            }
                        }
                    }
                }
            }

            Boat currentBoat = this.boatList[nboat];
            SeaElement target;
            Random random = new Random();
            int direction = random.Next(3); // 0 pour gauche, 1 pour haut, 2 pour droite, 3 pour bas
            int aleaX;
            int aleaY;
            if (direction == 0) 
            {
                aleaX = currentBoat.size + random.Next(10 - currentBoat.size); // on place au hasard la première case du bateau puis on place les autres à gauche
                aleaY = random.Next(10) + 1;
                for (int element = 0; element < currentBoat.size; element++)
                {
                    target = board.SeaElementList[10 * (aleaX - 1 - element) + (aleaY - 1)];
                    if (listInterdit[target.posX-1, target.posY-1] == 1)
                    {
                        return null;
                    }
                    else 
                    {
                        currentBoat.position[element] = target;
                    }
                }
            }

            else if (direction == 1)
            {
                aleaX = random.Next(10) + 1;  // on place au hasard la première case du bateau puis on place les autres en haut
                aleaY = random.Next(10 - currentBoat.size) +1; 
                for (int element = 0; element < currentBoat.size; element++)
                {
                    target = board.SeaElementList[10 * (aleaX - 1) + (aleaY - 1 + element)];
                    if (listInterdit[target.posX-1, target.posY-1] == 1)
                    {
                        return null;
                    }
                    else
                    {
                        currentBoat.position[element] = target;
                    }
                }
            }

            else if (direction == 2)
            {
                aleaX = random.Next(10 - currentBoat.size) + 1;  // on place au hasard la première case du bateau puis on place les autres à droite
                aleaY = random.Next(10) + 1;
                for (int element = 0; element < currentBoat.size; element++)
                {
                    target = board.SeaElementList[10 * (aleaX - 1 + element) + (aleaY - 1)];
                    if (listInterdit[target.posX-1, target.posY-1] == 1)
                    {
                        return null;
                    }
                    else
                    {
                        currentBoat.position[element] = target;
                    }
                }
            }

            else if (direction == 3)
            {
                aleaX = random.Next(10) + 1;  // on place au hasard la première case du bateau puis on place les autres en bas
                aleaY = currentBoat.size + random.Next(10 - currentBoat.size);
                for (int element = 0; element < currentBoat.size; element++)
                {
                    target = board.SeaElementList[10 * (aleaX - 1 ) + (aleaY - 1 - element)];
                    if (listInterdit[target.posX-1, target.posY-1] == 1)
                    {
                        return null;
                    }
                    else
                    {
                        currentBoat.position[element] = target;
                    }
                }
            }
            this.boatList[nboat] = currentBoat;
            for (int element = 0; element < currentBoat.size; element++)
            {
                board.SeaElementList[10 * (currentBoat.position[element].posX - 1) + (currentBoat.position[element].posY - 1)] = currentBoat.position[element];
                board.SeaElementList[10 * (currentBoat.position[element].posX - 1) + (currentBoat.position[element].posY - 1)].state = State.Boat;
            }
            return board;

        }
    }
}