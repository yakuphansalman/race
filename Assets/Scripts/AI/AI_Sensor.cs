using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Sensor : AI_Manager
    {
        #region Ray

        [SerializeField] private int _rayCount;

        private Ray[] _rays;
        private RaycastHit[] _hits;

        #endregion

        [SerializeField] private Vector3[] _posDeltas;
        [SerializeField] private float[] _posDeltaMagnitudes;

        private int _activeScenario;

        private void Start()
        {
            _rays = new Ray[_rayCount];
            _hits = new RaycastHit[_rayCount];
            _posDeltas = new Vector3[_rayCount];
            _posDeltaMagnitudes = new float[_rayCount];
        }

        private void FixedUpdate()
        {
            CastRays();
            CheckObstacles();
        }

        private void CastRays()
        {
            //0R,1F,2L,3FR,4O,5FL

            float pi = 180 * Mathf.Deg2Rad;
            for (int i = 0; i < _rayCount; i++)
            {
                _rays[i] = new Ray(transform.position, 200 * (transform.right * Mathf.Round(Mathf.Cos((pi / 2) * (i % 3))) + transform.forward * Mathf.Round(Mathf.Sin(90 * (i % 2)))));
                Physics.Raycast(_rays[i], out _hits[i], 200);

                if (_activeScenario == i)
                {
                    Debug.DrawRay(_rays[i].origin, _rays[i].direction * 200, new Color(0, 255, 0));
                }
                else
                {
                    Debug.DrawRay(_rays[i].origin, _rays[i].direction * 200, new Color(255, 0, 0));
                }

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
                        _posDeltas[i] = _hits[i].collider.transform.localPosition - transform.position;
                        _posDeltaMagnitudes[i] = _posDeltas[i].magnitude;
                    }
                }
                if (i != 4)//4th member of rays is origin which has not direction
                {
                    if (Mathf.Min(_posDeltaMagnitudes) == 0)
                    {

                    }
                    if (Mathf.Min(_posDeltaMagnitudes) == _posDeltaMagnitudes[i])
                    {
                        _activeScenario = i;
                    }
                }
            }
        }
    }
}

