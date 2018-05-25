using ProgressBar;
using System.Collections;
using UnityEngine;

public class progressBar : MonoBehaviour
{
    ProgressBarBehaviour BarBehaviour;
    //GameObject

    int totalSteps = 0;
    int currentStep = 0;
    GameObject recipeManagerObj;
    RecipeManager recipeManager;

    void Start()
    {
        BarBehaviour = GetComponent<ProgressBarBehaviour>();
        BarBehaviour.Value = 0;
        recipeManagerObj = GameObject.Find("RecipeManager");
        recipeManager = recipeManagerObj.GetComponent<RecipeManager>();
        //totalSteps = this.transform.parent.parent.gameObject.GetComponent<RecipeManager>().getStepCanvas().getTotalSteps();
        //currentStep = this.transform.parent.parent.gameObject.GetComponent<RecipeManager>().getStepCanvas().getCurrentStep();
        totalSteps = recipeManager.getStepCanvas().getTotalSteps();
        currentStep = recipeManager.getStepCanvas().getCurrentStep();
    }

    void Update()
    {
        BarBehaviour.Value = currentStep / totalSteps * 100;
        print(currentStep);
        //currentStep += 1;
    }
}