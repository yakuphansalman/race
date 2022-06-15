using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Physics : MonoBehaviour
{
    private Rigidbody _rB;
    [SerializeField] private Car_Preferences_SO _carPrefs = null;

    [SerializeField] private float _angle;

    [SerializeField] private Vector3 _currentVector;
    [SerializeField] private Vector3 _previousVector;
    [SerializeField] private Vector3 _centripitalForce;

    private void Start()
    {
        _rB = gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        CentripitalForce();
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

        _centripitalForce = transform.right * _angle * _rB.velocity.magnitude * _rB.velocity.magnitude * _carPrefs.cMultiplier;

        _rB.AddForce(-_centripitalForce * Input_Manager.Instance.i_Horizontal, ForceMode.Force);
        Debug.Log(_centripitalForce.magnitude);
    }
}
