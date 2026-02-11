using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ShipController _shipController;

    private GameController _gameController;

    private void Awake()
    {
        // Wait for Game Controller to start player.
        SetControlsActive(false);
    }

    public void Initialise(GameController gameController)
    {
        _gameController = gameController;
    }

    public void SetControlsActive(bool active)
    {
        _shipController.enabled = active;
        _shipController.ResetShip();
    }

    public void ResetPlayer()
    {
        GameController.ObjectPool.ReturnAllOfType(PoolMemberType.Bullet);
        SetControlsActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // TODO Explosion
        _gameController.OnPlayerDied();
    }
    
    
}
