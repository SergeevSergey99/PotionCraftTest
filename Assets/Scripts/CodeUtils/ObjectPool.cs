using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CodeUtils
{
    public abstract class ObjectPool<T> : MonoSingleton<ObjectPool<T>> where T : Component
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _initialPoolSize = 10;

        private Queue<T> _pool = new Queue<T>();

        protected override void Init()
        {
            for (int i = 0; i < _initialPoolSize; i++)
            {
                CreateNewObject();
            }
        }

        private T CreateNewObject()
        {
            T newObject = Instantiate(_prefab, transform);
            newObject.gameObject.SetActive(false);
            _pool.Enqueue(newObject);
            return newObject;
        }

        public static T GetObject()
        {
            if (Instance._pool.Count == 0)
            {
                Instance.CreateNewObject();
            }

            T obj = Instance._pool.Dequeue();
            obj.gameObject.SetActive(true);
            return obj;
        }

        public static void ReturnObject(T obj)
        {
            obj.gameObject.SetActive(false);
            Instance._pool.Enqueue(obj);
        }

        public static void ReturnAllObjects()
        {
            foreach (Transform child in Instance.transform)
            {
                if (child.TryGetComponent<T>(out var component))
                {
                    ReturnObject(component);
                }
            }
        }
        protected override void DeInit()
        {
            _pool.Clear();
        }
    }
}