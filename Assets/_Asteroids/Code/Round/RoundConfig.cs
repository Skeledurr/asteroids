using UnityEngine;

/// <summary>
/// Round Config is a scriptable object that allows for creating
/// Round Settings that can be serialized.
/// </summary>
[CreateAssetMenu(fileName = "RoundConfig", menuName = "Asteroids/Round Config", order = 0)]
public class RoundConfig : ScriptableObject
{
    public RoundSettings Settings;
}