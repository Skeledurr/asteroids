using UnityEngine;

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