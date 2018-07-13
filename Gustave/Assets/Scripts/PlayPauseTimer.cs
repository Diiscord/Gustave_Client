using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using UnityEngine.UI;

public class PlayPauseTimer : MonoBehaviour, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        bool val = this.transform.parent.transform.parent.gameObject.GetComponent<Timer>().playOrPauseTimer();
        Text txt = this.gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();

        if (val)
            txt.text = "Pause";
        else
            txt.text = "Play";
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
