using UnityEngine;
using System.Threading;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using Warforged;

public class OnClick : MonoBehaviour, IPointerClickHandler
{

    // Use this for initialization
    void Start () {
        //Thread t = new Thread(()=>new Game());
        im = gameObject.GetComponent<Image>();
        button = gameObject.GetComponent<Button>();
        if(cardDict == null)
        {
            cardDict = new Dictionary<string, Character.Card>();
            foreach(string t in cardTags)
            {
                cardDict.Add(t, null);
            }

        }
        if(buttonDict == null)
        {
            buttonDict = new Dictionary<string, object>();
            allButtons = new List<Button>(FindObjectsOfType<Button>());
            allButtons.Sort((x,y) => x.tag.CompareTo(y.tag));
            Debug.Log(allButtons.Count);
            foreach (string t in buttonTags)
            {
                buttonDict.Add(t, null);
            }
            for (int i = 0; i < 6; ++i)
            {
                allButtons[i].gameObject.SetActive(false);
                allButtons[i].GetComponentInChildren<Text>().text = "";
            }

        }

    }
	
	// Update is called once per frame
	void Update ()
    {

    }
    public static List<Button> allButtons;
    public static Character.Card cardReturn = null;
    public static object buttonReturn = null;
    static Dictionary<string, Character.Card> cardDict = null;
    static List<string> cardTags = new List<string>() {"Invocation1","Invocation2","Invocation3","Invocation4",
            "Hand1", "Hand2", "Hand3" , "Hand4" , "Hand5" ,"Hand6" ,"Hand7" ,"Hand8" ,"Hand9" ,"Hand10",
            "Standby1","Standby2","Standby3","Standby4",
            "Suspend1","Suspend2","Suspend3","Suspend4","Suspend5","Suspend6","Suspend7","Suspend8","Suspend9","Suspend10",
            "Link1_1","Link1_2","Link1_3","Link1_4","Link1_5","Link1_6","Link1_7","Link1_8",
            "Link2_1","Link2_2","Link2_3","Link2_4","Link2_5","Link2_6","Link2_7","Link2_8",
            "Link3_1","Link3_2","Link3_3","Link3_4","Link3_5","Link3_6","Link3_7","Link3_8",
            "Link4_1","Link4_2","Link4_3","Link4_4","Link4_5","Link4_6","Link4_7","Link4_8",
            "Character","PlaySlot"};
    static List<string> buttonTags = new List<string>() { "Choice1", "Choice2", "Choice3", "Choice4", "Choice5", "Choice6" };
    static Dictionary<string, object> buttonDict = null;
    private Image im;
    private Button button;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Something clicked");
        if (cardTags.Contains(im.tag))
        {
            //Do card clicky stuff
            Debug.Log(im.tag);
        }
        else if (buttonTags.Contains(im.tag))
        {
            //Do button clicky stuff
            Debug.Log("Tag: "+button.tag);
            buttonReturn = buttonDict[button.tag];
            Debug.Log("Return: " + buttonReturn);
            for (int i = 0; i < 6; ++i)
            {
                allButtons[i].gameObject.SetActive(false);
                allButtons[i].GetComponentInChildren<Text>().text = "";
            }
        }
        else
        {

        }
    }

    public static void setButtonOptions(string promptText,List<string> names,List<object> returns)
    {
        for(int i =0; i<Math.Min(Math.Min(names.Count,6),returns.Count);++i)
        {
            Debug.Log("Setting Button options");
            allButtons[i].gameObject.SetActive(true);
            allButtons[i].GetComponentInChildren<Text>().text = names[i];
            buttonDict[allButtons[i].tag] = returns[i];
        }
    }
}
