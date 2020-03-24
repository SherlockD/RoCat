using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool : SingletonBehaviour<ObjectsPool>
{
    private Dictionary<Type, Stack<GameObject>> _pool = new Dictionary<Type, Stack<GameObject>>();

    public static void AddObjectToPool<T>(T @object) where T : MonoBehaviour
    {
        var pool = Instance._pool;

        @object.gameObject.SetActive(false);
        @object.transform.parent = Instance.transform;

        if (!pool.ContainsKey(typeof(T)))
        {
            pool.Add(typeof(T), new Stack<GameObject>());
        }

        pool[typeof(T)].Push(@object.gameObject);
    }

    public static void TryGetObjectFromPool<T>(out T @object) where T : MonoBehaviour
    {
        var pool = Instance._pool;

        @object = null;

        if (!pool.ContainsKey(typeof(T)) || pool[typeof(T)].Count == 0)
        {
            return;
        }

        @object = pool[typeof(T)].Pop() as T;
        @object.gameObject.SetActive(true);
    }

    public static void TryGetObjectFromPool<T>(Transform parent, out T @object) where T : MonoBehaviour
    {
        var pool = Instance._pool;

        @object = null;

        if (!pool.ContainsKey(typeof(T)) || pool[typeof(T)].Count == 0)
        {
            return;
        }

        @object = pool[typeof(T)].Pop() as T;
        @object.gameObject.SetActive(true);
        @object.transform.parent = parent;
    }
}
