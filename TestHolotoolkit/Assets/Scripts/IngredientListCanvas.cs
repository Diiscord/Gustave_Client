using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Helpers;

public class IngredientListCanvas : Singleton<IngredientListCanvas> {
    public TextAsset json;
    public Text listText;
    public RawImage image;
   
    int progress = 0;
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
       /* if (progress == 0)
            return;
        SetImage();    
        image.enabled = true;
		*/
	}

    public void InitCanvasIngredientList(JsonRecipeReader json_recipe_reader)
    {
        image.enabled = false;
        print("init ingredientlist");
        string str = json_recipe_reader.getCurrentRecipeTitle() + "\n";
        for (int i = 0; i < json_recipe_reader.getCurrentRecipeNbIngredient(); i++)
        {
            str += "- " + json_recipe_reader.getCurrentRecipeIngredientName(i) + "\n";
        }
        listText.text = str;
    }

    void SetImage(string image_path)
    {
        image.enabled = true;
        //print(recette["steps"][progress]["image"].ToString());
        image.GetComponent<RawImage>().texture = Resources.Load<Texture>(image_path);
    }

    public void UpdateStep(int progress_action, JsonRecipeReader json_recipe_reader)
    {

        progress = progress_action;
        if (progress <= 0)
            image.enabled = false;
        else
            SetImage(json_recipe_reader.getCurrentRecipeStepImage(progress));
    }
}
