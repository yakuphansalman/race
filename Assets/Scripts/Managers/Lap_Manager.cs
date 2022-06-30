using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap_Manager : MonoBehaviour
{
    private static protected GameObject _pointZero;

    private static protected bool _isRunning;

    private static protected int _pointSize;
    private static protected int _sectorSize;
    private const int _lapSize = 1;
    private static protected int _currentLap = -1;
    private static protected int _currentSector = -1;


    private static protected float _currentLapTime;
    private static protected float _sectorTimer;
    private static protected float[] _sectorTimes;
    private static protected float[] _lapTimes;

    public float CurrentLapTime => _currentLapTime;
    public int CurrentLap => _currentLap;
    public int LapSize => _lapSize;
    public GameObject PointZero => _pointZero;

    #region Singleton
    private static Lap_Manager instance = null;
    public static Lap_Manager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SingletonLM").AddComponent<Lap_Manager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && this.gameObject.tag != "Point" && this.gameObject.name != "LapManager")
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion
}
