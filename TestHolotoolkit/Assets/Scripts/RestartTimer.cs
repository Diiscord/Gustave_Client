using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;

using System;

public class RestartTimer : MonoBehaviour, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        this.transform.parent.transform.parent.gameObject.GetComponent<Timer>().playOrPauseTimer();
        this.transform.parent.transform.parent.gameObject.GetComponent<Timer>().restartTimer();
        Text txt  = this.gameObject.transform.parent.transform.GetChild(1).transform.GetChild(0).gameObject.GetComponent<Text>();
        txt.text = "Play";
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
