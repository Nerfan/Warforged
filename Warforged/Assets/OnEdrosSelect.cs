﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class OnEdrosSelect : MonoBehaviour, IPointerClickHandler{


    public void OnPointerClick(PointerEventData eventData)
    {
        StartGame.characterPick = new Warforged.Edros();
        SceneManager.LoadScene("WarforgedBoard",LoadSceneMode.Single);

    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
