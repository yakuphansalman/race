using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AI_Preferences" , menuName = "AI_Settings")]
public class AI_Preferences_SO : ScriptableObject
{
    [SerializeField] private float _speed, _rotationSpeed;

    public float Speed => _speed;
    public float RotationSpeed => _rotationSpeed;
}
