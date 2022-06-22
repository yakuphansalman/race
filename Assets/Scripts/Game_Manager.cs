using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] _points;
    [SerializeField] private GameObject[] _cars;
    [SerializeField] private Point_Checker[] _checkers;



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
        _cars = GameObject.FindGameObjectsWithTag("Car");
        _points = GameObject.FindGameObjectsWithTag("Point");
        for (int i = 0; i < _points.Length; i++)
        {
            _checkers[i] = _points[i].GetComponent<Point_Checker>();
        }
    }
    private void Update()
    {
        for (int i = 0; i < _cars.Length; i++)
        {
            for (int j = 0; j < _points.Length; j++)
            {
            }
        }
    }
}
