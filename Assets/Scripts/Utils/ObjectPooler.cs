using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    // Simple object pooler
    public class ObjectPooler : MonoBehaviour
    {
        private static ObjectPooler Instance;
        [SerializeField] private List<Pool> pools;
        public Dictionary<string, Queue<GameObject>> poolsDictionary;
        private GameObject spawnedFromPool;
        private Queue<GameObject> objectQueue;
        private GameObject obj;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            poolsDictionary = new Dictionary<string, Queue<GameObject>>();
            foreach (var pool in pools)
            {
                objectQueue = new Queue<GameObject>();
                for (int i = 0; i < pool.prewarm; i++)
                {
                    obj = Instantiate(pool.prefab, transform, true);
                    obj.SetActive(false);
                    objectQueue.Enqueue(obj);
                }

                poolsDictionary.Add(pool.prefab.name, objectQueue);
            }
        }

        public GameObject SpawnFromPool(string obj)
        {
            spawnedFromPool = poolsDictionary[obj].Dequeue();
            spawnedFromPool.SetActive(true);
            poolsDictionary[obj].Enqueue(spawnedFromPool);
            return spawnedFromPool;
        }

        public void ResetObjects()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    [Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int prewarm;
    }
}