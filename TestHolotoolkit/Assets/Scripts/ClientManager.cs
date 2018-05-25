using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ClientManager : MonoBehaviour {

    private static ClientManager _cm = new ClientManager();
    public static JsonRecipeReader jsonRecipeReader = new JsonRecipeReader();
    public static UserInformations usrInfo = null;
    public List<string> jsonStrings;
    private string[] Scenes = { "Scenes/Gustave_menu_scene", "Scenes/Gutave_recipe_scene" };

    

    public enum Command { RIGHT, LEFT, NONE };

    public static ClientManager CM
    {
        get
        {

            return _cm;
        }
    }

    private static bool created = false;

    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
            Debug.Log("Awake: " + this.gameObject);
        }
    }

    void Start()
    {
        if (jsonRecipeReader == null)
        {
            initJsonReader();
        }
    }

    public void LoadRecipeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadConnectMenuScene()
    {
        SceneManager.LoadScene(2);
    }

    public ClientManager.Command checkCommands()
    {
        if (Input.GetKeyDown("right"))
            return Command.RIGHT;

        if (Input.GetKeyDown("left"))
            return Command.LEFT;

        return Command.NONE;
    }
    
    public void initJsonReader()
    {
        jsonRecipeReader = new JsonRecipeReader();
    }

    public JsonRecipeReader getJsonReader()
    {
        return jsonRecipeReader;
    }

    public void setUserInfo(UserInformations info)
    {
        Debug.Log("SET USR INFO");
        usrInfo = info;
    }

    public void setJsonStringList(List<string> list)
    {
        jsonStrings = list;
    }

}
