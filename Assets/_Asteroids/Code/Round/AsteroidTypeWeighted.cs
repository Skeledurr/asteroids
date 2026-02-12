using UnityEngine;

/// <summary>
/// Asteroid Type Weighted is a container that couples an Asteroid Type
/// with an Animation Curve that determines the type's weight as the
/// game progresses.
/// </summary>
[System.Serializable]
public class AsteroidTypeWeighted
{
    public AsteroidType Type;
    public AnimationCurve WeightOverRounds = AnimationCurve.Linear(0, 1, 1, 0);
}