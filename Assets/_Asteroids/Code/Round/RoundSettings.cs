using System;
using UnityEngine;

[System.Serializable]
public class RoundSettings
{
    public SpawnSetting[] AsteroidsToSpawn;
    
    [Serializable]
    public class SpawnSetting
    {
        public AsteroidType AsteroidType = AsteroidType.Size3_Default;
        public int Count = 1;
        public float BaseSpeedMultiplier = 1f;
    }
}