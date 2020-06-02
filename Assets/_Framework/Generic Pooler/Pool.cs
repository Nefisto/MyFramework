using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pooler", menuName = "Framework/Pool")]
public class Pool : ScriptableObject
{
    public GameObject prefab;
    public int initialAmmount;

    private Queue<GameObject> poolableObjects;
    private Transform folder;

    public GameObject Spawn()
    {
        if (poolableObjects == null)
        {
            Debug.Log("Objetos serão iniciado em tempo de execução, procure iniciar no loading para evitar spikes");

            InitPool();
        }

        if (poolableObjects.Count == 0)
            Grow(initialAmmount);

        return poolableObjects.Dequeue();
    }

    public T Spawn<T>()
    {
        if (poolableObjects == null)
        {
            Debug.Log("Objetos serão iniciado em tempo de execução, procure iniciar no loading para evitar spikes");

            InitPool();
        }

        if (poolableObjects.Count == 0)
            Grow(initialAmmount);

        return poolableObjects.Dequeue().GetComponent<T>();
    }

    public void Despawn(GameObject obj)
    {
        obj.transform.parent = folder;
        obj.SetActive(false);

        poolableObjects.Enqueue(obj);
    }

    public void InitPool()
    {
        // O pool já foi iniciado
        if (poolableObjects != null)
            return;

        poolableObjects = new Queue<GameObject>();

        folder = new GameObject().transform;
        folder.name = "Pool_" + name;

        Grow(initialAmmount);
    }

    public void ClosePool()
    {
        poolableObjects.Clear();
        poolableObjects = null;
    }

    private void Grow(int amount)
    {
        for (int i = 0; i < amount; i++)
            poolableObjects.Enqueue(CreateObject());
    }

    private GameObject CreateObject()
    {
        var obj = GameObject.Instantiate(prefab, folder);
        obj.SetActive(false);

        var poolObjRef = obj.GetComponent<PooledObject>();
        if (!poolObjRef)
            poolObjRef = obj.AddComponent<PooledObject>();

        poolObjRef.originPool = this;

        return obj;
    }
}