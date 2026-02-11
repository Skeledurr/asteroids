using UnityEngine;

[System.Serializable]
public class ProceduralRoundGenerator
{
    [Header("Ranges")]
    [SerializeField] private Vector2Int _asteroidCountRange = new Vector2Int(10, 40);
    [SerializeField] private Vector2 _asteroidSpeedMultiplierRange = new Vector2(1f, 3f);
    
    [Header("Difficulty Curve")]
    // The curve which values will increase by
    [SerializeField] private AnimationCurve _difficultyCurve = AnimationCurve.Linear(0, 0, 1, 1); // optional
    // The max amount of rounds before we start capping numbers.
    [SerializeField] private int _maxRoundsBeforeCapping = 50;

    public RoundSettings GenerateNextRound(int curRound)
    {
        float percent = Mathf.Clamp01((float)curRound / _maxRoundsBeforeCapping);
        float curveMultiplier = _difficultyCurve.Evaluate(percent);
        int round = Mathf.Clamp(curRound, curRound, _maxRoundsBeforeCapping);

        int spawnCount = GetRangeValue(curveMultiplier, _asteroidCountRange);
        float speedMultiplier = GetRangeValue(curveMultiplier, _asteroidSpeedMultiplierRange);

        //Debug.Log($"Generating New Round ({curRound}) - round(clamped): {round}, percent: {percent}, curve: {curveMultiplier}, spawnCount: {spawnCount}, speedMultiplier: {speedMultiplier}");
        
        RoundSettings newRound = new RoundSettings();
        newRound.BaseSpeedMultiplier = speedMultiplier;
        newRound.AsteroidsToSpawn = new RoundSettings.SpawnSetting[]
        {
            new RoundSettings.SpawnSetting()
            {
                AsteroidType = AsteroidType.Size3_Default,
                Count = spawnCount,
            }
        };

        return newRound;
    }

    private int GetRangeValue(float percent, Vector2Int range)
    {
        return range.x + Mathf.FloorToInt(percent * (range.y - range.x));
    }

    private float GetRangeValue(float percent, Vector2 range)
    {
        return range.x + (percent * (range.y - range.x));
    }
}