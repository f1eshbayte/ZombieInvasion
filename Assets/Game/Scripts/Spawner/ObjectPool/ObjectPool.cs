using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private UnityEngine.GameObject _container;

    private List<UnityEngine.GameObject> _pool = new List<UnityEngine.GameObject>();

    protected bool TryGetObject(out UnityEngine.GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);
        return result != null;
    }

    protected UnityEngine.GameObject CreateObject(UnityEngine.GameObject prefab)
    {
        UnityEngine.GameObject spawned = Instantiate(prefab, _container.transform);
        spawned.SetActive(false);
        _pool.Add(spawned);
        return spawned;
    }
}
