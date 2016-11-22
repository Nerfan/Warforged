﻿using System;
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
            //throw new NotImplementedException();
        }

        public override void clearHighlight(Character.Card card)
        {
            //throw new NotImplementedException();
        }

        public override void highlight(Character.Card card, byte r, byte g, byte b)
        {
            //throw new NotImplementedException();
        }

        public override object multiPrompt(string text, List<string> buttonTexts, List<object> returnTypes)
        {
            OnClick.buttonReturn = OnClick.NoReturn;
            StartGame.signal = () => { return StartGame.multiPrompt(text,buttonTexts,returnTypes); };
            barrier.SignalAndWait(threadID);
            return returnObject;
        }

        public override void setPromptText(string text)
        {
            OnClick.buttonReturn = OnClick.NoReturn;
            StartGame.signal = () => { return StartGame.setPromptText(text); };
            barrier.SignalAndWait(threadID);
        }

        public override void setupEdros(int player)
        {
            if(player == 2)
            {
                StartGame.signal = () => { return StartGame.setupEdros(OnClick.OCardImages); };
                barrier.SignalAndWait(threadID);
            }
            else
            {
                StartGame.signal = () => { return StartGame.setupEdros(OnClick.CardImages); };
                barrier.SignalAndWait(threadID);
            }
        }

        public override void updateOpponentUI(Character ch, bool showCurrCard, bool showHand)
        {
            StartGame.signal = () => { return StartGame.updateOpponentUI(ch, showCurrCard,showHand); };
            barrier.SignalAndWait(threadID);
        }

        public override void updateUI(Character ch, bool showCurrCard)
        {
            StartGame.signal = () => { return StartGame.updateUI(ch,showCurrCard); };
            barrier.SignalAndWait(threadID);
        }

        public override Character.Card waitForClick()
        {
            OnClick.cardReturn = OnClick.NoReturn;
            StartGame.signal = () => { return StartGame.waitForClick(); };
            barrier.SignalAndWait(threadID);
            return (Character.Card)returnObject;
        }

        public override Character.Card waitForClickOrCancel(string text)
        {
            OnClick.buttonReturn = OnClick.NoReturn;
            OnClick.cardReturn = OnClick.NoReturn;
            StartGame.signal = () => { return StartGame.waitForClickOrCancel(text); };
            barrier.SignalAndWait(threadID);
            return (Character.Card)returnObject;
        }

        public override bool yesnoPrompt(string text)
        {
            OnClick.buttonReturn = OnClick.NoReturn;
            StartGame.signal = () => { return StartGame.yesnoPrompt(text); };
            barrier.SignalAndWait(threadID);
            return (bool)returnObject;
        }
    }
}