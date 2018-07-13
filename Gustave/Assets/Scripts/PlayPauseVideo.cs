using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;
using UnityEngine;
using System;

public class PlayPauseVideo : MonoBehaviour, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        bool val = this.transform.parent.gameObject.GetComponent<MovieCanvas>().playOrPauseVideo();
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
