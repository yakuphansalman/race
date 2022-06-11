using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    private Rigidbody _rB;


    private float _direction;
    [SerializeField] private float _force;
    [SerializeField] private float _rotationForce;


    private void Start()
    {
        _rB = this.gameObject.GetComponent<Rigidbody>();
    }
    private void Update()
    {
        Accelarate();
        Rotate();
    }

    private void Rotate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.up * _rotationForce * _rB.velocity.magnitude * _direction);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.up * _rotationForce * -1 * _rB.velocity.magnitude * _direction);
        }
    }
    private void Accelarate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _direction = 1;
            _rB.AddForce(gameObject.transform.forward * _direction * _force, ForceMode.Acceleration);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _rB.AddForce(gameObject.transform.forward * _direction * _force, ForceMode.Acceleration);
            _direction = -1;
        }
        
    }
}
