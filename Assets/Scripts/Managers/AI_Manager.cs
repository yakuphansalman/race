using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Manager : MonoBehaviour
    {
        [SerializeField] private AI_Sensor _sensor;

        #region Commands
        [SerializeField] private int _commAcc = 1, _commSteer, _commBrake;

        public int CommAcc => _commAcc;
        public int CommSteer => _commSteer;
        public int CommBrake => _commBrake;

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


        private void FixedUpdate()
        {
            PreventCrashes();
        }

        #region Prioritized Methods

        private void PreventCrashes()
        {
            for (int i = 0; i < _sensor.RayCount; i++)
            {
                if (_sensor.DeltaMags[i] < 4f)
                {
                    if (i == 0)
                    {
                        _commBrake = 0;
                        _commSteer = -1;
                    }
                    else if (i == 1 || i== 4)
                    {
                        _commBrake = 1;
                        _commAcc = 0;
                    }
                    else if (i == 2)
                    {
                        _commBrake = 0;
                        _commSteer = 1;
                    }
                    else if (i == 3)
                    {
                        _commBrake = 1;
                        _commAcc = 0;
                        _commSteer = -1;
                    }
                    else if (i == 5)
                    {
                        _commBrake = 1;
                        _commAcc = 0;
                        _commSteer = 1;
                    }
                }
                else
                {
                    foreach (var delta in _sensor.DeltaMags)
                    {
                        if (delta !< 4f)
                        {

                        }
                    }
                }
            }
        }


        #endregion
    }
}

