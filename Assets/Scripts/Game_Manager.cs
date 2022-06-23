using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] _cars;
    [SerializeField] private GameObject[] _points;

    [SerializeField] private Point_Checker[] _pointChecker;

    [SerializeField] private int[] _pointNumbers;

    [SerializeField] private float[] _sectorTimes;
    [SerializeField] private float [] _lapTimes;


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
            _lapTimes = new float[_cars.Length];

            for (int k = 1; k < _points.Length + 1; k++)
            {
                _pointChecker[k - 1] = _points[k - 1].GetComponent<Point_Checker>();
                if (_points[k - 1].name == "Point_" + k)
                {
                    _pointNumbers[k - 1] = k;
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
                        switch (j)
                        {
                            case 0:

                                break;
                            case 1:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
    }

}
