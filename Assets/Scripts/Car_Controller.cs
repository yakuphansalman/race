using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Controller : MonoBehaviour
{
    private Rigidbody _rB;


    [SerializeField] private float _direction;
    [SerializeField] private float _force;
    [SerializeField] private float _rotationForce;
    [SerializeField] private Vector3[] _turnDistances;
    [SerializeField] private Vector3 _activeLength;

    private Vector3 _speed;
    [SerializeField] private GameObject[] _turns;
    [SerializeField] private GameObject _activeTurn;


    private void Start()
    {
        _rB = this.gameObject.GetComponent<Rigidbody>();
        _turns = new GameObject[2];
        _turnDistances = new Vector3[2];
        _turns[0] = GameObject.Find("Turn_1");
        _turns[1] = GameObject.Find("Turn_2");
    }
    private void Update()
    {
        _speed = _rB.velocity;
        SetDirection();
        Accelarate();
        Rotate();
        CheckTurns();
        CreateCentripitalForce();
    }

    private void Rotate()
    {
        if (_rB.velocity.magnitude != 0)
        {
            if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(transform.up * _rotationForce * _speed.magnitude * _direction);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(transform.up * _rotationForce * -1 * _speed.magnitude * _direction);
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

    private void CheckTurns()
    {
        for (int i = 0; i < _turns.Length; i++)
        {
            _turnDistances[i] = (transform.position - _turns[i].transform.position);
            for (int j = 0; j < _turns.Length; j++)
            {
                if (_turnDistances[i].magnitude < _turnDistances[j].magnitude)
                {
                    _activeTurn = _turns[i];
                    _activeLength = _turnDistances[i];
                }
            }
        }
    }
    
    private void CreateCentripitalForce()
    {
        _rB.AddForce(new Vector3(_activeLength.x , 0 , _activeLength.z) * _speed.magnitude * _speed.magnitude * 0.002f / _activeLength.magnitude, ForceMode.Force);
    }
}
