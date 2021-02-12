using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GroupeDNavalBattle
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            for(int p =1; p<3; p++)
            {
                for(int x = 1; x < 11; x++)
                {
                    for(int y = 1; y < 11; y++)
                    {
                        SeaElement createSeaElement = new SeaElement(x, y, p, true);
                        NavalBattleD.Children.Add(createSeaElement.button);
                    }
                }
            }
        }

        private void A_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
