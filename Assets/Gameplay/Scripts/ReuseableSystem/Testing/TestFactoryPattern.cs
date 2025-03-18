using System.Collections;
using System.Collections.Generic;
using ReuseSystem;
using UnityEngine;

public class TestFactoryPattern : MonoBehaviour
{
    [SerializeField] private int number;

    [ContextMenu("Test Factory")]
    public void Test()
    {
        var obj =FactoryManager.Instance.CreateItem<TestObjectFactory>(number);
        if (obj != null)
        {
            obj.Execute();

        }
        else
        {
            Debug.Log("obj not exist");
        }
    }
}
