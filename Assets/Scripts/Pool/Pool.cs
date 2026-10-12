using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class Pool : MonoBehaviour
{
    public GameObject prefab;

    public int maxDisableCount;
    public int startCount;

    public List<GameObject> objects { get; private set; }

    Action<GameObject> addEvent;

    public void Dispose()
    {
        Clear();

        addEvent = null;
    }
    public void Clear()
    {
        if (objects == null) objects = new List<GameObject>();
        else if (objects.Count != 0)
        {
            foreach (GameObject obj in objects)
            {
                if (obj != null) Object.Destroy(obj);
            }
            objects.Clear();
        }
    }
    public TComponent Get<TComponent>() 
        where TComponent : Component
    {
        return Get().GetComponent<TComponent>();
    }
    public GameObject Get()
    {
        if (objects == null)
        {
            objects = new List<GameObject>(1);
            return Add();
        }
        for (int i = 0; i < objects.Count; i++)
        {
            GameObject obj = objects[i];
            if (obj == null)
            {
                objects.RemoveAt(objects.Count - 1);
                continue;
            }

            if (obj.activeInHierarchy == false)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        return Add();
    }
    
    public void Return<TComponent>(TComponent component) 
        where TComponent : Component
    {
        Return(component.gameObject);
    }
    public void Return(GameObject obj)
    {
        obj.SetActive(false);
    }
    
    GameObject Add()
    {
        GameObject obj = Object.Instantiate(prefab, transform);
        obj.SetActive(false);

        objects.Add(obj);
        addEvent?.Invoke(obj);

        return obj;
    }
}
