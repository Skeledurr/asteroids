using UnityEngine;

/// <summary>
/// Asteroid Config is a scriptable object container for holding configuration
/// data for different Asteroid types.
/// </summary>
[CreateAssetMenu(fileName = "AsteroidConfig", menuName = "Asteroids/Asteroid Config", order = 0)]
public class AsteroidConfig : ScriptableObject
{
    public AsteroidConfigData Config;
}