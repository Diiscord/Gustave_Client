using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class Close_button : MonoBehaviour, IInputClickHandler
{

    // Use this for initialization
    void Start () {

    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        GameObject.Find("Error Popup").SetActive(false);
    }


    // Update is called once per frame
    void Update () {
		
	}
}
