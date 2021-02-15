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
            //case.
            //SeaElement.setState()
        }
        public override void place()
        {
            int i = 0;
            while (i< this.boatList.Length)
            {
                Boat currentBoat = this.boatList[i];
            } 
        }
    }
}
