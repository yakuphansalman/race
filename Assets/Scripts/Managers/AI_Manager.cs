using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Manager : MonoBehaviour
    {
        [SerializeField] private AI_Sensor _sensor;

        private int[] _methodPriorities;

        #region Commands
        [SerializeField] private float _commAcc = 0, _commSteer, _commBrake;

        public float CommAcc => _commAcc;
        public float CommSteer => _commSteer;
        public float CommBrake => _commBrake;

        #endregion

        #region Singleton

        private static AI_Manager instance = null;
        public static AI_Manager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameObject("SingletonAIM").AddComponent<AI_Manager>();
                }
                return instance;
            }
        }

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
            }
        }
        #endregion


        private void Start()
        {
            _methodPriorities = new int[3];
        }
        private void FixedUpdate()
        {
            //PreventCrashes();
        }

        #region Prioritized Methods

        private void PreventCrashes()
        {
            
            for (int i = 0; i < _sensor.RayCount; i++)
            {
                if (_sensor.ObsDeltaMags[i] == Mathf.Min(_sensor.ObsDeltaMags))
                {
                    if (_sensor.ObsDeltaMags[i] < 3f)
                    {
                        _methodPriorities[0] = 0;
                        if (i == 0)
                        {
                            _commBrake = 5;
                            _commSteer = -2;
                        }
                        else if (i == 2)
                        {
                            _commBrake = 5;
                            _commSteer = 2;
                        }
                        else if (i == 1 || i == 4)
                        {
                            _commSteer = 0;
                            _commBrake = 10;
                        }
                    }
                    else if (_sensor.ObsDeltaMags[i] < 4f)
                    {
                        _methodPriorities[0] = 0;
                        if (i == 3)
                        {
                            _commBrake = 2;
                            _commSteer = -1;
                        }
                        if (i == 5)
                        {
                            _commBrake = 2;
                            _commSteer = 1;
                        }
                    }
                    else if (i != 0 && i != 1 && i != 2 && i != 3 && i != 4 && i != 5)
                    {
                        _methodPriorities[0]++;
                        _commAcc = 1;
                        _commBrake = 0;
                        _commSteer = 0;
                    }
                }
            }
        }

        private void SyncPath()
        {
            for (int i = 0; i < _sensor.RayCount - 3; i++)
            {
                if (_sensor.PathDeltaMags[i] < 3f)
                {

                }
            }
        }


        #endregion
    }
}

