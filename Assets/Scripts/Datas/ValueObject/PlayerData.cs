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

}

[Serializable]
public class PlayerThrowForceData
{
    public Vector3 throwForce = new Vector3(0, 0, 3);
}
