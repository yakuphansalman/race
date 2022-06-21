using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    private Car_Mechanics _carMechanics;
    private Rigidbody _rb;
    private GameObject _car;

    [SerializeField] private Text _t_gear;
    [SerializeField] private Text _t_speed;

    private void Start()
    {
        _car = GameObject.Find("Car");
        _rb = _car.GetComponent<Rigidbody>();
        _carMechanics = _car.GetComponent<Car_Mechanics>();
    }
    private void Update()
    {
        _t_gear.text = _carMechanics.gear.ToString();
        _t_speed.text = ((int)(_rb.velocity.magnitude * 10)).ToString() + " " + "kmph";
    }
}
