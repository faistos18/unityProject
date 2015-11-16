using UnityEngine;
using System.Collections;

public class SpectatorTooltip : MonoBehaviour {

	// Use this for initialization

    private bool isMouseOn;

    private GameObject spectatorTooltip;
	void Start ()
    {
        isMouseOn = false;


        spectatorTooltip = GameObject.FindWithTag("Tooltip");
	}
	
	// Update is called once per frame
	void Update()
    {
        if(isMouseOn)
        {
            //Puts the tooltip is the right place.
            RectTransform rectTransform = (RectTransform)spectatorTooltip.GetComponent("RectTransform");
            rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x - Screen.width / 2.0f, Input.mousePosition.y - Screen.height / 2.0f);
        }
	}
    void OnMouseEnter()
    {
        Debug.Log("Mouse Enter Tooltip");

        //Puts the tooltip is the right place.
        RectTransform rectTransform = (RectTransform)spectatorTooltip.GetComponent("RectTransform");
        rectTransform.anchoredPosition = new Vector2(Input.mousePosition.x - Screen.width/2.0f, Input.mousePosition.y-Screen.height/2.0f);


        CanvasGroup alphaCanvas = (CanvasGroup)spectatorTooltip.GetComponent("CanvasGroup");
        alphaCanvas.alpha = 0.7f;
        isMouseOn=true;
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse Exit Tooltip");

        CanvasGroup alphaCanvas = (CanvasGroup)spectatorTooltip.GetComponent("CanvasGroup");

        alphaCanvas.alpha = 0;
        isMouseOn = false;
    }
}

/*
var toolTipText = ""; // set this in the Inspector

private var currentToolTipText = "";
private var guiStyleFore : GUIStyle;
private var guiStyleBack : GUIStyle;

function Start()
{
    guiStyleFore = new GUIStyle();
    guiStyleFore.normal.textColor = Color.white;
    guiStyleFore.alignment = TextAnchor.UpperCenter ;
    guiStyleFore.wordWrap = true;
    guiStyleBack = new GUIStyle();
    guiStyleBack.normal.textColor = Color.black;
    guiStyleBack.alignment = TextAnchor.UpperCenter ;
    guiStyleBack.wordWrap = true;
}

function OnMouseEnter ()
{
     currentToolTipText = toolTipText;
}

function OnMouseExit ()
{
    currentToolTipText = "";
}

function OnGUI()
{
    if (currentToolTipText != "")
    {
        var x = Event.current.mousePosition.x;
        var y = Event.current.mousePosition.y;
        GUI.Label (Rect (x-149,y+21,300,60), currentToolTipText, guiStyleBack);
        GUI.Label (Rect (x-150,y+20,300,60), currentToolTipText, guiStyleFore);
    }
}
 * */


