using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ReuseSystem
{
    public class FactoryManager : Singleton<FactoryManager>
    {
        [SerializeField] private List<FactoryControl> factories;

        private Dictionary<Type, FactoryControl> dictTypedFactory;

        private void Start()
        {
            Init();
        }

        void Init()
        {
            dictTypedFactory = new Dictionary<Type, FactoryControl>();
            foreach (var factory in factories)
            {
                dictTypedFactory.Add(factory.GetTypeFactory(), factory);
            }
        }

        public T CreateItem<T>(int itemKey) where T : MonoBehaviour
        {
            var type = typeof(T);
            if (!dictTypedFactory.ContainsKey(type)) return null;
            return dictTypedFactory[type].GetItem<T>(itemKey);
        }

    }
}
