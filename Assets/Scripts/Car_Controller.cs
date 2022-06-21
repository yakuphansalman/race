using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    

    private Rigidbody _rB;
    [SerializeField] private Car_Preferences_SO _carPrefs = null;
    private Car_Mechanics _carMechanics;

    private float _direction;
    private float _speed;

    public float direction => _direction;

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
        _rB = this.gameObject.GetComponent<Rigidbody>();
        _carMechanics = this.gameObject.GetComponent<Car_Mechanics>();
    }
    private void Update()
    {
        _speed = _rB.velocity.magnitude;
        SetDirection();
        Accelerate();
        Rotate();
        Brake();
    }

    private void Rotate()
    {
        float multiplier;
        if (_speed >= 0 && _speed < 1)
        {
            multiplier = Mathf.Pow(_speed, 1 / 10);
        }
        else
        {
            multiplier = 1 / Mathf.Pow(_speed, 1/2);
        }
        transform.Rotate(Input_Manager.Instance.i_Horizontal * transform.up * _carPrefs.rotationForce * _direction * multiplier);
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
        if (_speed < _carMechanics.gearLimits[_carMechanics.gear] && _speed * _direction > -_carPrefs.forceLimit / 10)
        {
            _rB.AddForce(Input_Manager.Instance.i_Vertical * gameObject.transform.forward * force * _carMechanics.gearForce, ForceMode.Acceleration);
        }

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
        if ((_rB.velocity - transform.forward).magnitude > (_rB.velocity + transform.forward).magnitude)
        {
            _direction = -1;
        }
        else
        {
            _direction = 1;
        }

        Debug.Log(_direction + " ");
    }

    
    
}
