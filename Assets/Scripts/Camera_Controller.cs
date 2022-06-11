using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private GameObject _car;

    [SerializeField] private Vector3 _offset;



    private void Start()
    {
        _car = GameObject.Find("Car");
    }
    private void Update()
    {
        transform.position = _car.transform.position + _offset;
    }
}
