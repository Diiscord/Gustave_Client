using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManagerGetter : MonoBehaviour {

    public static ClientManagerGetter CMG;
    public ClientManager _clientManager;
    // Use this for initialization
	void Start () {
        //print("found ?");
        //_clientManager = (ClientManager)FindObjectOfType(typeof(ClientManager));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
