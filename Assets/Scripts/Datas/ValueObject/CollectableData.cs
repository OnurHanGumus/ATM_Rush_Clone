using System;
using System.Numerics;


[Serializable]
public class CollectableData
{
    public LerpData lerpData;
}

[Serializable]
public class LerpData
{
    public float lerpSpaces = 2;
    public float lerpSoftnessX = 20;
    public float lerpSoftnessZ = 20;
}
