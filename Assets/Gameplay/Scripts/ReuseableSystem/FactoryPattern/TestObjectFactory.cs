using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectFactory : MonoBehaviour
{
    [SerializeField] private int numberObj;

    public void Execute()
    {
       Debug.Log($"Day la {numberObj}");
    }
}
