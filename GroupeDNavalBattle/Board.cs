using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    public class Board
    {
        public List<SeaElement> SeaElementList = new List<SeaElement> { };
        public int player;

        public Board(int player)
        {
            for (int x = 1; x < 11; x++)
            {
                for (int y = 1; y < 11; y++)
                {
                    SeaElement createdSeaElement = new SeaElement(x, y, player, true);
                    SeaElementList.Add(createdSeaElement);
                }
            }
            this.player = player;
        }
        public  List<SeaElement> getSeaElementList()
        {
            return SeaElementList;
        }
        public  int getPlayer()
        {
            return player;
        }

    }
}
