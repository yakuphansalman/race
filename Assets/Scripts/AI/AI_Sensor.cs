using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Sensor : MonoBehaviour
    {
        #region Ray

        [SerializeField] private int _rayCount;

        private Ray[] _rays;
        private RaycastHit[] _hits;

        #endregion

        [SerializeField] private Vector3[] _obsDeltas;
        [SerializeField] private Vector3[] _pathDeltas;

        [SerializeField] private float[] _obsDeltaMags;
        [SerializeField] private float[] _pathDeltaMags;
        [SerializeField] private float _activeDelta;

        public float[] ObsDeltaMags => _obsDeltaMags;
        public float[] PathDeltaMags => _pathDeltaMags;
        public float ActiveDelta => _activeDelta;
        public int RayCount => _rayCount;

        private void Start()
        {
            _rays = new Ray[_rayCount];
            _hits = new RaycastHit[_rayCount];
            _obsDeltas = new Vector3[_rayCount];
            _obsDeltaMags = new float[_rayCount];
            _pathDeltas = new Vector3[RayCount];
            _pathDeltaMags = new float[_rayCount];
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
                    _rays[i] = new Ray(transform.position, 200 * (transform.right * Mathf.Round(Mathf.Cos((pi / 2) * ((i-3) % 3))) + transform.forward * Mathf.Round(Mathf.Sin(90 * ((i - 3) % 2)))));
                }
                else
                {
                    _rays[i] = new Ray(transform.position, 200 * (transform.right * Mathf.Round(Mathf.Cos((pi / 2) * (i % 3))) + transform.forward * Mathf.Round(Mathf.Sin(90 * (i % 2)))));
                }
                Physics.Raycast(_rays[i], out _hits[i], 200);


                Debug.DrawRay(_rays[i].origin, _rays[i].direction * 200, new Color(255, 0, 0));

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
                        if (Mathf.Min(_obsDeltaMags) == _obsDeltaMags[i])
                        {
                            _activeDelta = _obsDeltaMags[i];
                        }
                    }
                    if (_hits[i].collider.tag == "Path")
                    {
                        if (i == 0 || i == 2)
                        {
                            _pathDeltas[i] = _hits[i].collider.ClosestPoint(transform.position) - transform.position;
                            _pathDeltaMags[i] = _pathDeltas[i].magnitude;
                            Debug.DrawRay(transform.position, new Vector3(_hits[i].collider.transform.localPosition.x - transform.localPosition.x, 1, 0), new Color(0, 0, 255)); ;
                        }
                    }
                }
            }
        }
    }
}

