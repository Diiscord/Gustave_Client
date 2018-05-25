using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class log_in_button : MonoBehaviour, IInputClickHandler {
 
    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject parent = GameObject.Find("Menu Manager");
        find_object.FindObject(parent, "Menu Canvas").SetActive(false);
        find_object.FindObject(parent, "Log In Menu").SetActive(true);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
