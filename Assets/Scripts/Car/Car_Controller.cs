using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    [SerializeField] private Car_Preferences_SO _carPrefs = null;
    [SerializeField] private WheelCollider[] _wheels;

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

    
    private void Update()
    {
        foreach (var wheelCollider in _wheels)
        {
            Accelerate(wheelCollider);
            Rotate(wheelCollider);
            Brake(wheelCollider);
        }
    }

    private void Rotate(WheelCollider wheel)
    {
        for (int i = 0; i < 2; i++)
        {
            if (wheel == _wheels[i])
            {
                wheel.steerAngle = _carPrefs.AngularForce * Input_Manager.Instance.I_Horizontal / Mathf.Pow(Car_Mechanics.Instance.GearForce, Mathf.Sqrt(Car_Physics.Instance.Speed));
            }
        }

    }
    private void Accelerate(WheelCollider wheel)
    {
        wheel.motorTorque = _carPrefs.Force * Input_Manager.Instance.I_Vertical * Car_Mechanics.Instance.GearForce;
    }
    private void Brake(WheelCollider wheel)
    {
        if (Input_Manager.Instance.I_Vertical < 0)
        {
            wheel.motorTorque = _carPrefs.BrakeForce * -1;
        }
    }
}
