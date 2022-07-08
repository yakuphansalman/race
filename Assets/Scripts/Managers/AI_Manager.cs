using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class AI_Manager : MonoBehaviour
    {
        [SerializeField] private AI_Sensor _sensor;
        [SerializeField] private Path _path;
        private GameObject _car;

        private float _steeringForce;

        #region Commands
        [SerializeField] private float _commAcc = 1, _commSteer, _commBrake;

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
            _car = GameObject.Find("AI_Car");
        }
        private void FixedUpdate()
        {
            //PreventCrashes();
        }


        //private void PreventCrashes()
        //{
        //    for (int i = 0; i < _sensor.RayCount; i++)
        //    {
        //        if (_sensor.ObsDeltaMags[i] == Mathf.Min(_sensor.ObsDeltaMags))
        //        {
        //            if (_sensor.ObsDeltaMags[i] < 2f)
        //            {
        //                if (i == 0)
        //                {
        //                    _commBrake = 5;
        //                    _commSteer = -2;
        //                }
        //                else if (i == 2)
        //                {
        //                    _commBrake = 5;
        //                    _commSteer = 2;
        //                }
        //                else if (i == 1 || i == 4)
        //                {
        //                    _commSteer = 0;
        //                    _commBrake = 100;
        //                }
        //            }
        //            else if (_sensor.ObsDeltaMags[i] < 3f)
        //            {
        //                if (i == 3)
        //                {
        //                    _commBrake = 2;
        //                    _commSteer = -1;
        //                }
        //                if (i == 5)
        //                {
        //                    _commBrake = 2;
        //                    _commSteer = 1;
        //                }
        //            }
        //        }
        //    }
        //}
        public float SteeringForce
        {
            get
            {
                Vector3 relativeVector = _car.transform.InverseTransformPoint(_path.TargetNode);
                _steeringForce = relativeVector.x / relativeVector.magnitude;
                return _steeringForce;
            }
        }
    }
}

