using UnityEngine;
using System.Collections.Generic;

public class PoolBase<T> where T : Component
{

    private T prefab;
    private List<T> pool = new List<T>();

    public PoolBase(T prefab, int initialSize)
    {
        this.prefab = prefab;

        for (int i = 0; i < initialSize; i++)
        {
            CreateObject();
        }
    }

    public T GetObjectFromPool()
    {
        foreach (T obj in pool)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                return obj;
            }
        }

        T newObj = CreateObject();
        newObj.gameObject.SetActive(true);
        return newObj;
    }

    private T CreateObject()
    {
        T newObj = Object.Instantiate(prefab);
        pool.Add(newObj);
        return newObj;
    }
}