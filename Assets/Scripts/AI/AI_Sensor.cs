using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Sensor : MonoBehaviour
    {
        #region Ray

        [SerializeField] private int _rayCount;

        [SerializeField] private float _rayRange;

        private Ray[] _rays;
        private Ray _antiBrakeRay;
        private RaycastHit[] _hits;
        private RaycastHit _antiBrakeHit;

        #endregion

        private GameObject[] _brakePoints;

        private Vector3[] _obsDeltas;
        private Vector3 _pathDelta;

        private float[] _obsDeltaMags;

        public float[] ObsDeltaMags => _obsDeltaMags;
        public Vector3 PathDelta => _pathDelta;
        public GameObject[] BrakePoints => _brakePoints;
        public float RayRange => _rayRange;
        public int RayCount => _rayCount;


        private void Start()
        {
            _rays = new Ray[_rayCount];
            _hits = new RaycastHit[_rayCount];
            _obsDeltas = new Vector3[_rayCount];
            _obsDeltaMags = new float[_rayCount];
            _brakePoints = new GameObject[GameObject.FindGameObjectsWithTag("Brake Point").Length];
            for (int i = 0; i < _brakePoints.Length; i++)
            {
                _brakePoints[i] = GameObject.FindGameObjectsWithTag("Brake Point")[i];
            }
        }

        private void FixedUpdate()
        {
            CastRays();
            CheckObstacles();
        }

        private void CastRays()
        {
            //Linear => 1R , 3L
            //Cross => 0FR , 2FL

            float pi = 180 * Mathf.Deg2Rad;
            for (int i = 0; i < _rayCount; i++)
            {
                float carWidth = 1f;
                _rays[i] = new Ray(transform.position + new Vector3(carWidth * Mathf.Sin(pi * i/2), 0, 0), transform.forward + transform.right * Mathf.Cos(pi * i/2));

                Physics.Raycast(_rays[i], out _hits[i], _rayRange);
                Debug.DrawRay(_rays[i].origin, _rays[i].direction * _rayRange, new Color(255, 0, 0));
            }
        }

        private void CheckObstacles()
        {
            for (int i = 0; i < _rayCount; i++)
            {
                if (_hits[i].collider != null)
                {
                    if (_hits[i].collider.tag == "Obstacle" || _hits[i].collider.tag == "Boundary")
                    {
                        _obsDeltas[i] = _hits[i].collider.ClosestPoint(transform.position) - transform.position;
                        _obsDeltaMags[i] = _obsDeltas[i].magnitude;
                    }
                }
            }
        }
        private bool IsOnBrakePoint()
        {
            for (int i = 0; i < _brakePoints.Length; i++)
            {
                if ((_brakePoints[i].transform.position - transform.position).magnitude < _brakePoints[i].GetComponent<Brake_Point>().AreaRadius)
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsObsDetected()
        {
            for (int i = 0; i < _rayCount; i++)
            {
                if (_hits[i].collider != null)
                {
                    if (_hits[i].collider.tag == "Obstacle" || _hits[i].collider.tag == "Boundary")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        
        public bool OnBrakePoint => IsOnBrakePoint();
        public bool ObsDetected => IsObsDetected();
    }
}

