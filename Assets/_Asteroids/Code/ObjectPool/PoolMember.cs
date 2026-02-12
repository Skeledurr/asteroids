using UnityEngine;

/// <summary>
/// Pool Member is a simple class that holds a reference
/// to the Member Type and the Object Pool, making it easier to
/// return members.
/// This class is often inherited. See <see cref="Asteroid"/> and <see cref="Player"/>
/// </summary>
public class PoolMember : MonoBehaviour
{
    public PoolMemberType MemberType { get; private set; }
    private ObjectPool _pool;

    public void PoolMemberInitialise(PoolMemberType type, ObjectPool pool)
    {
        MemberType = type;
        _pool = pool;
    }

    public void ReturnToPool()
    {
        _pool.Return(this);
    }
}