using UnityEngine;

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