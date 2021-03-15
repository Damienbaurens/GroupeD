using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    public class Boat
    {
        public int size;
        public SeaElement [] position;
        public Boat(int size)
        {
            this.size = size;
            this.position = new SeaElement[size];
        }
        public int Life()
        {
            int touch = 0;
            for (int element=0; element<position.Length; element++)
            {
                
                if (position[element].state == State.Touched)
                {
                    touch += 1;
                }
                
            }
            return size - touch;
        }
    }
}
