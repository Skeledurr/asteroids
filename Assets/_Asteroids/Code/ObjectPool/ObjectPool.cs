using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [System.Serializable]
    public class PoolEntry
    {
        public PoolMemberType Type;
        public PoolMember Prefab;
        public int InitialSize = 10;
    }

    [SerializeField] private PoolEntry[] _poolEntries;

    private Dictionary<PoolMemberType, Queue<PoolMember>> _pools = new();
    private Dictionary<PoolMemberType, List<PoolMember>> _activeObjects = new();

    #region Unity Methods

    private void Awake()
    {
        InitialisePoolEntries();
    }

    #endregion

    #region Private Methods

    private void InitialisePoolEntries()
    {
        foreach (PoolEntry entry in _poolEntries)
        {
            Queue<PoolMember> queue = new Queue<PoolMember>();
            List<PoolMember> activeList = new List<PoolMember>();

            for (int i = 0; i < entry.InitialSize; i++)
            {
                PoolMember poolObj = CreateNew(entry);
                poolObj.gameObject.SetActive(false);
                queue.Enqueue(poolObj);
            }

            _pools.Add(entry.Type, queue);
            _activeObjects.Add(entry.Type, activeList);
        }
    }

    private PoolMember CreateNew(PoolEntry entry)
    {
        PoolMember poolMember = Instantiate(entry.Prefab, this.transform);
        poolMember.PoolMemberInitialise(entry.Type, this);
        return poolMember;
    }

    private PoolEntry GetEntry(PoolMemberType type)
    {
        foreach (var entry in _poolEntries)
        {
            if (entry.Type == type)
            {
                return entry;
            } 
        }
        
        return null;
    }
    
    #endregion

    #region Public Methods
    
    public T Spawn<T>(PoolMemberType type, Vector3 position, Quaternion rotation) where T : MonoBehaviour
    {
        PoolMember member = Spawn(type, position, rotation);

        if (!member) return null;

        return member.GetComponent<T>();
    }

    public PoolMember Spawn(PoolMemberType type, Vector3 position, Quaternion rotation)
    {
        if (!_pools.ContainsKey(type))
        {
            Debug.LogError($"No pool found for type {type}");
            return null;
        }

        PoolMember poolObj = _pools[type].Count > 0 ? _pools[type].Dequeue() : CreateNew(GetEntry(type));

        poolObj.transform.SetPositionAndRotation(position, rotation);
        poolObj.gameObject.SetActive(true);

        _activeObjects[type].Add(poolObj);

        return poolObj;
    }

    public void Return(PoolMember poolMember)
    {
        poolMember.gameObject.SetActive(false);
        poolMember.transform.SetParent(this.transform);

        _activeObjects[poolMember.MemberType].Remove(poolMember);
        _pools[poolMember.MemberType].Enqueue(poolMember);
    }

    public void ReturnAllOfType(PoolMemberType typeMask)
    {
        foreach (var kvp in _activeObjects)
        {
            PoolMemberType objType = kvp.Key;

            // Check if this type is included in the mask
            if ((typeMask & objType) == 0) continue;

            var activeListCopy = new List<PoolMember>(kvp.Value);

            foreach (PoolMember member in activeListCopy)
            {
                Return(member);
            }
        }
    }
    
    #endregion
}