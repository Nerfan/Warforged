using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class OnEdrosSelect : MonoBehaviour, IPointerClickHandler{

    private MatchController controller;

    public void OnPointerClick(PointerEventData eventData)
    {
        controller = GameObject.Find("Match Controller").GetComponent<MatchController>();
        controller.localPlayer.deck = "Edros"; //Sets the local players deck, you can copy these two lines and change string to set any deck.


        StartGame.characterPick = "Edros";

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
