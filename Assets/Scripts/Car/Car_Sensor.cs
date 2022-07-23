using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Sensor : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;

    private Ray[] _horizontalSensors;
    private Ray[] _verticalSensors;
    private RaycastHit[] _horizontalHits;
    private RaycastHit[] _verticalHits;
    public RaycastHit[] HorizontalHits => _horizontalHits;
    public RaycastHit[] VerticalHits => _verticalHits;

    private void Start()
    {
        _horizontalSensors = new Ray[2];
        _horizontalHits = new RaycastHit[2];
        _verticalSensors = new Ray[3];
        _verticalHits = new RaycastHit[3];
    }
    private void FixedUpdate()
    {
        CastRays();
    }
    private void CastRays()
    {
        CastHorizontalSensors();
        CastVerticalSensors();
    }
    private void CastHorizontalSensors()
    {
        //0R , 1L
        float pi = 180 * Mathf.Deg2Rad;

        float range = 1000f;
        for (int i = 0; i < 2; i++)
        {
            _horizontalSensors[i] = new Ray(transform.position + _offset, transform.right * Mathf.Cos(pi * i));
            Physics.Raycast(_horizontalSensors[i], out _horizontalHits[i], range);

            Debug.DrawRay(_horizontalSensors[i].origin, _horizontalSensors[i].direction * range, new Color(255, 255, 0));
        }
    }
    private void CastVerticalSensors()
    {
        //0FR ,1F ,2FL
        float pi = 180 * Mathf.Deg2Rad;

        float range = 1000f;
        for (int i = 0; i < 3; i++)
        {
            _verticalSensors[i] = new Ray(transform.position + _offset + transform.right * Mathf.Cos(pi*i/2) * 0.75f, transform.forward);
            Physics.Raycast(_verticalSensors[i], out _verticalHits[i], range);

            Debug.DrawRay(_verticalSensors[i].origin, _verticalSensors[i].direction * range, new Color(255, 255, 0));
        }
    }
}
