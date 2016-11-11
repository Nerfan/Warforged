using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Warforged
{
    public class GameWindowLibrary
    {

        public GameWindow gameWindow = null;
        public Barrier barrier = new Barrier(2);
        private object returnObject = null;

        //GameWindowLibrary provides shorthand utility which allows for the model to interact with the UI.
        public GameWindowLibrary(GameWindow window)
        {
            gameWindow = window;
            gameWindow.library = this;
        }
        //Tell the UI that a player is going to be Edros. You only need to do this once per game.
        //@param isP2: If this value is 2, the UI is notified that P2 is Edros, otherwise P1 will be Edros
        //This function must be called twice, once for each player.
        public void setupEdros(int player)
        {
            if (player == 2)
            {
                gameWindow.Dispatcher.BeginInvoke((Action)(() => gameWindow.setupEdros(gameWindow.OCardImages)));
            }
            else
            {
                gameWindow.Dispatcher.BeginInvoke((Action)(() => gameWindow.setupEdros(gameWindow.CardImages)));
            }
        }

        //Updates the UI for Player 1 based on the character object passed in.
        //@param showCurrCard: If this is true the played card will be shown on the UI, otherwise the back of the card is used.
        public void updateUI(Character ch, bool showCurrCard)
        {
            gameWindow.Dispatcher.BeginInvoke((Action)(() => gameWindow.UpdateUI(ch, showCurrCard)));
        }

        //Updates the UI for Player 2 based on the character object passed in.
        //@param showCurrCard: If this is true the played card will be shown on the UI, otherwise the back of the card is used.
        //@param showHand: If this is true the Opponent's hand will be shown to the user, otherwise the hand will show the back of the cards.
        public void updateOpponentUI(Character ch, bool showCurrCard, bool showHand)
        {
            gameWindow.Dispatcher.BeginInvoke((Action)(() => gameWindow.UpdateOpponentUI(ch, showCurrCard, showHand)));
        }

        public void setPromptText(string text)
        {
            gameWindow.Dispatcher.BeginInvoke((Action)(() => gameWindow.setPromptText(text) ));
        }

        //Provide a yes/no prompt to the user with the given text.
        public bool yesnoPrompt(string text)
        {
            gameWindow.Dispatcher.BeginInvoke((Action)(() => gameWindow.yesnoPrompt(text) ));
            barrier.SignalAndWait();
            barrier = new Barrier(2);
            setPromptText("");
            return (bool)returnObject;
        }

        //Provide a multitude of options to the user
        //@param buttonTexts: 
        public object multiPrompt(string text,List<string> buttonTexts, List<object> returnTypes)
        {
            gameWindow.Dispatcher.BeginInvoke((Action)(() => gameWindow.multiPrompt(text, buttonTexts,returnTypes) ));
            barrier.SignalAndWait();
            barrier = new Barrier(2);
            setPromptText("");
            return returnObject;
        }

        public Character.Card waitForClick()
        {
            gameWindow.Dispatcher.BeginInvoke((Action)(() => gameWindow.waitForClick() ));
            barrier.SignalAndWait();
            barrier = new Barrier(2);
            return (Character.Card)returnObject;
        }

        //Used in the UI to set a returning value to this Library.
        //You probably should not use this unless you are coding the UI.
        public void setReturnObject(object o)
        {
            returnObject = o;
            this.barrier.SignalAndWait();
        }

    }
}
