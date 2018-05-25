using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movie : MonoBehaviour {

    [SerializeField]
    private GameObject _prefabMovieCanvas;

    MovieCanvas _movieCanvas;
    // Use this for initialization
    void Start () {
   
    }

    public void createMovieCanvas()
    {
        _movieCanvas = Instantiate(_prefabMovieCanvas).GetComponent<MovieCanvas>();
        _movieCanvas.transform.parent = gameObject.transform;
        _movieCanvas.transform.localPosition = Vector3.zero;
        _movieCanvas.transform.localScale = new Vector3(1, 1, 1);
        _movieCanvas.transform.GetChild(1).gameObject.AddComponent<PlayPauseVideo>();
        _movieCanvas.transform.GetChild(2).gameObject.AddComponent<RestartVideo>();

    }

    public void destroyMovieCanvas()
    {
        Destroy(_movieCanvas.gameObject);
    }

    public MovieCanvas getMovieCanvas()
    {
        return _movieCanvas;
    }
	
    public void setMovieTexture(string video_path)
    {
        _movieCanvas.setMovieTexture(Resources.Load("Videos/" + video_path) as MovieTexture);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
