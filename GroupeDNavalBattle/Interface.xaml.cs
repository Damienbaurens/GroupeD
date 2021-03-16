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
            Board p1Board = new Board(1);
            Board p2Board = new Board(2);
            GameManager.setBoardJ1(p1Board);
            for (int ligne = 0; ligne < 10; ligne++)
            {
                for (int colonne = 0; colonne < 10; colonne++)
                {
                    p2Board.SeaElementList.ElementAt(ligne * 10 + colonne).known = false;
                }
            }
            GameManager.setBoardJ2(p2Board);

            foreach (SeaElement seaelement in p1Board.SeaElementList)
            {
                NavalBattleD.Children.Add(seaelement.button);
            }
            foreach (SeaElement seaelement in p2Board.SeaElementList)
            {
                NavalBattleD.Children.Add(seaelement.button);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void A_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _4_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
