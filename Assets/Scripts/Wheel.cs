using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private void FixedUpdate()
    {
        Rotate();
    }
    private void Rotate()
    {
        Vector3 rotateDirection = new Vector3(0, transform.localPosition.y, 0);
        transform.Rotate(Car_Physics.Instance.speed * rotateDirection * Car_Physics.Instance.direction);
    }
}
