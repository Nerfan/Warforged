using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Warforged;
using System;
using System.Threading;
using System.Collections.Generic;

public class StartGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
        OnClick.buttonReturn = OnClick.NoReturn;
        OnClick.cardReturn = OnClick.NoReturn;
        StartCoroutine(StartModel());
    }
	
	// Update is called once per frame
	void Update () {

	}

    public delegate IEnumerator ModelSignal();
    public static ModelSignal signal = null;
    public static UnityLibrary lib = null;
    private static int threadID = 0;

    IEnumerator StartModel()
    {
        Game g = new Game();
        Thread t = new Thread(Game.Main);
        t.Start();
        yield return new WaitUntil(() => Game.library != null && ((UnityLibrary)Game.library).barrier != null);
        lib = (UnityLibrary)Game.library;
        threadID = lib.barrier.AddThread();
        while (true)
        {
            Debug.Log("Before Check");
            yield return new WaitUntil(() => signal != null);
            Debug.Log("After Check");
            yield return signal();
            Debug.Log("After signal Call");
            lib.barrier.SignalAndWait(threadID);
            signal = null;
        }
    }

    public static IEnumerator yesnoPrompt(string text)
    {
        OnClick.setButtonOptions(text,new List<string>() {"Yes","No" },new List<object>() {true,false });
        yield return new WaitUntil(() => !OnClick.NoReturn.Equals(OnClick.buttonReturn));
        OnClick.Prompt.text = "";
        lib.setReturnObject(OnClick.buttonReturn);
        OnClick.buttonReturn = OnClick.NoReturn;
        yield return null;
    }
    public static IEnumerator multiPrompt(string text, List<string> buttonTexts,List<object> returns)
    {
        OnClick.Prompt.text = text;
        OnClick.setButtonOptions(text, buttonTexts, returns);
        yield return new WaitUntil(() => !OnClick.NoReturn.Equals(OnClick.buttonReturn));
        lib.setReturnObject(OnClick.buttonReturn);
        OnClick.buttonReturn = OnClick.NoReturn;
        yield return null;
    }

    public static IEnumerator setPromptText(string text)
    {
        OnClick.Prompt.text = text;
        yield return null;
    }

    public static IEnumerator waitForClick()
    {

        yield return new WaitUntil(() => !OnClick.NoReturn.Equals(OnClick.cardReturn));
        lib.setReturnObject(OnClick.cardReturn);
        OnClick.cardReturn = OnClick.NoReturn;
        yield return null;
    }

    public static IEnumerator waitForClickOrCancel(string text)
    {
        OnClick.setButtonOptions(text, new List<string>() { "Cancel" }, new List<object>() { null });
        yield return new WaitUntil(() => !OnClick.NoReturn.Equals(OnClick.cardReturn) || !OnClick.NoReturn.Equals(OnClick.buttonReturn));
        for (int i = 0; i < 6; ++i)
        {
            OnClick.allButtons[i].gameObject.SetActive(false);
            OnClick.allButtons[i].GetComponentInChildren<Text>().text = "";
        }
        if (!OnClick.NoReturn.Equals(OnClick.buttonReturn))
        {
            lib.setReturnObject(OnClick.buttonReturn);
        }
        else
        {
            lib.setReturnObject(OnClick.cardReturn);
        }
        OnClick.cardReturn = OnClick.NoReturn;
        OnClick.buttonReturn = OnClick.NoReturn;
        OnClick.Prompt.text = "";
        yield return null;
    }

    public static IEnumerator updateUI(Character ch, bool showCurrCard)
    {
        for(int i = 0; i<OnClick.Hand.Count; ++i)
        {
            if(ch.hand.Count <= i)
            {
                OnClick.Hand[i].sprite = null;
                OnClick.Hand[i].color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f);
                OnClick.Hand[i].gameObject.SetActive(false);
            }
            else
            {

                OnClick.Hand[i].sprite = OnClick.CardImages[ch.hand[i].name];
                OnClick.Hand[i].color = new UnityEngine.Color(1, 1, 1);
                OnClick.Hand[i].gameObject.SetActive(true);
                OnClick.cardDict["Hand"+i] = ch.hand[i];
            }
        }
        /*
        for (int i = 0; i < OnClick.Suspend.Count; ++i)
        {
            if (ch.Suspend.Count <= i)
            {
                OnClick.Suspend[i].sprite = null;
            }
            else
            {
                OnClick.Suspend[i].sprite = OnClick.CardImages[ch.name];
            }
        }*/
        for (int i = 0; i < OnClick.Standby.Count; ++i)
        {
            if (ch.standby.Count <= i)
            {
                OnClick.Standby[i].sprite = null;
                OnClick.Standby[i].color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.33f);
                OnClick.Standby[i].gameObject.SetActive(false);
            }
            else
            {
                OnClick.Standby[i].sprite = OnClick.CardImages[ch.standby[i].name];
                OnClick.Standby[i].color = new UnityEngine.Color(1, 1, 1);
                OnClick.Standby[i].gameObject.SetActive(true);
                OnClick.cardDict["Standby"+(i+1)] = ch.standby[i];
            }
        }
        for (int i = 0; i < OnClick.Invocation.Count; ++i)
        {
            if (ch.invocation.Count <= i)
            {
                OnClick.Invocation[i].sprite = null;
                OnClick.Invocation[i].color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.33f);
                OnClick.Invocation[i].gameObject.SetActive(false);
            }
            else if (!ch.invocation[i].active)
            {
                OnClick.Invocation[i].sprite = null;
                OnClick.Invocation[i].color = new UnityEngine.Color(0, 0, 0);
                OnClick.Invocation[i].gameObject.SetActive(true);
                OnClick.cardDict["Invocation" + (i + 1)] = OnClick.NoReturn; ;
            }
            else
            {
                OnClick.Invocation[i].sprite = OnClick.CardImages[ch.invocation[i].name];
                OnClick.Invocation[i].color = new UnityEngine.Color(1, 1, 1);
                OnClick.Invocation[i].gameObject.SetActive(true);
                OnClick.cardDict["Invocation"+(i+1)] = ch.invocation[i];
            }
        }
        if(ch.currCard == null)
        {
            OnClick.PlaySlot.sprite = null;
            OnClick.PlaySlot.color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.33f);
            OnClick.PlaySlot.gameObject.SetActive(false);
        }
        else if (showCurrCard)
        {
            OnClick.PlaySlot.sprite = OnClick.CardImages[ch.currCard.name];
            OnClick.PlaySlot.color = new UnityEngine.Color(1, 1, 1);
            OnClick.PlaySlot.gameObject.SetActive(true);
            OnClick.cardDict["PlaySlot"] = ch.currCard;
        }
        else
        {
            OnClick.PlaySlot.sprite = null;
            OnClick.PlaySlot.color = new UnityEngine.Color(0, 0, 0);
            OnClick.PlaySlot.gameObject.SetActive(true);
            OnClick.cardDict["PlaySlot"] = ch.currCard;
        }
        OnClick.CharacterSlot.sprite = OnClick.CardImages[ch.name];
        OnClick.CharacterSlot.color = new UnityEngine.Color(1, 1, 1);
        yield return null;
    }


    public static IEnumerator updateOpponentUI(Character ch, bool showCurrCard, bool showHand)
    {
        for (int i = 0; i < OnClick.OHand.Count; ++i)
        {
            if (ch.hand.Count <= i)
            {
                OnClick.OHand[i].sprite = null;
                OnClick.OHand[i].color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.0f);
                OnClick.OHand[i].gameObject.SetActive(false);
            }
            else if(!showHand)
            {
                OnClick.OHand[i].sprite = null;
                OnClick.OHand[i].color = new UnityEngine.Color(0, 0, 0,1);
                OnClick.OHand[i].gameObject.SetActive(true);
                OnClick.cardDict["OHand" + i] = ch.hand[i];
            }
            else
            {

                OnClick.OHand[i].sprite = OnClick.OCardImages[ch.hand[i].name];
                OnClick.OHand[i].color = new UnityEngine.Color(1, 1, 1,1);
                OnClick.OHand[i].gameObject.SetActive(true);
                OnClick.cardDict["OHand" + i] = ch.hand[i];
            }
        }
        /*
        for (int i = 0; i < OnClick.Suspend.Count; ++i)
        {
            if (ch.Suspend.Count <= i)
            {
                OnClick.Suspend[i].sprite = null;
            }
            else
            {
                OnClick.Suspend[i].sprite = OnClick.CardImages[ch.name];
            }
        }*/
        for (int i = 0; i < OnClick.OStandby.Count; ++i)
        {
            if (ch.standby.Count <= i)
            {
                OnClick.OStandby[i].sprite = null;
                OnClick.OStandby[i].color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.33f);
                OnClick.OStandby[i].gameObject.SetActive(false);
            }
            else
            {
                OnClick.OStandby[i].sprite = OnClick.OCardImages[ch.standby[i].name];
                OnClick.OStandby[i].color = new UnityEngine.Color(1, 1, 1);
                OnClick.OStandby[i].gameObject.SetActive(true);
                OnClick.cardDict["OStandby" + (i + 1)] = ch.standby[i];
            }
        }
        for (int i = 0; i < OnClick.OInvocation.Count; ++i)
        {
            if (ch.invocation.Count <= i)
            {
                OnClick.OInvocation[i].sprite = null;
                OnClick.OInvocation[i].color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.33f);
                OnClick.OInvocation[i].gameObject.SetActive(false);
            }
            else if (!ch.invocation[i].active)
            {
                OnClick.OInvocation[i].sprite = null;
                OnClick.OInvocation[i].color = new UnityEngine.Color(0, 0, 0);
                OnClick.OInvocation[i].gameObject.SetActive(true);
                OnClick.cardDict["OInvocation" + (i + 1)] = OnClick.NoReturn;
            }
            else
            {
                OnClick.OInvocation[i].sprite = OnClick.OCardImages[ch.invocation[i].name];
                OnClick.OInvocation[i].color = new UnityEngine.Color(1, 1, 1);
                OnClick.OInvocation[i].gameObject.SetActive(true);
                OnClick.cardDict["OInvocation" + (i + 1)] = ch.invocation[i];
            }
        }
        if (ch.currCard == null)
        {
            OnClick.OPlaySlot.sprite = null;
            OnClick.OPlaySlot.color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, 0.33f);
            OnClick.OPlaySlot.gameObject.SetActive(false);
        }
        else if (showCurrCard)
        {
            OnClick.OPlaySlot.sprite = OnClick.OCardImages[ch.currCard.name];
            OnClick.OPlaySlot.color = new UnityEngine.Color(1, 1, 1);
            OnClick.OPlaySlot.gameObject.SetActive(true);
            OnClick.cardDict["OPlaySlot"] = ch.currCard;
        }
        else
        {
            OnClick.OPlaySlot.sprite = null;
            OnClick.OPlaySlot.color = new UnityEngine.Color(0, 0, 0);
            OnClick.OPlaySlot.gameObject.SetActive(true);
            OnClick.cardDict["OPlaySlot"] = ch.currCard;
        }
        OnClick.OCharacterSlot.sprite = OnClick.OCardImages[ch.name];
        OnClick.OCharacterSlot.color = new UnityEngine.Color(1, 1, 1);
        yield return null;
    }


    public static IEnumerator setupEdros(Dictionary<string, Sprite> CurrCardImages)
    {

        CurrCardImages.Add("Celestial Surge", Resources.Load("CardImages/Edros/Celestial Surge", typeof(Sprite)) as Sprite);
        CurrCardImages.Add("Purging Lightning", Resources.Load("CardImages/Edros/Purging Lightning", typeof(Sprite)) as Sprite);
        CurrCardImages.Add("Crashing Sky", Resources.Load("CardImages/Edros/Crashing Sky", typeof(Sprite)) as Sprite);
        CurrCardImages.Add("Edros", Resources.Load<Sprite>("CardImages/Edros/Edros"));

        CurrCardImages.Add("Faith Unquestioned", Resources.Load("CardImages/Edros/Faith Unquestioned", typeof(Sprite)) as Sprite);
        CurrCardImages.Add("Grace of Heaven", Resources.Load("CardImages/Edros/Grace of Heaven", typeof(Sprite)) as Sprite);
        CurrCardImages.Add("Hand of Toren", Resources.Load("CardImages/Edros/Hand of Toren", typeof(Sprite)) as Sprite);

        CurrCardImages.Add("Pillar of Lightning", Resources.Load("CardImages/Edros/Pillar of Lightning", typeof(Sprite)) as Sprite);
        CurrCardImages.Add("Rolling Thunder", Resources.Load("CardImages/Edros/Rolling Thunder", typeof(Sprite)) as Sprite);
        CurrCardImages.Add("Scorn of Thunder", Resources.Load("CardImages/Edros/Scorn of Thunder", typeof(Sprite)) as Sprite);

        CurrCardImages.Add("Sky Blessed Shield", Resources.Load("CardImages/Edros/Sky Blessed Shield", typeof(Sprite)) as Sprite);
        CurrCardImages.Add("Toren's Favored", Resources.Load("CardImages/Edros/Toren's Favored", typeof(Sprite)) as Sprite);
        CurrCardImages.Add("Wrath of Lightning", Resources.Load("CardImages/Edros/Wrath of Lightning", typeof(Sprite)) as Sprite);
        yield return null;
    }
}
