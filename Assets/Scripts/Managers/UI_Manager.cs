using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class UI_Manager : MonoBehaviour
    {
        [SerializeField] private Text _speed_Car, _lapTime_Car, _laps_Car;
        [SerializeField] private Text _speed_AI_01;

        private void FixedUpdate()
        {
            ShowSpeed();
            ShowLaps();
            ShowLapTimes();
        }
        private void ShowSpeed()
        {
            float speedCar = GameObject.Find("Car").GetComponent<Rigidbody>().velocity.magnitude;
            _speed_Car.text = ((int)(speedCar * 10)).ToString() + " " + "kmph";

            float speedAI01 = GameObject.Find("AI_Car").GetComponent<Rigidbody>().velocity.magnitude;
            _speed_AI_01.text = ((int)(speedAI01 * 10)).ToString() + " " + "kmph";
        }
        private void ShowLaps()
        {
            if (Lap_Manager.Instance.CurrentLap < 0)
            {
                _laps_Car.text = 0 + " / " + Lap_Manager.Instance.LapSize.ToString();
            }
            else
            {
                _laps_Car.text = Lap_Manager.Instance.CurrentLap.ToString() + " / " + Lap_Manager.Instance.LapSize.ToString();
            }
        }
        private void ShowLapTimes()
        {
            float currentLapTime = Lap_Manager.Instance.CurrentLapTime;
            _lapTime_Car.text = Mathf.Floor(currentLapTime / 60).ToString() + " : " + Mathf.Floor(currentLapTime % 60).ToString() + " : " + Mathf.Floor(currentLapTime * 10 % 10).ToString();
        }
    }
}
