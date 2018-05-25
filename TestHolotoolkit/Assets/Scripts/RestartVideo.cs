using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using UnityEngine.UI;



public class RestartVideo : MonoBehaviour, IInputClickHandler {

    public void OnInputClicked(InputClickedEventData eventData)
    {
        this.transform.parent.gameObject.GetComponent<MovieCanvas>().RestartVideo();
        Text txt = this.gameObject.transform.parent.GetChild(1).GetChild(0).gameObject.GetComponent<Text>();
        txt.text = "Play";
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
