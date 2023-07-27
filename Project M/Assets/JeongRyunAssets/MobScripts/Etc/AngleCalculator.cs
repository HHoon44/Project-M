using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectM.Calculator
{
    public class AngleCalculator
    {
        static public float AtFlipAngle(float _angle)
        {
            _angle %= 360;

            if (_angle <= 90 || _angle >= 270)
                return _angle;

            float newAngle = -(180 - _angle);
            return newAngle;
        }
        static public bool FlipForAngle(float _angle)
        {
            _angle %= 360;

            if (_angle <= 90 || _angle >= 270)
                return false;
            else
                return true;
        }
    }
}
