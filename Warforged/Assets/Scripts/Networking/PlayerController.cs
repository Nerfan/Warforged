﻿using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System;
using Warforged;
using System.Xml.Serialization;
using System.IO;

public class PlayerController : NetworkBehaviour
{
    #region Player Variables

    public string playerName = "Fill"; // Screen name for player
    public static Character deck = null;           // The Player's deck, set in second scene: WarforgedCharacterSelect.
    public Character opponent = null;
    public Character characterForTurn;         // String determining which card will be played by the local player each turn. Set by RPC calls below

    public string textToSend;          // Used in first scene for chat room
    public bool readyFlag = false;     // Used to determine whther local player is ready to move forward at any given moment.
    public static MatchController controller;// Match Controller reference
    public static PlayerController playerController;
    #endregion

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject); // Immortalizes object
        controller = GameObject.Find("Match Controller").GetComponent<MatchController>(); // Sets match controller reference.
        playerController = this;

    }

    void SetupReferences(Character ch, Character opponent)
    {
        if (opponent == null || ch == null)
            return;
        foreach(Character.Card c in ch.hand)
        {
            c.init(ch);
        }
        foreach (Character.Card c in ch.standby)
        {
            c.init(ch);
        }
        foreach (Character.Card c in ch.invocation)
        {
            c.init(ch);
        }
        //TODO: Add more like suspension and link
        if(ch.currCard != null)
            ch.currCard.init(ch);
        ch.setOpponent(opponent);
        opponent.setOpponent(ch);
    }

    #region NetworkBehaviour
    public override void OnStartClient() // Called on server when client is connected
    {
        base.OnStartClient();
        
        controller.OnPlayerStarted(this, null);
    }

    public override void OnStartLocalPlayer() // Called on client on connect
    {
        base.OnStartLocalPlayer();
        controller.OnPlayerStartedLocal(this, null);
    }

    void OnDestroy() // Called on disconnect
    {
        controller.OnPlayerDestroyed(this, null);
    }
    #endregion

    #region Networking

    /*
    The way this section works is the client can send a Cmd (command) to the server, which then
    (in this implementation) calls a Rpc (remote procedure call) which calls the following code 
    on every client currently connected to the system. I'm only going to comment the one's 
    relevant to you guys. Remember, you MUST call the Cmd version, or it will not sync correctly.
    All of the Cmd versions will call the Rpc versions of the methods.
    */
    
    [Command]
    public void CmdConnectionStatus(string status)
    {
        RpcConnectionStatus(status);
    }

    [ClientRpc]
    public void RpcConnectionStatus(string status)
    {
        Text text = GameObject.Find("Connection Status").GetComponent<Text>();
        text.text = status;
    }

    [Command]
    public void CmdSendMessage(string msg)
    {
        RpcSendMessage(msg);
    }

    [ClientRpc]
    void RpcSendMessage(string msg)
    {
        MessageBoard msgbrd = GameObject.Find("Messaging Board").GetComponent<MessageBoard>();
        msgbrd.updateMessageBox(msg);
    }

    [Command]
    public void CmdUnReady()
    {
        RpcUnReady();
    }

    [ClientRpc]
    void RpcUnReady()
    {
        /*
        This method takes in bool: isServer, which tells wether or not the client who called the method
        is the same client who is currently running it now (as it runs on all machines connected). If it
        is the same one, it sets the value of the ready flag on the local machine of this script. If it 
        is not, it sets the value on the remote machine.
        
        Use this method in conjuction with your own to tell both machines they are ready to make the 
        next turn in the game. If you need a better understanding, in my ButtonManager class this method was
        implemented twice so you can get an idea.
        */
        controller.localPlayer.readyFlag = false;
        controller.remotePlayer.readyFlag = false;
    }

    [Command]
    public void CmdImReady(bool isServer)
    {
        RpcImReady(isServer);
    }

    [ClientRpc]
    void RpcImReady(bool isServer)
    {
        /*
        This method takes in bool: isServer, which tells wether or not the client who called the method
        is the same client who is currently running it now (as it runs on all machines connected). If it
        is the same one, it sets the value of the ready flag on the local machine of this script. If it 
        is not, it sets the value on the remote machine.
        
        Use this method in conjuction with your own to tell both machines they are ready to make the 
        next turn in the game. If you need a better understanding, in my ButtonManager class this method was
        implemented twice so you can get an idea.
        */
        if (isServer == controller.localPlayer.isServer)
            controller.localPlayer.readyFlag = true;
        else
        {
            controller.remotePlayer.readyFlag = true;
        }
    }

    [Command]
    public void CmdMoveToScene(string scene)
    {
        RpcMoveToScene(scene);
    }

    [ClientRpc]
    void RpcMoveToScene(string scene)
    {
        // Simply tells both machines to go to the scene defined from the scene paramater.
        SceneManager.LoadScene(scene);
    }
    [Command]
    public void CmdSetCharacter(string data, bool isServer)
    {
        RpcSetCharacter(data, isServer);
    }

    [ClientRpc]
    public void RpcSetCharacter(string charcter, bool isServer)
    {
        /// Gets the opposing character from the network
        if (controller.localPlayer.isServer == isServer)
        {
            
        }
        else
        {
            /*XmlSerializer xml = new XmlSerializer(typeof(Character));
            Game.p1 = (Character)xml.Deserialize(new StringReader(charcter));
            SetupReferences(Game.p1, Game.p2);*/
            XmlSerializer xml = new XmlSerializer(typeof(Character));
            Game.p2 = (Character)xml.Deserialize(new StringReader(charcter));
            SetupReferences(Game.p2, Game.p1);
        }
    }

    [Command]
    public void CmdInit(string data, bool isServer)
    {
        RpcInit(data, isServer);
    }

    [ClientRpc]
    public void RpcInit(string charcter, bool isServer)
    {
        /// Gets the opposing character from the network
        if (controller.localPlayer.isServer == isServer)
        {
            
            
        }
        else
        {
            /*XmlSerializer xml = new XmlSerializer(typeof(Character));
            Game.p1 = (Character)xml.Deserialize(new StringReader(charcter));
            SetupReferences(Game.p1, Game.p2);*/
            XmlSerializer xml = new XmlSerializer(typeof(Character));
            Game.p2 = (Character)xml.Deserialize(new StringReader(charcter));
            SetupReferences(Game.p2, Game.p1);
        }
    }

    [Command]
    public void CmdCallTurn()
    {
        RpcCallTurn();
    }

    [ClientRpc]
    public void RpcCallTurn()
    {
        /*
        This method assumes that you have already called CmdSetCard for both players, meaning both have a string 
        defining what they will do and both players readyFlags are true. The flags don't necessarily have to be 
        part of the preconditions for this method, but I would recommend it as it is a very easy way to know when
        both players are ready to continue the game.
        */

        //Replace with name of function that starts turn.
    }
    #endregion
}
