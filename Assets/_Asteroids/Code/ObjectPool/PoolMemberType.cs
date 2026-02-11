[System.Flags]
public enum PoolMemberType
{
    None = 0,
    Bullet = 1 << 0,

    Asteroid_Size3  = 1 << 1,
    Asteroid_Size2  = 1 << 2,
    Asteroid_Size1  = 1 << 3,

    Asteroid = Asteroid_Size1 | Asteroid_Size2 | Asteroid_Size3
}