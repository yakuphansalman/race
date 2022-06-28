using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Physics : MonoBehaviour
{
    private Rigidbody _rB;
    [SerializeField] private Car_Preferences_SO _carPrefs = null;

    private float _angle;
    private float _directionX , _directionY;
    private float _speed;
    private float _dotX, _dotY;
    private Vector3 _rotationLast, _rotationDelta;

    private Vector3 _currentVector, _previousVector, _centripitalForce;

    public float directionX => _directionX;
    public float directionY => _directionY;
    public float speed => _speed;
    public float dotY => _dotY;

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
    private void CentripitalForce()
    {
        if (Input_Manager.Instance.i_Horizontal == 0)
        {
            _angle = 0;
        }
        _previousVector = _currentVector;
        _currentVector = transform.forward;
        _angle = Vector3.Angle(_previousVector, _currentVector);

        _centripitalForce = transform.right * Mathf.Sqrt(_angle) * _rB.velocity.magnitude * _rB.velocity.magnitude * _carPrefs.cMultiplier;

        _rB.AddForce(-_centripitalForce * Input_Manager.Instance.i_Horizontal, ForceMode.Force);
    }
    private void SetDirection()
    {
        _dotX = Vector3.Dot(transform.forward, _rB.velocity);

        if (_dotX < -0.5f)
        {
            _directionX = -1;
        }
        if (_dotX >= -0.5f && _dotX <= 0.5f)
        {
            _directionX = 0;
        }
        if (_dotX > 0.5f)
        {
            _directionX = 1;
        }

        _rotationDelta = transform.localRotation.eulerAngles - _rotationLast;
        _rotationLast = transform.localRotation.eulerAngles;
        _directionY = angularSpeed.normalized.y;

    }
    public Vector3 angularSpeed
    {
        get
        {
            return _rotationDelta;
        }
    }
}
