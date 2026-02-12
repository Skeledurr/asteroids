using System.Collections;
using UnityEngine;

/// <summary>
/// Round Controller handles the game flow during rounds.
/// Coordinating key components and timings during the start
/// and end of the round.
/// </summary>
public class RoundController : MonoBehaviour
{
    private GameSession Session => GameController.GameSession;
    
    [SerializeField] private Player _player;
    [SerializeField] private AsteroidManager _asteroidManager;
    
    private readonly float _startDelay = 1f;
    private readonly float _endDelay = 1.5f;

    private GameController _gameController;
    private GameConfig _config;
    private Coroutine _roundRoutine;

    #region Public Methods
    
    public void Initialise(GameController gameController, GameConfig config)
    {
        _config = config;
        _gameController = gameController;
    }

    public void StartNewGame()
    {
        Session.StartNewSession(_config);
        StartRound();
    }
    
    public void StartRound()
    {
        if (_roundRoutine != null)
        {
            Debug.LogError("A Round Routine is already running");
            return;
        }
        _roundRoutine = StartCoroutine(RoundFlow());
    }
    
    #endregion
    

    #region Private Methods

    private IEnumerator RoundFlow()
    {
        PrepareRound();
        
        yield return new WaitForSeconds(_startDelay);

        RoundStart();
        
        // Wait for round to complete
        while (!Session.IsRoundOver && 
               !Session.IsGameOver)
        {
            yield return null;
        }

        // Wait before completing.
        yield return new WaitForSeconds(_endDelay);

        // Set round to null before we call game controller.
        _roundRoutine = null;

        RoundComplete();
    }

    private void PrepareRound()
    {
        _player.PrepareRoundStart();
        _asteroidManager.PrepareRoundStart();
    }

    private void RoundStart()
    {
        _player.StartRound();
        
        RoundSettings settings = _config.GetRoundSettings(Session.Round);
        _asteroidManager.StartRound(settings);
    }

    private void RoundComplete()
    {
        Session.RoundComplete();
        
        if (Session.IsGameOver)
        {
            _gameController.GameOver();
        }
        else
        {
            if (Session.IsPlayerAlive)
            {
                Session.NextRound();
            }
            
            _gameController.RoundComplete();
        }
    }

    #endregion
}
