using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    private Rigidbody _rB;


    [SerializeField] private float _direction;
    [SerializeField] private float _force;
    [SerializeField] private float _rotationForce;
    [SerializeField] private float _angle;
    [SerializeField] private float _cMultiplier;

    private Vector3 _speed;
    [SerializeField] private Vector3 _currentVector;
    [SerializeField] private Vector3 _previousVector;
    [SerializeField] private Vector3 _centripitalForce;





    private void Start()
    {
        _rB = this.gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _speed = _rB.velocity;
        SetDirection();
        Accelarate();
        Rotate();
        CreateCentripitalForce();
    }

    private void Rotate()
    {
        float multiplier;
        if (_speed.magnitude <= 5)
        {
            multiplier = _speed.magnitude * 0.01f;
        }
        else
        {
            multiplier = 1 / _speed.magnitude;
        }
        if (_speed.magnitude > 0)
        {
            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(transform.up * _rotationForce * _direction * multiplier);
            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(transform.up * _rotationForce * -1 * _direction * multiplier);
            }
        }
        

    }
    private void Accelarate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rB.AddForce(gameObject.transform.forward  * _force, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rB.AddForce(-gameObject.transform.forward * _force * 0.5f, ForceMode.Acceleration);
        }
    }
    private void SetDirection()
    {
        _direction = 0;
        if ((transform.forward - _speed).magnitude < (-transform.forward - _speed).magnitude)
        {
            _direction = 1;
        }
        if ((transform.forward - _speed).magnitude > (-transform.forward - _speed).magnitude)
        {
            _direction = -1;
        }
    }

    
    private void CreateCentripitalForce()
    {
        if (!Input.GetKey(KeyCode.D) || !Input.GetKey(KeyCode.A))
        {
            _angle = 0;
        }
        _previousVector = _currentVector;
        _currentVector = transform.forward;
        _angle = Vector3.Angle(_previousVector, _currentVector);

        _centripitalForce = transform.right * _angle * _speed.magnitude * _speed.magnitude * _cMultiplier;
        if (Input.GetKey(KeyCode.A))
        {
            _rB.AddForce(_centripitalForce, ForceMode.Force);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _rB.AddForce(-_centripitalForce, ForceMode.Force);
        }
        Debug.Log(_centripitalForce.magnitude);
    }
}
