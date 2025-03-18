using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFactory : GeneralFactory
{
    [SerializeField] private TestObjectFactory prefab;
    public sealed override T CreateItem<T>()
    {
        if (!CheckIsType<T>()) return null;
        return CreateObject() as T;
    }

    protected override void SetupTypeFactory()
    {
        factoryGeneratedType = typeof(TestObjectFactory);
    }

    TestObjectFactory CreateObject()
    {
        return Instantiate(prefab);
    }

}
