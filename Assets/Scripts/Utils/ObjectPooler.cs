using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    // Simple object pooler
    public class ObjectPooler : MonoBehaviour
    {
        public static ObjectPooler Instance;
        [SerializeField] private List<Pool> pools;
        public Dictionary<GameObject, Queue<GameObject>> poolsDictionary;
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            poolsDictionary = new Dictionary<GameObject, Queue<GameObject>>();
            foreach (var pool in pools)
            {
                var objectQueue = new Queue<GameObject>();
                for (int i = 0; i < pool.prewarm; i++)
                {
                    var obj = Instantiate(pool.prefab, transform, true);
                    obj.SetActive(false);
                    objectQueue.Enqueue(obj);
                }
                poolsDictionary.Add(pool.prefab, objectQueue);
            }
        }

        public GameObject SpawnFromPool(GameObject obj)
        {
            GameObject spawnedFromPool = poolsDictionary[obj].Dequeue();
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