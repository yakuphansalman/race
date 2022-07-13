using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brake_Point : MonoBehaviour
{
    [SerializeField] float _speedLimit;
    [SerializeField] float _areaRadius;
    public float SpeedLimit => _speedLimit;
    public float AreaRadius => _areaRadius;
}
