using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class MenuRightArrow : MonoBehaviour, IInputClickHandler {

    public void OnInputClicked(InputClickedEventData eventData)
    {
        //print("heyhey");
        GameObject.Find("Menu Manager").GetComponent<MenuManager>().nextRecipeWindowsList();
        //MenuManager menu_manager = GameObject.Find("Menu Manager").GetComponent<MenuManager>();
       // menu_manager.updateJsonReaderAndLoadScene(0);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
