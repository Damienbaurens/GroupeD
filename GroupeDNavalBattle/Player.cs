using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    public abstract class Player
    {
        public int id { get; set; }
        public Boat[] boatList { get; set; }
        public abstract void shoot( Player opponent, Board boardOpponent);
        public abstract void place(Board board);
       
    }
}
