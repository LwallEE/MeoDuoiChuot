using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct FactoryItem
{
    public GeneralFactory factory;
    public int itemKey;
}
public class FactoryControl : MonoBehaviour
{
    [SerializeField] private List<FactoryItem> _factoryItems;
    private Dictionary<int, GeneralFactory> dictItemKeyFactory;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        dictItemKeyFactory = new Dictionary<int, GeneralFactory>();
        foreach (var item in _factoryItems)
        {
            dictItemKeyFactory.Add(item.itemKey, item.factory);
        }
    }

    public T GetItem<T>(int itemKey) where T: MonoBehaviour
    {
        if (!dictItemKeyFactory.ContainsKey(itemKey)) return null;
        return dictItemKeyFactory[itemKey].CreateItem<T>();
    }

    public Type GetTypeFactory()
    {
        return _factoryItems[0].factory.GetTypeFactoryObject();
    }
}
