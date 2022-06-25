using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_Checker : MonoBehaviour
{
    private bool _isTriggered;

    private int _timesTriggered = 0;

    public bool isTriggered => _isTriggered;
    public int timesTriggered => _timesTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car" && !_isTriggered)
        {
            _isTriggered = true;
            _timesTriggered++;
            _isTriggered = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            _isTriggered = false;
        }
    }
}
