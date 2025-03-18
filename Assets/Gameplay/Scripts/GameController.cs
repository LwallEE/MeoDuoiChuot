using System.Collections;
using System.Collections.Generic;
using ReuseSystem;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private Cat cat;
    private Mouse mouse;
    private float currentCountDownTime;
    
    protected override void Awake()
    {
        base.Awake();
        cat = FindObjectOfType<Cat>();
        mouse = FindObjectOfType<Mouse>();
    }
}
