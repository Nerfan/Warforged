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
                OnClick.cardDict[ch.hand[i].name] = ch.currCard;
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
                OnClick.cardDict[ch.standby[i].name] = ch.currCard;
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
                OnClick.cardDict[ch.invocation[i].name] = ch.currCard;
            }
            else
            {
                OnClick.Invocation[i].sprite = OnClick.CardImages[ch.invocation[i].name];
                OnClick.Invocation[i].color = new UnityEngine.Color(1, 1, 1);
                OnClick.Invocation[i].gameObject.SetActive(true);
                OnClick.cardDict[ch.invocation[i].name] = ch.currCard;
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
            OnClick.cardDict[ch.currCard.name] = ch.currCard;
        }
        else
        {
            OnClick.PlaySlot.sprite = null;
            OnClick.PlaySlot.color = new UnityEngine.Color(0, 0, 0);
            OnClick.PlaySlot.gameObject.SetActive(true);
            OnClick.cardDict[ch.currCard.name] = ch.currCard;
        }
        OnClick.CharacterSlot.sprite = OnClick.CardImages[ch.name];
        OnClick.CharacterSlot.color = new UnityEngine.Color(1, 1, 1);
        yield return null;
    }

    public static IEnumerator setupEdros(Dictionary<string, Sprite> CardImages)
    {

        CardImages.Add("Celestial Surge", Resources.Load("CardImages/Edros/Celestial Surge", typeof(Sprite)) as Sprite);
        CardImages.Add("Purging Lightning", Resources.Load("CardImages/Edros/Purging Lightning", typeof(Sprite)) as Sprite);
        CardImages.Add("Crashing Sky", Resources.Load("CardImages/Edros/Crashing Sky", typeof(Sprite)) as Sprite);
        CardImages.Add("Edros", Resources.Load<Sprite>("CardImages/Edros/Edros"));

        CardImages.Add("Faith Unquestioned", Resources.Load("CardImages/Edros/Faith Unquestioned", typeof(Sprite)) as Sprite);
        CardImages.Add("Grace of Heaven", Resources.Load("CardImages/Edros/Grace of Heaven", typeof(Sprite)) as Sprite);
        CardImages.Add("Hand of Toren", Resources.Load("CardImages/Edros/Hand of Toren", typeof(Sprite)) as Sprite);

        CardImages.Add("Pillar of Lightning", Resources.Load("CardImages/Edros/Pillar of Lightning", typeof(Sprite)) as Sprite);
        CardImages.Add("Rolling Thunder", Resources.Load("CardImages/Edros/Rolling Thunder", typeof(Sprite)) as Sprite);
        CardImages.Add("Scorn of Thunder", Resources.Load("CardImages/Edros/Scorn of Thunder", typeof(Sprite)) as Sprite);

        CardImages.Add("Sky Blessed Shield", Resources.Load("CardImages/Edros/Sky Blessed Shield", typeof(Sprite)) as Sprite);
        CardImages.Add("Toren's Favored", Resources.Load("CardImages/Edros/Toren's Favored", typeof(Sprite)) as Sprite);
        CardImages.Add("Wrath of Lightning", Resources.Load("CardImages/Edros/Wrath of Lightning", typeof(Sprite)) as Sprite);
        yield return null;
    }
}
