using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race_Manager : MonoBehaviour
{
    [SerializeField] private GameObject[] _carPlacements;


    public GameObject[] CarPlacements => _carPlacements;


    #region Singleton

    private static Race_Manager instance = null;
    public static Race_Manager Instance
    {
        get
        {
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
        _carPlacements = GameObject.FindGameObjectsWithTag("Car");
    }
    private void Update()
    {
        ManagePlaces();
    }
    private void ManagePlaces()
    {
        for (int i = 0; i < _carPlacements.Length; i++)
        {
            for (int j = 0; j < _carPlacements.Length; j++)
            {
                if (i != j)
                {
                    Vector3 relativeVector = _carPlacements[i].transform.InverseTransformPoint(_carPlacements[j].transform.position);
                    if (relativeVector.z < 0)
                    {
                        GameObject tempCar = _carPlacements[j];
                        _carPlacements[j] = _carPlacements[i];
                        _carPlacements[i] = tempCar;
                    }
                }
            }
        }
    }
}
