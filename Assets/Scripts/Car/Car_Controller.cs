using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Car_Controller : MonoBehaviour
{
    [SerializeField] private protected Car_Preferences_SO _carPrefs = null;
    [SerializeField] private protected WheelCollider[] _wheels;

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
        instance = this;
    }
    #endregion

    
    private void Update()
    {
        Control();
    }

    protected abstract float Steer();
    protected abstract float Accelerate();
    protected abstract float Brake();
    protected virtual float AntiSteer(float speed)
    {
        return Mathf.Pow(1.55f, Mathf.Sqrt(speed));
    }
    protected abstract float SlipStreamSpeed();

    private void Control()
    {
        foreach (var wheelCollider in _wheels)
        {
            if (wheelCollider == _wheels[0] || wheelCollider == _wheels[1])
            {
                wheelCollider.steerAngle = Steer();
            }
            wheelCollider.motorTorque = Accelerate();
            wheelCollider.brakeTorque = Brake();
        }
    }
}
