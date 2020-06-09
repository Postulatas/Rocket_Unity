using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Map
{
    public static double mapping(this double val, double from1, double to1, double from2, double to2)
    {
        return (val - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
