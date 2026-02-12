using System;

/// <summary>
/// Round Settings are values that determine how
/// many asteroids should be spawned and how fast they are.
///
/// There is room for adding many more difficulty levers and unique round features.
/// </summary>
[System.Serializable]
public class RoundSettings
{
    public float BaseSpeedMultiplier = 1f;
    public SpawnSetting[] AsteroidsToSpawn;
    
    [Serializable]
    public class SpawnSetting
    {
        public AsteroidType AsteroidType = AsteroidType.Size3_Default;
        public int Count = 1;
    }
}