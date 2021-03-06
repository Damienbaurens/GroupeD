using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    class Computer : Player
    {
        public override void shoot(Player opponent, Board boardOpponent)
        {
            throw new NotImplementedException();
        }

        public override void place(Board board)
        {
            int nboat = 0;
            int[,] listInterdit = new int[10, 10];// liste d'élément inclickable strictement
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    listInterdit[i, j] = 0;
                }
            }
            while (nboat < this.boatList.Length)
            {
                Boat currentBoat = this.boatList[nboat];
                Boolean Horizontal = true;
                SeaElement target;
                int[][] utilisable;
                for (int element = 0; element < currentBoat.size; element++)
                {
                    Boolean acceptable = false;
                    if (element == 0)
                    {
                        while (acceptable == false)
                        {
                            Random random = new Random();
                            int aleaX = random.Next(10) + 1;
                            int aleaY = random.Next(10) + 1;
                            if (listInterdit[aleaY - 1, aleaX - 1] == 0)
                            {
                                acceptable = true;
                                target = board.getSeaElementList()[10 * (aleaX - 1) + (aleaY - 1)];
                                if (aleaX > 1)
                                {
                                    if (aleaY > 1)
                                    {
                                        //utilisable;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {

                    }


                }
                throw new NotImplementedException();
            }
        }
    }
}

