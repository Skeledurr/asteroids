using UnityEngine;

/// <summary>
/// Game Rounds Config is a container for data that determines
/// how difficult each round is.
/// Game play will start rounds using the Manual Round Configs (if there are any).
/// It will then proceed using the Procedural Rounds Generator indefinitely. This ensures
/// game play will continue for as many rounds as possible.
/// </summary>
[System.Serializable]
public class GameRoundsConfig
{
    [SerializeField] private RoundConfig[] _manualRoundConfigs;
    [SerializeField] private ProceduralRoundGenerator _proceduralRoundsGenerator;
    
    public RoundSettings GetRoundSettings(int roundNumber)
    {
        if (_manualRoundConfigs.Length > 0 && 
            roundNumber <= _manualRoundConfigs.Length)
        {
            return _manualRoundConfigs[roundNumber - 1].Settings;
        }
           
        return _proceduralRoundsGenerator.GenerateNextRound(roundNumber);
    }
}