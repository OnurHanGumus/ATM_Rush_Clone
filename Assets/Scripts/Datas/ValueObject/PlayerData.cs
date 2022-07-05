using System;
using System.Numerics;


[Serializable]
public class PlayerData
{
    public PlayerMovementData playerMovementData;
    public PlayerThrowForceData playerThrowForceData;
}

[Serializable]
public class PlayerMovementData
{
    public float ForwardSpeed = 5;
    public float SidewaysSpeed = 2;


}

[Serializable]
public class PlayerThrowForceData
{
    public Vector2 ThrowForce = new Vector2(2, 2);
}
