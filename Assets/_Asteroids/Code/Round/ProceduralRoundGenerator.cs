using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Procedural Round Generator handles generating additional rounds
/// after all the Manually created rounds have been completed.
///
/// By determining a Max Round and calculating a percentage with the current round.
/// The percent value will be passed into Animation Curves to determine values that
/// alter what is spawned and how difficult the round is.
/// </summary>
[System.Serializable]
public class ProceduralRoundGenerator
{
    [Header("Weights")]
    [SerializeField] private AsteroidTypeWeighted[] _weightedAsteroids;
    
    [Header("Ranges")]
    [SerializeField] private AnimationCurve _asteroidCountRange = AnimationCurve.Linear(0, 10, 1, 40);
    [SerializeField] private AnimationCurve _asteroidSpeedMultiplierRange = AnimationCurve.Linear(0, 1f, 1, 3f);
    
    [Header("Difficulty Cap")]
    // The max amount of rounds before we start capping numbers.
    [SerializeField] private int _maxRoundsBeforeCapping = 50;

    public RoundSettings GenerateNextRound(int curRound)
    {
        float percent = Mathf.Clamp01((float)curRound / _maxRoundsBeforeCapping);

        int spawnCount = Mathf.FloorToInt(_asteroidCountRange.Evaluate(percent));
        float speedMultiplier = _asteroidSpeedMultiplierRange.Evaluate(percent);

        RoundSettings newRound = new RoundSettings();
        newRound.BaseSpeedMultiplier = speedMultiplier;
        newRound.AsteroidsToSpawn = GetWeightedSpawnSettings(percent, spawnCount, _weightedAsteroids);

        return newRound;
    }

    private RoundSettings.SpawnSetting[] GetWeightedSpawnSettings(float percent, int spawnCount, AsteroidTypeWeighted[] weightedAsteroids)
    {
        // Calculate weights for current round
        float totalWeight = 0f;
        foreach (AsteroidTypeWeighted weight in weightedAsteroids)
        {
            totalWeight += weight.WeightOverRounds.Evaluate(percent);
        }

        // Build a dictionary of all the asteroids to spawn.
        Dictionary<AsteroidType, int> asteroidCount = new();
        
        for (int i = 0; i < spawnCount; i++)
        {
            float roll = Random.value * totalWeight;
            float cumulative = 0f;

            foreach (AsteroidTypeWeighted weight in _weightedAsteroids)
            {
                cumulative += weight.WeightOverRounds.Evaluate(percent);
                if (roll <= cumulative)
                {
                    if (!asteroidCount.ContainsKey(weight.Type))
                    {
                        asteroidCount[weight.Type] = 0;
                    }

                    asteroidCount[weight.Type]++;
                }
            }
        }

        // Build the spawn settings based on the dictionary.
        List<RoundSettings.SpawnSetting> settings = new List<RoundSettings.SpawnSetting>();

        foreach (var kvp in asteroidCount)
        {
            settings.Add(new RoundSettings.SpawnSetting()
            {
                AsteroidType = kvp.Key,
                Count = kvp.Value
            });
        }

        return settings.ToArray();
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