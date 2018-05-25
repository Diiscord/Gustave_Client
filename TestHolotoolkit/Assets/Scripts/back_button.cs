using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class back_button : MonoBehaviour, IInputClickHandler {

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject parent = GameObject.Find("Menu Manager");
        find_object.FindObject(parent, "Menu Canvas").SetActive(true);
        find_object.FindObject(parent, "Sign In Menu").SetActive(false);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
