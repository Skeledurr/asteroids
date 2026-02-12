using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _startGameObj;
    [SerializeField] private GameObject _gameplayObj;
    [SerializeField] private GameObject _gameoverObj;
    
    private GameController _gameController;
    private PlayerControls _controls;

    public enum UIState
    {
        None,
        StartGame,
        GameOver,
        GamePlay
    }

    private void Awake()
    {
        _controls = new PlayerControls();
        _controls.UI.Submit.performed += OnStartGame;
    }

    public void Initialise(GameController gameController)
    {
        _gameController = gameController;
    }

    public void SetUIState(UIState newState)
    {
        _startGameObj.SetActive(newState == UIState.StartGame);
        _gameoverObj.SetActive(newState == UIState.GameOver);
        _gameplayObj.SetActive(newState == UIState.GamePlay);
        
        SetStartGameInputActive(newState == UIState.StartGame ||
                                newState == UIState.GameOver);
    }

    private void SetStartGameInputActive(bool active)
    {
        if (active)
        {
            _controls.UI.Enable();
        }
        else
        {
            _controls.UI.Disable();
        }
    }
    
    private void OnStartGame(InputAction.CallbackContext obj)
    {
        _gameController.StartNewGame();
    }
}