using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ShipController _shipController;

    private void Awake()
    {
        // Wait for Game Controller to start player.
        SetControlsActive(false);
    }

    public void SetControlsActive(bool active)
    {
        _shipController.enabled = active;
    }

    public void ResetPlayer()
    {
        GameController.ObjectPool.ReturnAllOfType(PoolMemberType.Bullet);
        SetControlsActive(false);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Player Hit: {other.gameObject.name}");
    }
    
    
}
