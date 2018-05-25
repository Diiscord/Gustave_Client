using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using UnityEngine.SceneManagement;

public class DisconnectButton : MonoBehaviour, IInputClickHandler
{
    GameObject camera;
    GameObject clientManager;
    GameObject inputManager;

    public void OnInputClicked(InputClickedEventData eventData)
    {

        //supprimer tous les fichiers du dossier
        ClientManager.CM.setUserInfo(null);
        Destroy(camera);
        Destroy(clientManager);
        Destroy(inputManager);
        SceneManager.LoadScene(2);
    }

    // Use this for initialization
    void Start () {
        camera = GameObject.Find("MixedRealityCameraParent");
        clientManager = GameObject.Find("ClientManager");
        inputManager = GameObject.Find("InputManager");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
