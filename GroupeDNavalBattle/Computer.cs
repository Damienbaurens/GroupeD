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
        public override void shoot(Player opponent, Board boardOpponent)
        {
            List<int> targetable = new List<int> { };
            for (int index = 0; index < boardOpponent.SeaElementList.Count; index++)
            {
                if (boardOpponent.SeaElementList[index].state != State.Plouf && boardOpponent.SeaElementList[index].state != State.Touched && boardOpponent.SeaElementList[index].state != State.Sunk)
                {
                    targetable.Add(index);
                }
            }
            Random random = new Random();
            int targetIndex = targetable[random.Next(targetable.Count)];
            SeaElement target = boardOpponent.SeaElementList[targetIndex];
            if (target.state == State.Boat)
            {
                for (int nboat=0; nboat < opponent.boatList.Length; nboat++)
                {
                    Boat currentBoat = opponent.boatList[nboat];
                    for (int element = 0; element < currentBoat.size; element++)
                    {
                        if ((target.posX == currentBoat.position[element].posX - 1) && (target.posY == currentBoat.position[element].posY - 1))
                        {
                            int pv = currentBoat.Life();
                            if (pv > 1)
                            {
                                target.state = State.Touched;
                                target.clickable = false;
                            }
                            else
                            {
                                for (int element2 = 0; element2 < currentBoat.size; element2++)
                                {
                                    currentBoat.position[element2].state = State.Sunk;
                                }
                                target.state = State.Sunk;
                                target.clickable = false;
                            }

                        }
                    }
                }
                boardOpponent.SeaElementList[targetIndex] = target;
            }
        }

        public override Board place(Board board)
        {
            return null;
            int nboat = 0;
            int[,] listInterdit = new int[10, 10];// liste d'élément inclickable strictement
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    listInterdit[i, j] = 0; // au début tout est clickable
                }
            }

            while (nboat < this.boatList.Length)
            {
                Boat currentBoat = this.boatList[nboat];
                SeaElement target;
                Boolean acceptable = false;
                while (!acceptable)
                {
                    acceptable = true;
                    Random random = new Random();
                    int direction = random.Next(3); // 0 pour gauche, 1 pour haut, 2 pour droite, 3 pour bas
                    int aleaX = currentBoat.size + random.Next(10 - 2 * currentBoat.size) + 1;
                    int aleaY = currentBoat.size + random.Next(10 - 2 * currentBoat.size) + 1;
                    if (direction == 0)
                    {
                        for (int element = 0; element < currentBoat.size; element++)
                        {
                            target = board.SeaElementList[10 * (aleaX - 1 - element) + (aleaY - 1)];
                            if (listInterdit[target.posX, target.posY] == 1)
                            {
                                acceptable = false;
                                break;
                            }
                            else 
                            {
                                currentBoat.position[element] = target;
                            }
                        }
                    }

                    else if (direction == 1)
                    {
                        for (int element = 0; element < currentBoat.size; element++)
                        {
                            target = board.SeaElementList[10 * (aleaX - 1) + (aleaY - 1 + element)];
                            if (listInterdit[target.posX, target.posY] == 1)
                            {
                                acceptable = false;
                                break;
                            }
                            else
                            {
                                currentBoat.position[element] = target;
                            }
                        }
                    }

                    else if (direction == 2)
                    {
                        for (int element = 0; element < currentBoat.size; element++)
                        {
                            target = board.SeaElementList[10 * (aleaX - 1 + element) + (aleaY - 1)];
                            if (listInterdit[target.posX, target.posY] == 1)
                            {
                                acceptable = false;
                                break;
                            }
                            else
                            {
                                currentBoat.position[element] = target;
                            }
                        }
                    }

                    else if (direction == 3)
                    {
                        for (int element = 0; element < currentBoat.size; element++)
                        {
                            target = board.SeaElementList[10 * (aleaX - 1 ) + (aleaY - 1 - element)];
                            if (listInterdit[target.posX, target.posY] == 1)
                            {
                                acceptable = false;
                                break;
                            }
                            else
                            {
                                currentBoat.position[element] = target;
                            }
                        }
                    }

                    if (acceptable)
                    {
                        this.boatList[nboat] = currentBoat;
                        for (int element = 0; element < currentBoat.size; element++)
                        {
                            board.SeaElementList[10 * (currentBoat.position[element].posX - 1 - element) + (currentBoat.position[element].posY - 1)] = currentBoat.position[element];
                            listInterdit[currentBoat.position[element].posX, currentBoat.position[element].posY] = 1;
                        }
                    }
                }
                nboat++;
            }
        }
    }
}