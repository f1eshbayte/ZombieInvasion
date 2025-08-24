using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;

    private List<GameObject> _pool = new List<GameObject>();

    // Инициализация пула
    protected void Initialize(GameObject prefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject spawned = Instantiate(prefab, _container.transform);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    // Вернуть свободный объект
    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);
        return result != null;
    }

    // Добавить новый объект, если все заняты
    protected GameObject CreateObject(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, _container.transform);
        obj.SetActive(false);
        _pool.Add(obj);
        return obj;
    }
}
