using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupeDNavalBattle
{
    class GameManager
    {
        // Attributs
        private Boat[] InGameBoats;
        private float timer { get; set; }

        // Constructeur
        public GameManager(Boat[] InGameBoats, int timer)
        {
            this.InGameBoats = InGameBoats;
            this.timer = timer;
        }

        //Méthodes
        public Player Play(Player player1, Player player2, Board board)
        {
            player1.place(board);
            player2.place(board);

            bool finished = false;
            Player curr_player = player1;
            Player opponent = player2;

            while (!finished)
            {
                curr_player.shoot(opponent);
                if (opponent.boatList.Length == 0) { finished = true; }
                else
                {
                    Player tmpPlayer = curr_player;
                    curr_player = opponent;
                    opponent = tmpPlayer;
                }
            }
            return curr_player;
        }
    }
}
