using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ShipController _shipController;
    [SerializeField] private GameObject _playerVisualsObj;
    [SerializeField] private Collider _shipCollider;

    public void PrepareRoundStart()
    {
        ReturnAllBullets();
        _shipController.ResetShip();
        SetColliderActive(false);
        SetVisualsActive(true);
        SetControlsActive(true);
    }

    public void StartRound()
    {
        SetColliderActive(true);
        SetControlsActive(true);
        GameController.GameSession.PlayerAlive();
    }
    
    private void PlayerDied()
    {
        GameController.ObjectPool.Spawn(PoolMemberType.Player_Explosion, this.transform.position, Quaternion.identity);
        ReturnAllBullets();
        SetVisualsActive(false);
        SetControlsActive(false);
        SetColliderActive(false);
        GameController.GameSession.PlayerDied();
    }
    
    private void SetControlsActive(bool active)
    {
        _shipController.enabled = active;
    }

    private void SetVisualsActive(bool active)
    {
        _playerVisualsObj.SetActive(active);
    }

    private void SetColliderActive(bool active)
    {
        _shipCollider.enabled = active;
    }

    private void ReturnAllBullets()
    {
        GameController.ObjectPool.ReturnAllOfType(PoolMemberType.Bullet);
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerDied();
    }
}
