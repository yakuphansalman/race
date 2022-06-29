using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    private Rigidbody _rB;
    [SerializeField] private Car_Preferences_SO _carPrefs = null;
    [SerializeField] private WheelCollider[] _wheels;

    private float _localForce;

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
    }
    private void Update()
    {
        Accelerate();
        Rotate();
        Brake();
    }

    private void Rotate()
    {
        if (Car_Physics.Instance.direction == 1)
        {
            switch (Car_Mechanics.Instance.gear)
            {
                case 1:
                    _localForce = Mathf.Pow(Car_Physics.Instance.speed, 0.9f);
                    break;
                case 2:
                    _localForce = Mathf.Pow(Car_Physics.Instance.speed, 0.4f);
                    break;
                case 3:
                    _localForce = Mathf.Pow(Car_Physics.Instance.speed, 0.1f);
                    break;
                default:
                    _localForce = 0.01f;
                    break;
            }
        }
        if (Car_Physics.Instance.direction == 0)
        {
            _localForce = 0;
        }
        if (Car_Physics.Instance.direction == -1)
        {
            _localForce = Mathf.Pow(Car_Physics.Instance.speed, 0.6f);
        }
    }
    private void Accelerate()
    {
        float force = _carPrefs.force;
        if (Car_Physics.Instance.direction == 1 && Input_Manager.Instance.i_Vertical < 0)
        {
            force = 0;
        }
        else
        {
            force = _carPrefs.force;
        }
        if (Car_Physics.Instance.speed < _carPrefs.a_gearLimits[Car_Mechanics.Instance.gear] && Car_Physics.Instance.speed * Car_Physics.Instance.direction > -_carPrefs.forceLimit / 10)
        {
            foreach (var wheel in _wheels)
            {
                wheel.motorTorque = force * Input_Manager.Instance.i_Vertical;
                if (wheel == _wheels[0] || wheel == _wheels[1])
                {
                    wheel.steerAngle = _carPrefs.rotationForce * Input_Manager.Instance.i_Horizontal;
                }
            }
        }

    }
    private void Brake()
    {
        if (Input_Manager.Instance.i_Vertical < 0 && Car_Physics.Instance.direction == 1)
        {
            foreach (var wheel in _wheels)
            {
                wheel.brakeTorque = _carPrefs.brakeForce * Input_Manager.Instance.i_Vertical;
            }
        }
        
    }
    private void Friction()
    {
        if (Car_Physics.Instance.direction != 0)
        {
            foreach (var wheel in _wheels)
            {
                wheel.motorTorque = -Car_Physics.Instance.direction * _carPrefs.friction;
            }
        }
    }
    

    
    
}
