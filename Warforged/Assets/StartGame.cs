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
        yield return new WaitUntil(() => OnClick.buttonReturn != null);
        Debug.Log("After buttonReturn Call");
        lib.setReturnObject(OnClick.buttonReturn);
        yield return null;
    }
}
