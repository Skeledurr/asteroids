using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidAtlas", menuName = "Asteroids/Asteroid Atlas", order = 0)]
public class AsteroidAtlas : ScriptableObject
{
    [SerializeField] private AsteroidConfig[] _configs;
    
    public AsteroidConfigData GetAsteroidConfig(AsteroidType asteroidType)
    {
        for (int i = 0; i < _configs.Length; i++)
        {
            if (_configs[i].Config.AsteroidType == asteroidType)
            {
                return _configs[i].Config;
            }
        }

        Debug.LogError($"Did not find config data for Asteroid: {asteroidType}");
        return new AsteroidConfigData();
    }
}