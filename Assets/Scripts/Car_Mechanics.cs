using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Mechanics : MonoBehaviour
{
    private Rigidbody _rB;

    private int _gear;

    
    [SerializeField] private float[] _gearLimits;
    [SerializeField] private float _gearForce;
    private const float _gearMultiplier = 1.1f;
    private float _speed;

    public float gearForce => _gearForce;
    public int gear => _gear;

    private void Start()
    {
        _rB = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ManageGears();
    }
    private void ManageGears()
    {
        _speed = _rB.velocity.magnitude;
        for (int i = 0; i < _gearLimits.Length -1; i++)
        {
            if (_speed > _gearLimits[i] && _speed <= _gearLimits[i + 1])
            {
                _gearForce = Mathf.Pow(_gearMultiplier, i+1);
                _gear = i+1;
            }
        }
    }
}
