using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [Header("Points")]

    [SerializeField] private GameObject _pointZero;
    [SerializeField] private GameObject[] _points;
    [SerializeField] private Point_Checker[] _pointChecker;

    [Header("Info")]

    private bool _isStarted, _isFinished;
    [SerializeField] private int _lapSize;
    private int _currentLap = -1;
    private float _currentLapTime;
    [SerializeField] private float[] _lapTimes;

    public float currentLapTime => _currentLapTime;
    public int currentLap => _currentLap;
    public int lapSize => _lapSize;

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
            if (_pointChecker[0].timesTriggered > 0)
            {
                _isStarted = true;
                _isFinished = false;
                if (_currentLap != _lapSize && _isStarted)
                {
                    _lapTimes[_currentLap] = _currentLapTime;
                    _currentLap = _pointChecker[0].timesTriggered;
                    _currentLapTime = 0;
                }
                else if (_currentLap == _lapSize && !_isFinished)
                {
                    _isStarted = false;
                    _isFinished = true;
                }

            }
            if (_isStarted)
            {
                _currentLapTime += Time.deltaTime;
            }
            if (_isFinished)
            {
                _currentLapTime = 0;
            }
            Debug.Log(_currentLapTime);
        }
    }

}
