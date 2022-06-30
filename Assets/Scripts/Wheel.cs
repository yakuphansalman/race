using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField] private Transform[] _wheelPoses;
    private void FixedUpdate()
    {
        Rotate();
        Steer();
    }
    private void Rotate()
    {
        Vector3 rotateDirection = new Vector3(0, transform.localPosition.y, 0);
        transform.Rotate(Car_Physics.Instance.Speed * rotateDirection * Car_Physics.Instance.Direction);
    }
    private void Steer()
    {
        
    }
}
