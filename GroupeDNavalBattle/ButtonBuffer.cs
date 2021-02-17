using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    static class ButtonBuffer
    {
        private static String pressedButton;

        public static String getPressedButton()
        {
            return pressedButton;
        }

        public static void setPressedButton(String value)
        {
            pressedButton = value;
        }
    }
}
