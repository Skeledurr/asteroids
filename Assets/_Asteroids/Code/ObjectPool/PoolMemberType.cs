[System.Flags]
public enum PoolMemberType
{
    None = 0,
    Bullet = 1 << 0,

    Asteroid_Size3  = 1 << 1,
    Asteroid_Size2  = 1 << 2,
    Asteroid_Size1  = 1 << 3,
    
    Player_Explosion = 1 << 4,
    Asteroid_Size1_Explosion = 1 << 5,
    Asteroid_Size2_Explosion = 1 << 6,
    Asteroid_Size3_Explosion = 1 << 7,

    Asteroid = Asteroid_Size1 | Asteroid_Size2 | Asteroid_Size3
}