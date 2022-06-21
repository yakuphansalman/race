using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Mechanics : MonoBehaviour
{
    private Rigidbody _rB;


    [SerializeField] private bool _canAcc = true;

    private int _gear;
    
    [SerializeField] private float[] _gearLimits;
    [SerializeField] private float _gearForce;
    private const float _gearMultiplier = 1.1f;
    private float _speed;

    public float gearForce => _gearForce;
    public float[] gearLimits => _gearLimits;
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
            if (_speed > _gearLimits[i] && _speed <= _gearLimits[i + 1] && _canAcc)
            {
                _gearForce = Mathf.Pow(_gearMultiplier, i+1);
                StartCoroutine(GearUpAndDown(i + 1));
            }
        }
    }
    private IEnumerator GearUpAndDown(int gear)
    {
        _canAcc = false;
        _gear = gear;
        yield return new WaitForSeconds(0.5f);
        _canAcc = true;
    }
}
