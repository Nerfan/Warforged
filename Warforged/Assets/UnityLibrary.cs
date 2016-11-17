using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Warforged
{
    public class UnityLibrary : WindowLibrary
    {
        public Barrier barrier = new Barrier(2);
        private int threadID = 0;

        public UnityLibrary():base()
        {
            threadID = barrier.AddThread();
        }
        

        public override void clearAllHighlighting()
        {
            throw new NotImplementedException();
        }

        public override void clearHighlight(Character.Card card)
        {
            throw new NotImplementedException();
        }

        public override void highlight(Character.Card card, byte r, byte g, byte b)
        {
            throw new NotImplementedException();
        }

        public override object multiPrompt(string text, List<string> buttonTexts, List<object> returnTypes)
        {
            throw new NotImplementedException();
        }

        public override void setPromptText(string text)
        {
            throw new NotImplementedException();
        }

        public override void setupEdros(int player)
        {
            throw new NotImplementedException();
        }

        public override void updateOpponentUI(Character ch, bool showCurrCard, bool showHand)
        {
            throw new NotImplementedException();
        }

        public override void updateUI(Character ch, bool showCurrCard)
        {
            throw new NotImplementedException();
        }

        public override Character.Card waitForClick()
        {
            throw new NotImplementedException();
        }

        public override Character.Card waitForClickOrCancel(string text)
        {
            throw new NotImplementedException();
        }

        public override bool yesnoPrompt(string text)
        {
            StartGame.signal = () => { return StartGame.yesnoPrompt(text); };
            barrier.SignalAndWait(threadID);
            return (bool)returnObject;
        }
    }
}
