using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField]
    private GameObject _prefabProgressBar;

    [SerializeField]
    private GameObject _prefabTimerPanel;

    // Use this for initialization
    float _timeLeft = 0f;
    float _currenttime = 0f;
    float _totaltime = 0f;
    bool _timerplay = false;
    bool flag = true;
    ProgressBar.ProgressRadialBehaviour _progressRadialHollow;
    GameObject _timerPanel;
    //GameObject cube;

    void Start () {
        
    }

    public void createTimer()
    {
        _timerPanel = Instantiate(_prefabTimerPanel);
        _timerPanel.transform.parent = gameObject.transform;
        _timerPanel.transform.localPosition = new Vector3(0, 150, 0);
        _timerPanel.transform.localScale = new Vector3(1, 1, 1);
        _progressRadialHollow = Instantiate(_prefabProgressBar).GetComponent<ProgressBar.ProgressRadialBehaviour>();
        _progressRadialHollow.transform.parent = _timerPanel.transform;
        _progressRadialHollow.GetComponent<RectTransform>().position = gameObject.transform.position;
        _progressRadialHollow.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        _timerPanel.transform.GetChild(1).gameObject.AddComponent<PlayPauseTimer>();
        _timerPanel.transform.GetChild(2).gameObject.AddComponent<RestartTimer>();
    }

    public void destroyTimer()
    {
        Destroy(_progressRadialHollow.gameObject);
    }

    void Update()
    {

        if (!_timerplay)
            return;
        print("still ?");
        Debug.Log("time left before = " + _timeLeft);
        _currenttime += Time.deltaTime;
        _timeLeft -= Time.deltaTime;
       // if (flag) {
       Debug.Log("time left after = " + _timeLeft);
       Debug.Log("time pourcentage = " + _currenttime / _totaltime * 100);
        _progressRadialHollow.Value = _currenttime / _totaltime * 100;
     //   }
        if (_timeLeft < 0)
        {
            _timerplay = false;
            _timerPanel.transform.GetChild(0).GetComponent<Text>().text = "Timer ended !";
        }
        _timerPanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = _timeLeft.ToString();
    }

    public void setTimer(float timer)
    {
        _timeLeft = timer;
        _totaltime = timer;
        _timerPanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = _timeLeft.ToString();

    }

    public bool playOrPauseTimer()
    {
        if (!_timerplay)
            _timerplay = true;
        else
            _timerplay = false;
        print(_timerplay);
        if (!_timerplay)
            print("PAUSE");
        return _timerplay;
    }

    public void restartTimer()
    {
        _timeLeft = _totaltime;
        _currenttime = 0;
        _progressRadialHollow.Value = 0;
        _timerPanel.transform.GetChild(0).transform.GetChild(0).GetComponent<Text>().text = _timeLeft.ToString();

    }
}
