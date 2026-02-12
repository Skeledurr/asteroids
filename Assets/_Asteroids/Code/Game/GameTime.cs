using UnityEngine;

/// <summary>
/// Game Time is a simple time scale class for the game.
/// Currently only used to provide a specific time scale for asteroids
/// to use, so the slow motion mechanic can only slow asteroids down.
/// </summary>
public class GameTime
{
    public float AsteroidTimeScale { get; private set; } = 1f;

    public void SetSlowMotion(float scale)
    {
        AsteroidTimeScale = Mathf.Clamp(scale, 0.05f, 1f);
    }

    public void ResetSlowMotion()
    {
        AsteroidTimeScale = 1f;
    }

    public void ResetTime()
    {
        AsteroidTimeScale = 1f;
    }
}