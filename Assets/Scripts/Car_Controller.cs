using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    #region Singleton
    private static Car_Controller instance = null;
    public static Car_Controller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SingletonCC").AddComponent<Car_Controller>();
            }
            return instance;
        }
    }
    #endregion

    private Rigidbody _rB;
    [SerializeField] private Car_Preferences_SO _carPrefs = null;
    private Car_Mechanics _carMechanics;

    private float _direction;
    

    private void Start()
    {
        _rB = this.gameObject.GetComponent<Rigidbody>();
        _carMechanics = this.gameObject.GetComponent<Car_Mechanics>();
    }
    private void Update()
    {
        SetDirection();
        Accelerate();
        Rotate();
        Brake();
    }

    private void Rotate()
    {
        float multiplier;
        if (_rB.velocity.magnitude <= 5)
        {
            multiplier = _rB.velocity.magnitude * 0.01f;
        }
        else
        {
            multiplier = 1 / _rB.velocity.magnitude;
        }
        if (_rB.velocity.magnitude > 0)
        {
            transform.Rotate(Input_Manager.Instance.i_Horizontal * transform.up * _carPrefs.rotationForce * _direction * multiplier);
        }
        

    }
    private void Accelerate()
    {
        float force = _carPrefs.force;
        if (_direction == -1)
        {
            force = _carPrefs.force * 0.6f;
        }
        if (_direction == 1 && Input_Manager.Instance.i_Vertical < 0)
        {
            force = 0;
        }
        else
        {
            force = _carPrefs.force;
        }
        if (_rB.velocity.magnitude < _carPrefs.forceLimit && _rB.velocity.magnitude * _direction > -_carPrefs.forceLimit / 10)
        {
            _rB.AddForce(Input_Manager.Instance.i_Vertical * gameObject.transform.forward * force * _carMechanics.gearForce, ForceMode.Acceleration);
        }

        Debug.Log(force + " " + " direction is " + _direction);
    }
    private void Brake()
    {
        if (Input_Manager.Instance.i_Vertical < 0)
        {
            if (_direction == 1)
            {
                _rB.AddForce(Input_Manager.Instance.i_Vertical * gameObject.transform.forward * 0.3f, ForceMode.Force);
            }
        }
        
    }
    private void SetDirection()
    {
        _direction = 0;
        if ((transform.forward - _rB.velocity).magnitude < (-transform.forward - _rB.velocity).magnitude)
        {
            _direction = 1;
        }
        if ((transform.forward - _rB.velocity).magnitude > (-transform.forward - _rB.velocity).magnitude)
        {
            _direction = -1;
        }
    }

    
    
}
