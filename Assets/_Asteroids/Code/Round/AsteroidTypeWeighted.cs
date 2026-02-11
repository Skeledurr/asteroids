using UnityEngine;

[System.Serializable]
public class AsteroidTypeWeighted
{
    public AsteroidType Type;
    public AnimationCurve WeightOverRounds = AnimationCurve.Linear(0, 1, 1, 0);
}