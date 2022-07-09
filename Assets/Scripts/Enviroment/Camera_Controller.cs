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

    }
    private void FixedUpdate()
    {
        FollowTarget();
        Zoom();
    }
    private void FollowTarget()
    {
        transform.LookAt(_car.transform);
    }
    private void Zoom()
    {
        if (Car_Physics.Instance.Direction > 0)
        {
            float speed = GameObject.Find("Car").GetComponent<Rigidbody>().velocity.magnitude;
            float posZ = Mathf.Lerp(_offset.z, _offset.z - 2f, speed / 15);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, posZ);
            
        }
    }
}
