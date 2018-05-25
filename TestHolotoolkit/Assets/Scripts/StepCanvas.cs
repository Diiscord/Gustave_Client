using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StepCanvas : MonoBehaviour {

    // Use this for initialization

    [SerializeField]
    private GameObject _prefabRightArrow;

    [SerializeField]
    private GameObject _prefabLeftArrow;

    public Square square;
    public TextAsset json;
    public Text stepDescribe;
    public Square[] squareArray;
    public GameObject help;
    public Mesh squareMesh;
    public Mesh tubeMesh;
    public GameObject _leftArrow;
    public GameObject _rightArrow;

    public int progress = -1;
    int stepLength = 0;
    
    void Start () {

    }

    public void InitCanvas(JsonRecipeReader json_recipe_reader)
    {
        Debug.Log("HEYYYYYYYY");
        stepDescribe.text = json_recipe_reader.getCurrentRecipeTitle();
        UpdateHud(json_recipe_reader);
        print("here :" + json_recipe_reader.getCurrentRecipeTitle());
        stepLength = json_recipe_reader.getCurrentRecipeNbSteps();
        squareArray = new Square[stepLength];
        updateArrows();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateHud(JsonRecipeReader json_recipe_reader)
    {
        print("it's me");
        var yoffset = 140;
        var space = 60;
        var posx = -330;
        var posy = 0;

        UpdateHelp();
        for (int i = 0; i < squareArray.Length; i++)
            if (squareArray[i] != null)
                Destroy(squareArray[i].gameObject);
        for (int i = 0; i < stepLength; i++)
        {
            if (i - 1 == progress || i + 1 == progress)
            {
                posx += 30;
                posy = 10 + yoffset;
            } else if (i == progress) {
                posx += 50;
                posy = 0 + yoffset;
            }
            else
            {
                posx += 20;
                posy = 20 + yoffset;
            }
            Square tmp;
            tmp = (Instantiate(square, new Vector3(posx, posy, 0), Quaternion.identity) as Square);
            tmp.transform.SetParent(this.transform, false);
            squareArray[i] = tmp;
            posx += space;
            if (i - 1 == progress || i + 1 == progress)
            {
                RectTransform rt = (RectTransform)squareArray[i].transform;
                rt.sizeDelta = new Vector2(60, 60);
            }
            print(">>>>>>");

            print(i);
            print(progress);
            if (i == progress)
            {
                RectTransform rt = (RectTransform)squareArray[i].transform;
                rt.sizeDelta = new Vector2(90, 90);
                //text de l'étape !!
                print("hello ?");
                stepDescribe.text = json_recipe_reader.getCurrentRecipeStepText(i);
            }
        }
    }
    //Les petits carrés à refaire!!!!
    void UpdateHelp()
    {
        if (progress >= 2 && progress < 6) // Attention ça le refait à chaque fois
        {
            help.GetComponent<MeshFilter>().mesh = squareMesh;
            Vector3 scale = help.transform.localScale;
            scale.x = 0.035F;
            scale.y = 0.007F;
            scale.z = 0.007F;
            help.transform.eulerAngles = new Vector3(0, 0, 0);
            help.transform.localScale = scale;
        }
        else if (progress >= 6 && progress < 7)
        {
            help.GetComponent<MeshFilter>().mesh = tubeMesh;
            Vector3 scale = help.transform.localScale;
            scale.x = 0.025F;
            scale.y = 0.06F;
            scale.z = 0.025F;
            help.transform.eulerAngles = new Vector3(0, 0, 90);
            help.transform.localScale = scale;
        }
        else if (progress >= 7)
        {
            help.GetComponent<MeshFilter>().mesh = tubeMesh;
            Vector3 scale = help.transform.localScale;
            scale.x = 0.03F;
            scale.y = 0.01F;
            scale.z = 0.03F;
            help.transform.localScale = scale;
        }
        else
        {

        }
    }

    public void NextStep(JsonRecipeReader json_recipe_reader)
    {
        progress += 1;
        if (progress >= stepLength)
        {
            //SceneManager.LoadScene(0);
            progress = 0;
        }
        UpdateHud(json_recipe_reader);
        updateArrows();

    }

    public void updateArrows()
    {
        Destroy(_rightArrow);
        Destroy(_leftArrow);
        if (progress != -1)
        {
            print("progress != -1");
            _leftArrow = instantiateArrow(_prefabLeftArrow);
            _leftArrow.GetComponent<MenuLeftArrow>().enabled = false;
            _leftArrow.GetComponent<RecipeLeftArrow>().enabled = true;
        }
        print("progress : " + progress + " -- steplength : " + stepLength);
        if (progress < stepLength - 1)
        {
            _rightArrow = instantiateArrow(_prefabRightArrow);
            _rightArrow.GetComponent<MenuRightArrow>().enabled = false;
            _rightArrow.GetComponent<RecipeRightArrow>().enabled = true;
        }
    }

    public void PrevStep(JsonRecipeReader json_recipe_reader)
    {
        progress -= 1;
        if (progress < 0)
            progress = 0;
        UpdateHud(json_recipe_reader);
      
        updateArrows();
    }

    private GameObject instantiateArrow(GameObject arrow)
    {
        GameObject tmp = Instantiate(arrow);
        tmp.transform.SetParent(gameObject.transform, false);
        return tmp;
    }
    public int getCurrentStep()
    {
        return (progress);
    }

    public int getTotalSteps()
    {
        return (stepLength);
    }
}