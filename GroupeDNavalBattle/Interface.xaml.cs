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
            Board[] args = (Board[])e.Parameter;
            Board p1Board = args[0];
            Board p2Board = args[1];
            foreach (SeaElement seaelement in p1Board.SeaElementList)
            {
                NavalBattleD.Children.Add(seaelement.button);
            }
            foreach (SeaElement seaelement in p2Board.SeaElementList)
            {
                NavalBattleD.Children.Add(seaelement.button);
            }
            Boat[] p1Boats = new Boat[6];
            p1Boats[0] = new Boat(2);
            p1Boats[1] = new Boat(3);
            p1Boats[2] = new Boat(3);
            p1Boats[3] = new Boat(4);
            p1Boats[4] = new Boat(4);
            p1Boats[5] = new Boat(5);

            Boat[] p2Boats = new Boat[6];
            p2Boats[0] = new Boat(2);
            p2Boats[1] = new Boat(3);
            p2Boats[2] = new Boat(3);
            p2Boats[3] = new Boat(4);
            p2Boats[4] = new Boat(4);
            p2Boats[5] = new Boat(5);

            GameManager GM = new GameManager(10);
            HumanPlayer p1 = new HumanPlayer(1, p1Boats);
            Computer p2 = new Computer(2, p2Boats);
            GM.Play(p1,p2,p1Board,p2Board);
            base.OnNavigatedTo(e);
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
