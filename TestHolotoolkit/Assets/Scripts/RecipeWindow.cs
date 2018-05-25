using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HoloToolkit.Unity.InputModule;
using System;

public class RecipeWindow : MonoBehaviour, IInputClickHandler {
    //string json_file_name = null;
    int nb = 0;

	// Use this for initialization
	void Start () {
		
	}


	void Update () {
		
	}

    public void setInfo(string recipe_title, int pos) {
        Text text = this.transform.GetChild(1).GetComponent<Text>();
        text.text = recipe_title;
        nb = pos;
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
         
        MenuManager menu_manager = GameObject.Find("Menu Manager").GetComponent<MenuManager>();
        menu_manager.updateJsonReaderAndLoadScene(nb);
    
    }
}
