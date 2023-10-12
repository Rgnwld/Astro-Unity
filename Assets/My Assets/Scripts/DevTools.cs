using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevTools
{
    public static bool IsInLayerMask(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }

    public static Vector2 Vec3toVec2(Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }

    public static string TranslateSecondsToString(float seconds)
    {
        int min, sec;
        float milisec;

        min = (int)seconds / 60;
        sec = (int)seconds % 60;
        milisec = (int)((seconds % 1) * 100);

        string strMin = min < 10 ? "0" + min.ToString() : min.ToString();
        string strSec = sec < 10 ? "0" + sec.ToString() : sec.ToString();
        string strMilisec = milisec < 10 ? "0" + milisec.ToString() : milisec.ToString();

        return $"{strMin}:{strSec}:{strMilisec}";
    }
}
