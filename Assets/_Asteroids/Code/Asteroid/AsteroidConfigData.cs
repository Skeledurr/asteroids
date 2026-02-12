/// <summary>
/// Asteroid Config Data is a holder of all the game
/// settings for different Asteroids.
/// </summary>
[System.Serializable]
public struct AsteroidConfigData
{
    public AsteroidType AsteroidType;
    public PoolMemberType PoolMemberType;

    public int PointValue;
    
    public float BaseSpeed;
    public float SpeedRngRange;
    public float RotationRngRange;

    public int SplitCount;
    public AsteroidType SplitAsteroidType;
    
    public PoolMemberType ExplosionType;
}