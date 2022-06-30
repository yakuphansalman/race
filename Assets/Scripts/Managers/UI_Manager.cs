using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text _t_gear, _t_speed, _t_lapTime,_t_laps;

    private void FixedUpdate()
    {
        ShowGear();
        ShowSpeed();
        ShowLaps();
        ShowLapTimes();
    }
    private void ShowGear()
    {
        if (Car_Physics.Instance.Direction == 0)
        {
            _t_gear.text = "0";
        }
        if (Car_Physics.Instance.Direction == -1 && Input_Manager.Instance.I_Vertical < 0)
        {
            _t_gear.text = "R";
        }
        else if (Car_Physics.Instance.Direction == 1)
        {
            _t_gear.text = Car_Mechanics.Instance.Gear.ToString();
        }
    }
    private void ShowSpeed()
    {
        _t_speed.text = ((int)(Car_Physics.Instance.Speed * 10)).ToString() + " " + "kmph";
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
