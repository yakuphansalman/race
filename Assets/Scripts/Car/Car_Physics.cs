using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Physics : MonoBehaviour
{
    private Rigidbody _rB;

    [SerializeField] private Transform _centerOfMass;

    private float _angle;
    private float _direction;
    private float _speed;
    private float _dotX;

    private Vector3 _rotationLast, _rotationDelta;
    private Vector3 _currentVector, _previousVector, _centripitalForce;

    public float Direction => _direction;
    public float Speed => _speed;

    #region Singleton

    private static Car_Physics instance = null;
    public static Car_Physics Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("SingletonCP").AddComponent<Car_Physics>();
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
        _rB = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _speed = _rB.velocity.magnitude;
        CentripitalForce();
        SetDirection();
    }
    private void FixedUpdate()
    {
        _rB.centerOfMass = _centerOfMass.localPosition;
        SetAngularDirection();
    }
    private void CentripitalForce()
    {
        if (Input_Manager.Instance.I_Horizontal == 0)
        {
            _angle = 0;
        }
        _previousVector = _currentVector;
        _currentVector = transform.forward;
        _angle = Vector3.Angle(_previousVector, _currentVector);

        _centripitalForce = transform.right * Mathf.Sqrt(_angle) * _rB.velocity.magnitude * _speed;

        _rB.AddForce(-_centripitalForce * Input_Manager.Instance.I_Horizontal, ForceMode.Force);
    }
    private void SetDirection()
    {
        _dotX = Vector3.Dot(transform.forward, _rB.velocity);

        if (_dotX < -0.5f)
        {
            _direction = -1;
        }
        if (_dotX >= -0.5f && _dotX <= 0.5f)
        {
            _direction = 0;
        }
        if (_dotX > 0.5f)
        {
            _direction = 1;
        }
    }
    private void SetAngularDirection()
    {
        _rotationDelta = transform.localRotation.eulerAngles - _rotationLast;
        _rotationLast = transform.localRotation.eulerAngles;
    }
    public float AngularSpeed
    {
        get
        {
            return _rotationDelta.normalized.y;
        }
    }
}
