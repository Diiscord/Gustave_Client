using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class MenuLeftArrow : MonoBehaviour, IInputClickHandler {
    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject.Find("Menu Manager").GetComponent<MenuManager>().prevRecipeWindowsList();

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
