using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    internal class HumanPlayer : Player
    {
        public override void shoot(Player opponent)
        { 
            //SeaElement.setState();
        }
        public override void place(Board board)
        {
            int nboat = 0;
            int[10][10] listInterdit;// liste d'élément inclickable strictement
            while (nboat < this.boatList.Length)
            {
                Boat currentBoat = this.boatList[nboat];
                Boolean Horizontal = true;
                for (int element = 0; element < currentBoat.size; element++)
                {
                    SeaElement target = getPressedSeaElement();
                    while (target == null)
                    {
                        target = getPressedSeaElement();
                    }
                    setPressedSeaElement(null);
                    currentBoat.position[element] = target;
                    if (element == 1)
                    {
                        if (currentBoat.position[0].posX== target.posX)
                        {
                            Horizontal = false;
                        }
                    }
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
                                }
                                listInterdit[target.posX][target.posY] = 1;
                                //rendre la case inclicable liste
                            }
                        }
                    }
                    else
                    {
                        int x0 = currentBoat.position[0].posX - 1;
                        int y0 = currentBoat.position[0].posY - 1;
                        int x = currentBoat.position[element].posX - 1;
                        int y = currentBoat.position[element].posY - 1;
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
                            // après avoir verifier que les prochaines cases existaient et qu'on pouvait y mettre un élément ont les rends clicable
                            if (y > 0)
                            {
                                if(listInterdit[target.posX][target.posY-1] != 1)
                                {
                                    board.SeaElementList.ElementAt((x) * 10 + (y - 1)).clickable = true;
                                }
                            }
                            if (y < 9)
                            {
                                if (listInterdit[target.posX][target.posY + 1] != 1)
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
                            // après avoir verifier que les prochaines cases existaient et qu'on pouvait y mettre un élément ont les rends clicable
                            if (x > 0)
                            {
                                if (listInterdit[target.posX-1][target.posY] != 1)
                                {
                                    board.SeaElementList.ElementAt((x-1) * 10 + (y)).clickable = true;
                                }
                            }
                            if (x < 9)
                            {
                                if (listInterdit[target.posX+1][target.posY] != 1)
                                {
                                    board.SeaElementList.ElementAt((x+1) * 10 + (y)).clickable = true;
                                }
                            }

                        }
                        //rend l'élément interdit et inclickable
                        listInterdit[x][y] = 1;
                        board.SeaElementList.ElementAt((x) * 10 + (y)).clickable = false;
                    }
                }
                //ajout des cases de bord des bateaux dans la liste d'élément interdit
                for (int element=0; element< currentBoat.size; element++)
                {
                    int x = currentBoat.position[element].posX - 1;
                    int y = currentBoat.position[element].posY - 1;
                    for (int i=0; i<3; i++)
                    {
                        for (int j=0; j<3; j++)
                        {
                            if((0 < x + i < 9) && (0 < y + i < 9))
                            {
                                listInterdit[x - 1 + i][y - 1 + j] = 1;
                            }
                            
                        }
                    }
                }
                // rendre tous les éléments non totalement interdit clickable pour le prochain bateau
                for(int c=0; c<10; c++)
                {
                    for(int l = 0; l < 10; l++)
                    {
                        if (listInterdit[c][l] != 1)
                        {
                            board.SeaElementList.ElementAt((c - 1) * 10 + (l - 1)).clickable = true;
                        }
                    }
                }
                    

            }
        }
    }
}
