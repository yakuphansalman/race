using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDrivable
{
    float Accelerate();
    float Brake();
    float Steer();
}
