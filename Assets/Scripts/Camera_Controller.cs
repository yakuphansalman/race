using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    private GameObject _car;

    [SerializeField] private Vector3 _offset;

    private void Start()
    {
        _car = GameObject.Find("Car");
        transform.localPosition = _offset;
    }
    private void Update()
    {
        FollowTarget();
    }
    private void FixedUpdate()
    {
        Zoom();
        LerpTurns();
    }
    private void FollowTarget()
    {
        transform.LookAt(_car.transform);
    }
    private void Zoom()
    {
        if (Car_Physics.Instance.directionX > 0)
        {
            float posZ = Mathf.Lerp(_offset.z, _offset.z - 0.5f, Car_Physics.Instance.speed / 15);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, posZ);
        }
    }
    private void LerpTurns()
    {
        float posX;
        posX = Mathf.Lerp(_offset.x, _offset.x - 1.5f * Car_Physics.Instance.directionY, Car_Physics.Instance.angularSpeed.magnitude * 1.5f);
        transform.localPosition = new Vector3(posX, transform.localPosition.y, transform.localPosition.z);
    }
}
