using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text _t_gear, _t_speed, _t_lapTime,_t_laps;

    private void Update()
    {
        ShowGear();
        ShowSpeed();
        ShowLaps();
        ShowLapTimes();
    }
    private void ShowGear()
    {
        if (Car_Physics.Instance.direction == 0)
        {
            _t_gear.text = "0";
        }
        if (Car_Physics.Instance.direction == -1 && Input_Manager.Instance.i_Vertical < 0)
        {
            _t_gear.text = "R";
        }
        else if (Car_Physics.Instance.direction == 1)
        {
            _t_gear.text = Car_Mechanics.Instance.gear.ToString();
        }
    }
    private void ShowSpeed()
    {
        _t_speed.text = ((int)(Car_Physics.Instance.speed * 10)).ToString() + " " + "kmph";
    }
    private void ShowLaps()
    {
        if (Game_Manager.Instance.currentLap < 0)
        {
            _t_laps.text = 0 + " / " + Game_Manager.Instance.lapSize.ToString();
        }
        else
        {
            _t_laps.text = Game_Manager.Instance.currentLap.ToString() + " / " + Game_Manager.Instance.lapSize.ToString();
        }
    }
    private void ShowLapTimes()
    {
        _t_lapTime.text = Game_Manager.Instance.currentLapTime.ToString();
    }
}
