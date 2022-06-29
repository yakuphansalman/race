using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap_Checker : Lap_Manager
{
    private void Start()
    {
        SectorTimer("Start");
    }
    private void Update()
    {
        SectorTimer("Update");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            if (this.gameObject == PointZero)
            {
                _isRunning = true;
                _currentLap++;
            }
            if (_currentLap == LapSize)
            {
                _isRunning = false;
            }
            _currentSector++;
            _sectorTimer = 0;
        }
    }

    private void SectorTimer(string method)
    {
        if (method == "Start")
        {
            _pointSize = GameObject.FindGameObjectsWithTag("Point").Length;
            _sectorSize = LapSize * _pointSize;
            _sectorTimes = new float[_sectorSize];
            _lapTimes = new float[LapSize];
            _pointZero = GameObject.Find("Point_0");
        }
        if (method == "Update")
        {
            if (_isRunning)
            {
                _sectorTimer += Time.deltaTime;
                for (int i = 0; i < _sectorSize; i++)
                {
                    if (i == _currentSector)
                    {
                        _sectorTimes[i] = _sectorTimer;
                    }
                    if (i % _pointSize == 0)
                    {
                        _lapTimes[i/_pointSize] = _sectorTimes[i] + _sectorTimes[i + 1] + _sectorTimes[i + 2];
                        if (i/_pointSize == _currentLap)
                        {
                            _currentLapTime = _lapTimes[i / _pointSize];
                        }
                    }
                }
            }
        }
    }
}
