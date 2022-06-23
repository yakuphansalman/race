using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] _cars;
    [SerializeField] private GameObject[] _points;

    [SerializeField] private Point_Checker[] _pointChecker;

    [SerializeField] private bool _onLap;
    [SerializeField] private bool _onSector;

    [SerializeField] private int[] _pointNumbers;
    [SerializeField] private int _currentPoint;
    [SerializeField] private int _nextPoint;
    [SerializeField] private int _lapSize;
    

    [SerializeField] private float[] _sectorTimes;
    [SerializeField] private float[] _lapTimes;
    [SerializeField] private float _totalSectorTime;



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
            _cars = GameObject.FindGameObjectsWithTag("Car");
            _points = GameObject.FindGameObjectsWithTag("Point");

            _pointChecker = new Point_Checker[_points.Length];
            _pointNumbers = new int[_points.Length];
            _sectorTimes = new float[_points.Length - 1];
            _lapTimes = new float[_lapSize + 1];
            _sectorTimes = new float[(_lapSize + 1) * (_points.Length - 1)];

            for (int k = 0; k < _points.Length; k++)
            {
                _pointChecker[k] = _points[k].GetComponent<Point_Checker>();
                if (_points[k].name == "Point_" + k)
                {
                    _pointNumbers[k] = k;
                }
            }
        }

        if (method == "Update")
        {
            for (int i = 0; i < _cars.Length; i++)
            {
                for (int j = 0; j < _points.Length; j++)
                {
                    if (_pointChecker[j].isTriggered)
                    {
                        _currentPoint = _pointNumbers[j];

                        if (_pointNumbers[j] == _points.Length - 1)
                        {
                            _nextPoint = 0;
                        }
                        else
                        {
                            _nextPoint = j + 1;
                        }

                        if (j == 0)
                        {
                            _onLap = true;
                            _onSector = true;
                        }
                        else if (j == _points.Length)
                        {
                            _onLap = false;
                            _onSector = false;
                        }
                    }
                    for (int k = 0; k < _lapSize + 1; k++)
                    {
                        if (_currentPoint == k && k != 3)
                        {
                            if (_onSector)
                            {
                                _sectorTimes[k + 1] = Time.time + _sectorTimes[k - 1];
                            }
                            if (!_onSector)
                            {
                                _sectorTimes[k] = Time.time;
                            }
                        }
                    }
                }
            }
        }
    }

}
