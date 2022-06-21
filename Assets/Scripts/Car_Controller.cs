using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    private Rigidbody _rB;
    [SerializeField] private Car_Preferences_SO _carPrefs = null;

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
        float multiplier;
        if (Car_Physics.Instance.direction == 1)
        {
            if (Car_Physics.Instance.speed < 1 && Car_Physics.Instance.speed > 0.2f)
            {
                multiplier = Mathf.Pow(Car_Physics.Instance.speed + 0.5f, 0.01f);
            }
            if (Car_Physics.Instance.speed >= 1)
            {
                multiplier = 1 / Mathf.Pow(Car_Physics.Instance.speed, 0.7f);
            }
            else
            {
                multiplier = 0;
            }
        }
        if (Car_Physics.Instance.direction == 0)
        {
            multiplier = 0;
        }
        else
        {
            multiplier = Mathf.Pow(Car_Physics.Instance.speed*0.1f, 0.001f);
        }
        transform.Rotate(Input_Manager.Instance.i_Horizontal * transform.up * _carPrefs.rotationForce * Car_Physics.Instance.direction * multiplier);
    }
    private void Accelerate()
    {
        float force = _carPrefs.force;
        if (Car_Physics.Instance.direction == -1)
        {
            force = _carPrefs.force * 0.6f;
        }
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
            _rB.AddForce(Input_Manager.Instance.i_Vertical * gameObject.transform.forward * force * Car_Mechanics.Instance.gearForce, ForceMode.Acceleration);
        }

    }
    private void Brake()
    {
        if (Input_Manager.Instance.i_Vertical < 0)
        {
            if (Car_Physics.Instance.direction == 1)
            {
                _rB.AddForce(Input_Manager.Instance.i_Vertical * gameObject.transform.forward * 0.3f, ForceMode.Force);
            }
        }
        
    }
    

    
    
}
