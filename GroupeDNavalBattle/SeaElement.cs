using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GroupeDNavalBattle
{
    class SeaElement
    {
        //Attributs
        public Button button { get; set; } //Le { get; set; } permet d'avoir directement le getter et le setter par défaut

        // TODO : Class enum State state { get; set; } 
        int posX { get; }
        int posY { get;}
        int player { get; }
        Boolean known { get; set; }
        String name;

        //Constructeur
        public SeaElement(int posX, int posY, int player, Boolean known)
        {
            this.posX = posX; //position en abscisse (valeurs de 1 à 10, le placement du bouton se fera en multipliant)
            this.posY = posY; //position en ordonnée
            this.player = player; //numéro du joueur
            this.known = known;
            this.name = "P" + player.ToString() + posX.ToString() + posY.ToString();

            this.button = new Button();
            this.button.Name = this.name + "Button";
            this.button.Content = ""; //bouton vide

            // largeur et hauteur du bouton, on les fait carrés pour si on veut mettre des images dessus un jour
            this.button.Width = 85;
            this.button.Height = 85;

            this.button.Background = BrushSet.waterBrush; //couleur de fond du bouton, par défaut c'est de l'eau
            this.button.BorderThickness = new Thickness(1, 1, 1, 1); //épaisseur en pixel de la bordure du bouton
            this.button.BorderBrush = BrushSet.borderBrush; //couleur de la bordure

            // point d'ancrage du bouton : pour le placer, on compte à partir du coin en haut à gauche
            this.button.HorizontalAlignment = HorizontalAlignment.Left;
            this.button.VerticalAlignment = VerticalAlignment.Top;

            //position exacte sur le board
            this.button.Margin = new Thickness(120 + (player - 1) * 950 + (posX - 1) * 85, 230 + (posY - 1) * 85, 0, 0);

            //event quand on presse le bouton
            this.button.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO : faire l'event de pressage du bouton
            throw new NotImplementedException();
        }
    }
}
