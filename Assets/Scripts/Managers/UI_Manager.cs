using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class UI_Manager : MonoBehaviour
    {
        [SerializeField] private Text _t_speed, _t_lapTime, _t_laps;

        private void FixedUpdate()
        {
            ShowSpeed();
            ShowLaps();
            ShowLapTimes();
        }
        private void ShowSpeed()
        {
            float speed = GameObject.Find("Car").GetComponent<Rigidbody>().velocity.magnitude;
            _t_speed.text = ((int)(speed * 10)).ToString() + " " + "kmph";
        }
        private void ShowLaps()
        {
            if (Lap_Manager.Instance.CurrentLap < 0)
            {
                _t_laps.text = 0 + " / " + Lap_Manager.Instance.LapSize.ToString();
            }
            else
            {
                _t_laps.text = Lap_Manager.Instance.CurrentLap.ToString() + " / " + Lap_Manager.Instance.LapSize.ToString();
            }
        }
        private void ShowLapTimes()
        {
            float currentLapTime = Lap_Manager.Instance.CurrentLapTime;
            _t_lapTime.text = Mathf.Floor(currentLapTime / 60).ToString() + " : " + Mathf.Floor(currentLapTime % 60).ToString() + " : " + Mathf.Floor(currentLapTime * 10 % 10).ToString();
        }
    }
}
