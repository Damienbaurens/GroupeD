using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    public enum State // tous les états possibles pour une case
    {
        Water,
        Boat,
        Touched,
        Sunk,
        Unknown,
        Plouf,
    }
}
