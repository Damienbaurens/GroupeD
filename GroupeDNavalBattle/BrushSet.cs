using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace GroupeDNavalBattle
{
    class BrushSet
    {
        /// <summary>
        /// Set de Pinceaux permettant de changer la couleur d'une case rapidement. Pas besoin de l'instancier pour utiliser les pinceaux
        /// </summary>
        public static SolidColorBrush waterBrush = new SolidColorBrush(Color.FromArgb(255, 12, 74, 182));
        public static SolidColorBrush boatBrush = new SolidColorBrush(Color.FromArgb(255, 107, 109, 114));
        public static SolidColorBrush touchedBrush = new SolidColorBrush(Color.FromArgb(255, 241, 132,0));
        public static SolidColorBrush sunkBrush = new SolidColorBrush(Color.FromArgb(255, 180, 7, 7));
        public static SolidColorBrush ploufBrush = new SolidColorBrush(Color.FromArgb(255, 30, 217, 236));
        public static SolidColorBrush unclickableBrush = new SolidColorBrush(Color.FromArgb(255, 110, 138, 187));
        public static SolidColorBrush borderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
    }
}
