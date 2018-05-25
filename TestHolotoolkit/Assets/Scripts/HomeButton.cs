using System.Collections;
using System.Collections.Generic;
using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class HomeButton : MonoBehaviour, IInputClickHandler
{
    public void OnInputClicked(InputClickedEventData eventData)
    {
        ClientManager.CM.LoadMenuScene();
    }
        // Use this for initialization
        void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
