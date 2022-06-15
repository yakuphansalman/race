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

    [SerializeField] private float _direction;
    

    private void Start()
    {
        _rB = this.gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        SetDirection();
        Accelarate();
        Rotate();
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
    private void Accelarate()
    {
        _rB.AddForce(Input_Manager.Instance.i_Vertical * gameObject.transform.forward * _carPrefs.force, ForceMode.Acceleration);
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
