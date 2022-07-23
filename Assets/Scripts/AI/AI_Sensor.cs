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
        [SerializeField] private float _overtakeRange;

        private Ray[] _rays;
        private RaycastHit[] _hits;
        private RaycastHit[] _overtakeHits;

        public RaycastHit[] OvertakeHits => _overtakeHits;

        #endregion


        private GameObject[] _brakePoints;
        private GameObject _carFront;

        private Vector3[] _obsDeltas;

        private float[] _obsDeltaMags;

        public float[] ObsDeltaMags => _obsDeltaMags;
        public GameObject[] BrakePoints => _brakePoints;
        public GameObject CarFront => _carFront;
        public float RayRange => _rayRange;
        public float OvertakeRange => _overtakeRange;
        public int RayCount => _rayCount;

        private void Start()
        {
            _rays = new Ray[_rayCount];
            _hits = new RaycastHit[_rayCount];
            _overtakeHits = new RaycastHit[2];
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
                _rays[i] = new Ray(transform.position + transform.right * Mathf.Sin(pi * i / 2) * 0.5f, transform.forward + transform.right * Mathf.Cos(pi * i/2));
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

