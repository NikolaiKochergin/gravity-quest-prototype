using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Common
{
    public class ObjectPool<TComponent> where TComponent : Component
    {
        private readonly TComponent _prefab;
        private readonly Transform _parent;
        
        private readonly List<TComponent> _all;
        private readonly Queue<TComponent> _free;
        private readonly bool _canExpand;

        public ObjectPool(TComponent prefab, int capacity, Transform parent = null, bool canExpand = false)
        {
            if (prefab == null)
                throw new System.ArgumentNullException(nameof(prefab), "Prefab can't be null");
            
            _prefab = prefab;
            _all = new List<TComponent>(capacity);
            _free = new Queue<TComponent>(capacity);
            _parent = parent;
            _canExpand = canExpand;

            for (int i = 0; i < capacity; i++)
            {
                var component = CreateNew();
                _all.Add(component);
                _free.Enqueue(component);
            }
        }

        public TComponent Get(bool active = true)
        {
            if (_free.TryDequeue(out TComponent component))
            {
                component.gameObject.SetActive(active);
                return component;
            }

            if (_canExpand)
            {
                component = CreateNew();
                component.gameObject.SetActive(active);
                _all.Add(component);
                return component;
            }

            return null;
        }

        public void Release(TComponent component)
        {
            component.gameObject.SetActive(false);
            _free.Enqueue(component);
        }

        public void ReleaseAll()
        {
            _free.Clear();
            foreach (var component in _all)
            {
                component.gameObject.SetActive(false);
                _free.Enqueue(component);
            }
        }

        private TComponent CreateNew()
        {
            TComponent component = Object.Instantiate(_prefab, _parent);
            component.gameObject.SetActive(false);
            return component;
        }
    }
}