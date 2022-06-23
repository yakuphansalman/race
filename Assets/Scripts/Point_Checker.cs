using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_Checker : MonoBehaviour
{
    [SerializeField] private bool _isTriggered;

    public bool isTriggered => _isTriggered;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Car")
        {
            _isTriggered = true;
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
