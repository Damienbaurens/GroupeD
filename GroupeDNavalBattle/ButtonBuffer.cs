using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    static class ButtonBuffer
    {
        private static SeaElement pressedSeaElement;

        public static SeaElement getPressedSeaElement()
        {
            return pressedSeaElement;
        }

        public static void setPressedSeaElement(SeaElement value)
        {
            pressedSeaElement = value;
        }
    }
}
