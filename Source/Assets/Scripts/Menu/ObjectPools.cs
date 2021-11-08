using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    [SerializeField] private Transform listMissionParent;
    
    public static ObjectPools Instance;

    public List<GameObject> pooledObjects;
    public List<GameObject> pooledUsage;

    public GameObject objectToPool;

    public int amountToPool;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Start()
    {
        pooledUsage = new List<GameObject>();
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool,transform);
            tmp.transform.SetParent(listMissionParent);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    public void AddPooledUsage(GameObject go)
    {
        pooledUsage.Add(go);
    }

    public List<GameObject> GetPooledUsage()
    {
        return pooledUsage;
    }
}
