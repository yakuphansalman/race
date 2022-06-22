using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    [SerializeField] private Text _t_gear;
    [SerializeField] private Text _t_speed;

    private void Update()
    {
        ShowGear();
        ShowSpeed();
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
}
