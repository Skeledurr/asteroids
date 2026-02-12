using UnityEngine;

/// <summary>
/// Player Config is a scriptable object that contains all of the
/// player game play values.
/// </summary>
[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Asteroids/Player Config", order = 0)]
public class PlayerConfig : ScriptableObject
{
    [Header("Bullet")]
    public PoolMemberType BulletType;
    
    [Header("Movement")]
    public float ShipThrustPower = 10f;
    public float ShipTurnSpeed = 180f;
    
    [Header("Slow Motion")]
    public float SlowMotionPercent = 0.1f;
    public float SlowMotionDuration = 5f;
    public float SlowMotionRechargeRate = 0.1f;
}
