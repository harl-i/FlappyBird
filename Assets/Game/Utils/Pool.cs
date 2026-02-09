using System.Collections.Generic;
using UnityEngine;

namespace Game.Utils
{
    public class Pool<T> where T : MonoBehaviour
    {
        private T _prefab;
        private Transform _parent;
        private Queue<T> _pool = new Queue<T>();

        public Pool(T prefab, Transform parent, int initialSize)
        {
            _prefab = prefab;
            _parent = parent;

            Prewarm(initialSize);
        }

        public T Get()
        {
            T item;

            if (_pool.Count == 0)
            {
                item = Object.Instantiate(_prefab, _parent);
            }
            else
            {
                item = _pool.Dequeue();
            }

            item.gameObject.SetActive(true);
            return item;
        }

        public void Return(T item)
        {
            if (item == null)
                return;

            item.gameObject.SetActive(false);
            _pool.Enqueue(item);
        }

        public void Clear()
        {
            while (_pool.Count > 0)
            {
                var item = _pool.Dequeue();

                if (item != null)
                    Object.Destroy(item.gameObject);
            }
        }

        private void Prewarm(int initialSize)
        {
            for (int i = 0; i < initialSize; i++)
            {
                var item = Object.Instantiate(_prefab, _parent);
                item.gameObject.SetActive(false);
                _pool.Enqueue(item);
            }
        }
    }
}