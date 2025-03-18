using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GeneralFactory : MonoBehaviour
{
    protected Type factoryGeneratedType;

    protected virtual void Awake()
    {
        SetupTypeFactory();
    }

    public abstract T CreateItem<T>() where T : MonoBehaviour;

    protected bool CheckIsType<T>()
    {
        return typeof(T) == factoryGeneratedType;
    }

    protected abstract void SetupTypeFactory();

    public Type GetTypeFactoryObject()
    {
        return factoryGeneratedType;
    }
    
}

