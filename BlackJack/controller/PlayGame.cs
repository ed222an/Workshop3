using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace BlackJack.controller
{
    class PlayGame : IObserver<model.Card>
    {
        private model.Game a_game;
        private view.IView a_view;

        public PlayGame(model.Game game, view.IView view)
        {
            a_game = game;
            a_view = view;
        }

        public bool Play()
        {
            a_game.GetPlayer().Subscribe(this);
            a_game.GetDealer().Subscribe(this);

            RenderOutput();

            if (a_game.IsGameOver())
            {
                a_view.DisplayGameOver(a_game.IsDealerWinner());
            }

            int input = a_view.GetInput();

            if (a_view.GetActionNewGame(input) == true)
            {
                a_game.NewGame();
            }
            else if (a_view.GetActionHit(input) == true)
            {
                a_game.Hit();
            }
            else if (a_view.GetActionStand(input) == true)
            {
                a_game.Stand();
            }

            return a_view.GetActionQuit(input) == false;
        }

        // Observer pattern.
        public void OnNext(model.Card card)
        {
            Thread.Sleep(500);
            RenderOutput();
        }

        public void RenderOutput()
        {
            a_view.DisplayWelcomeMessage();

            a_view.DisplayDealerHand(a_game.GetDealerHand(), a_game.GetDealerScore());
            a_view.DisplayPlayerHand(a_game.GetPlayerHand(), a_game.GetPlayerScore());
        }
    }
}
