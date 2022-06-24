using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [Header("Points")]

    [SerializeField] private GameObject _pointStart, _pointFinish;
    [SerializeField] private GameObject[] _points;
    [SerializeField] private Point_Checker[] _pointChecker;

    [Header("Info")]

    private bool _isStarted, _isFinished,_isLapped;
    [SerializeField] private int _lapSize;
    private int _timesLapped = 0;
    private float _timer, _realizedTime;
    [SerializeField] private float[] _lapTimes;

    #region Singleton
    private static Game_Manager instance = null;
    public static Game_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SingletonGM").AddComponent<Game_Manager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion


    private void Start()
    {
        LapTime("Start");
    }
    private void Update()
    {
        LapTime("Update");
    }


    private void LapTime(string method)
    {
        if (method == "Start")
        {
            _lapTimes = new float[_lapSize];
        }

        if (method == "Update")
        {
            for (int i = 0; i < _points.Length; i++)
            {
                if (_pointChecker[i].isTriggered && _points[i] == _pointFinish && _timesLapped != _lapSize && _isStarted)
                {
                    _lapTimes[_timesLapped] = _timer;
                    _timesLapped++;
                    _timer = 0;
                }
                if (_pointChecker[i].isTriggered && _points[i] == _pointStart)
                { 
                    _isStarted = true;
                    _isFinished = false;
                }
                if (_pointChecker[i].isTriggered && _points[i] == _pointFinish && _timesLapped == _lapSize && !_isFinished)
                {
                    _isStarted = false;
                    _isFinished = true;
                }
            }
            if (_isStarted)
            {
                _timer += Time.deltaTime;
                
            }
            if (_isFinished)
            {
                _timer = 0;
            }
            Debug.Log(_timer + " " + _timesLapped);
        }
    }

}
