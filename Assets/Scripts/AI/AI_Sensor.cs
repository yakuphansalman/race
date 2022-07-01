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

        private Vector3[] _posDeltas;

        private void Start()
        {
            _rays = new Ray[_rayCount];
            _hits = new RaycastHit[_rayCount];
            _posDeltas = new Vector3[_rayCount];
        }

        private void FixedUpdate()
        {
            CastRays();
        }

        private void CastRays()
        {
            for (int i = 0; i < _rayCount; i++)
            {
                for (int j = -1; j < 2; j++)//"j" represents left , origin or right
                {
                    for (int k = 0; k < 2; k++)//"k" represents origin or forward
                    {
                        if (k != 0 && j != 0)
                        {
                            _rays[j*k + j + k ] = new Ray(transform.position, transform.right * j + transform.forward * k);
                        }
                    }
                    
                }
                Debug.DrawRay(_rays[i].origin, _rays[i].direction * 30, new Color(0, 255, 0));
            }
        }
    }
}

