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

        private Vector3[] _obsDeltas;
        private Vector3[] _brakePoints;
        private Vector3 _pathDelta;

        private float[] _obsDeltaMags;
        private float _activeDelta;
        private float _dot;
        private float _forwardDistance;
        [SerializeField] private float _brakeAreaRadius;

        public float[] ObsDeltaMags => _obsDeltaMags;
        public Vector3 PathDelta => _pathDelta;
        public float ActiveDelta => _activeDelta;
        public float Dot => _dot;
        public float ForwardDistance => _forwardDistance;
        public float RayRange => _rayRange;
        public int RayCount => _rayCount;


        private void Start()
        {
            _rays = new Ray[_rayCount];
            _hits = new RaycastHit[_rayCount];
            _obsDeltas = new Vector3[_rayCount];
            _obsDeltaMags = new float[_rayCount];
            _brakePoints = new Vector3[GameObject.FindGameObjectsWithTag("Brake Point").Length];
            for (int i = 0; i < _brakePoints.Length; i++)
            {
                _brakePoints[i] = GameObject.FindGameObjectsWithTag("Brake Point")[i].transform.position;
            }
        }

        private void FixedUpdate()
        {
            CastRays();
            CheckObstacles();
        }

        private void CastRays()
        {
            //0R,1F,2L,3FR,4F,5FL

            float pi = 180 * Mathf.Deg2Rad;
            for (int i = 0; i < _rayCount; i++)
            {
                if (i == 4)
                {
                    _rays[i] = new Ray(transform.position, _rayRange * (transform.right * Mathf.Round(Mathf.Cos((pi / 2) * ((i-3) % 3))) + transform.forward * Mathf.Round(Mathf.Sin(90 * ((i - 3) % 2)))));
                }
                else
                {
                    _rays[i] = new Ray(transform.position, _rayRange * (transform.right * Mathf.Round(Mathf.Cos((pi / 2) * (i % 3))) + transform.forward * Mathf.Round(Mathf.Sin(90 * (i % 2)))));
                }
                Physics.Raycast(_rays[i], out _hits[i], _rayRange);


                Debug.DrawRay(_rays[i].origin, _rays[i].direction * _rayRange, new Color(255, 0, 0));

            }

            float antiBrakeRange = 150f;
            _antiBrakeRay = new Ray(transform.position, transform.forward * antiBrakeRange);
            Physics.Raycast(_antiBrakeRay, out _antiBrakeHit, antiBrakeRange);
            Debug.DrawRay(_antiBrakeRay.origin, _antiBrakeRay.direction * antiBrakeRange, new Color(0, 255, 0));
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
                        if (Mathf.Min(_obsDeltaMags) == _obsDeltaMags[i])
                        {
                            _activeDelta = _obsDeltaMags[i];
                        }
                    }
                }
            }
            if (_antiBrakeHit.collider != null)
            {
                if (_antiBrakeHit.collider.tag == "Obstacle" || _antiBrakeHit.collider.tag == "Boundary")
                {
                    _forwardDistance = (_antiBrakeHit.collider.transform.position - transform.position).magnitude/150f;
                }
            }
            else
            {
                _forwardDistance = 1;
            }
        }
        private bool IsOnBrakePoint()
        {
            for (int i = 0; i < _brakePoints.Length; i++)
            {
                if ((_brakePoints[i] - transform.position).magnitude <_brakeAreaRadius)
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

