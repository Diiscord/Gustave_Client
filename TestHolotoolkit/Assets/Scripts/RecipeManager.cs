using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeManager : MonoBehaviour {
    public TextAsset _json;
    private JsonRecipeReader _jsonRecipeReader;
    private IngredientListCanvas _ingredientListCanvas;
    private StepCanvas _stepCanvas;
    private Movie _movie;
    private Timer _timer;
    private bool _timerset = false;
    private bool _videoset = false;
    public ClientManager _cm;

    // Use this for initialization
    void Start () {
        print("----start");
        _cm = (ClientManager)FindObjectOfType(typeof(ClientManager));
        print(_cm.getJsonReader().getCurrentRecipeTitle());
        _stepCanvas = this.transform.GetChild(0).gameObject.GetComponent<StepCanvas>();
        _ingredientListCanvas = this.transform.GetChild(1).gameObject.GetComponent<IngredientListCanvas>();
        _timer = this.transform.GetChild(2).gameObject.GetComponent<Timer>();
        _movie = this.transform.GetChild(3).gameObject.GetComponent<Movie>();
        _jsonRecipeReader = _cm.getJsonReader();

        //CERIC A DECOMMENTER
        //_jsonRecipeReader.setJsonRecipe("");
        _stepCanvas.InitCanvas(_jsonRecipeReader);
        _ingredientListCanvas.InitCanvasIngredientList(_jsonRecipeReader);
    }
	
	// Update is called once per frame
	void Update () {
        _ingredientListCanvas.UpdateStep(_stepCanvas.progress, _jsonRecipeReader);
        checkVideo();
        checkTimer();
    }

    void checkTimer()
    {
        if (_jsonRecipeReader.getCurrentRecipeStepTimerFlag(_stepCanvas.progress) == "1" && !_timerset)
        {
            _timer.createTimer();
            _timer.setTimer(_jsonRecipeReader.getCurrentRecipeStepTimer(_stepCanvas.progress));
            _timerset = true;
        }
        else if (_jsonRecipeReader.getCurrentRecipeStepTimerFlag(_stepCanvas.progress) == "0" && _timerset)
        {
            _timer.destroyTimer();
            _timerset = false;
        }
    }

    void checkVideo()
    {
        if (_jsonRecipeReader.getCurrentRecipeStepVideoFlag(_stepCanvas.progress) == "1" && !_videoset)
        {
            _movie.createMovieCanvas();
            _movie.setMovieTexture(_jsonRecipeReader.getCurrentRecipeStepVideoPath(_stepCanvas.progress));
            _videoset = true;
        }
        else if (_jsonRecipeReader.getCurrentRecipeStepVideoFlag(_stepCanvas.progress) == "0" && _videoset)
        {
            _movie.destroyMovieCanvas();
            _videoset = false;
        }
    }

    public void nextStep()
    {
        _stepCanvas.NextStep(_jsonRecipeReader);

    }

    public void prevStep()
    {
        _stepCanvas.PrevStep(_jsonRecipeReader);
    }

    public StepCanvas getStepCanvas()
    {
        return (_stepCanvas);
    }


}
