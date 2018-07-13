using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class MenuManager : MonoBehaviour {

    [SerializeField]
    private GameObject _prefabRecipeWindow;

    [SerializeField]
    private GameObject _prefabRightArrow;

    [SerializeField]
    private GameObject _prefabLeftArrow;

    List<Action> _methodList = new List<Action>();
    List<RecipeWindow> _recipeWindows = null;
    List<string> _jsonFilePathList = null;
    JsonRecipeReader _jsonRecipeReader;
    List<GameObject> _recipeWindowsGameobjectList = new List<GameObject>();
    GameObject _rightArrow;
    GameObject _leftArrow;
    int _windowNb = 0;
    int _jsonPathStartPositionInList = 0;
    int _jsonPathCurrentPositionInList = 0;
    int _windowMax = 4;
    bool _multiplePages = false;
    int[] x_position = new int[] { 0, 400, 0, 400 };
    int[] y_position = new int[] { 0, 0, -250, -250 };
    ClientManager _cm;
    // Use this for initialization
    void Start () {

        _cm = (ClientManager)FindObjectOfType(typeof(ClientManager));
        if (ClientManager.CM.getJsonReader() == null)
        {
            ClientManager.CM.initJsonReader();
        }
        _jsonRecipeReader = ClientManager.CM.getJsonReader();
        //_jsonFilePathList = _jsonRecipeReader.getJsonFilePaths();
        createRecipeWindowsList();
        if (ClientManager.CM.jsonStrings.Count > 4)
        {
            _multiplePages = true;
            //  create Prev and Next Arrows !
        }
    }

    void addActionsToMethodsList()
    {
        /*_methodList.Add(() => nextRecipeWindowsList());
        _methodList.Add(() => prevRecipeWindowsList());
        _methodList.Add(() => updateJsonReaderAndLoadScene());*/
    }

   
	// Update is called once per frame
	void Update () {
        //if (ClientManager.CM.checkCommands() == ClientManager.CM.Right)
        //  nextRecipeWindowsList();
        checkCommands();

    }

    GameObject createRecipeWindow(string json_path, int nb)
    {
        GameObject recipe_window = null;
        
        recipe_window = Instantiate(_prefabRecipeWindow);
        recipe_window.transform.SetParent(gameObject.transform.GetChild(0).transform, false);
        recipe_window.transform.localPosition += new Vector3(x_position[_windowNb], y_position[_windowNb], 0);
        recipe_window.transform.localScale = new Vector3(1, 1, 1);
        //.setJsonFileName(json_path);
        //recipe_window.GetComponent<RecipeWindow>().setJsonFileName(json_path);
        //print("json file name" + json_path);
        print("create recipe window");
        recipe_window.GetComponent<RecipeWindow>().setInfo(_jsonRecipeReader.getFastRecipeTitle(json_path), nb);
        
        return recipe_window;
    }

    void createRecipeWindowsList()
    {
        _windowNb = 0;
        //print(window_nb);
        print("icicici" + ClientManager.CM.jsonStrings[0]);
        if (ClientManager.CM.jsonStrings.Count == 0)
            return;
        print("here is the page :" + _jsonPathStartPositionInList.ToString());
        print("max = " + _windowMax.ToString());
        print("size = " + ClientManager.CM.jsonStrings.Count.ToString());
        //_jsonFilePathList.Count
        for (; (_windowNb < _windowMax) && (_windowNb <  ClientManager.CM.jsonStrings.Count - _jsonPathStartPositionInList); _windowNb++) {
           // _jsonFilePathList
            print(ClientManager.CM.jsonStrings[_jsonPathCurrentPositionInList]);
            //createRecipeWindow(_jsonFilePathList[_jsonPathCurrentPositionInList])
            _recipeWindowsGameobjectList.Add(createRecipeWindow(ClientManager.CM.jsonStrings[_jsonPathCurrentPositionInList], _jsonPathCurrentPositionInList));
            _jsonPathCurrentPositionInList++;
        }
        if (_jsonPathStartPositionInList != 0) { 
            _leftArrow = instantiateArrow(_prefabLeftArrow);
            _leftArrow.GetComponent<MenuLeftArrow>().enabled = true;
            _leftArrow.GetComponent<RecipeLeftArrow>().enabled = false;
        }
        //_jsonFilePathList.Count
        if (_jsonPathStartPositionInList <  ClientManager.CM.jsonStrings.Count - 4) { 
            _rightArrow = instantiateArrow(_prefabRightArrow);
            _rightArrow.GetComponent<MenuRightArrow>().enabled = true;
            _rightArrow.GetComponent<RecipeRightArrow>().enabled = false;
        }
    }

    private GameObject instantiateArrow(GameObject arrow)
    {
        GameObject tmp = Instantiate(arrow);
        tmp.transform.SetParent(gameObject.transform.GetChild(0).transform, false);
        return tmp;
    }

    private void destroyWindowsGameobjectList()
    {
        GameObject tmp_window;
        while (_recipeWindowsGameobjectList.Count != 0)
        {
             tmp_window = _recipeWindowsGameobjectList[0];
            _recipeWindowsGameobjectList.RemoveAt(0);
            Destroy(tmp_window);
        }
        Destroy(_rightArrow);
        Destroy(_leftArrow);
    } 

    public void nextRecipeWindowsList()
    {
        if (_jsonPathStartPositionInList + 4 > _jsonFilePathList.Count - 1) {
            return;
        }
        _jsonPathStartPositionInList += 4;
        destroyWindowsGameobjectList();
        _jsonPathCurrentPositionInList = _jsonPathStartPositionInList;
        createRecipeWindowsList();
    }

    public void prevRecipeWindowsList()
    {
        if (_jsonPathStartPositionInList == 0)
            return;
        _jsonPathStartPositionInList -= 4;
        destroyWindowsGameobjectList();
        _jsonPathCurrentPositionInList = _jsonPathStartPositionInList;
        createRecipeWindowsList();
    }

    public void updateJsonReaderAndLoadScene(int nb)
    {
        Debug.Log(ClientManager.CM.jsonStrings);
        Debug.Log(ClientManager.CM.jsonStrings[nb]);
        ClientManager.CM.getJsonReader().setJsonRecipe(ClientManager.CM.jsonStrings[nb]);
        ClientManager.CM.LoadRecipeScene();
    }

    public int getJsonFilePathListNb(String json_file)
    {
        return _jsonFilePathList.IndexOf(json_file);
    }

    void checkCommands()
    {
        /*ClientManager.Command command = ClientManager.CM.checkCommands();
        if (command == ClientManager.Command.NONE)
            return;
        _methodList[(int)command].Invoke();*/

        if (Input.GetKeyDown("right"))
            nextRecipeWindowsList();

        if (Input.GetKeyDown("left"))
            prevRecipeWindowsList();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            updateJsonReaderAndLoadScene(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            updateJsonReaderAndLoadScene(3);
    }

}
