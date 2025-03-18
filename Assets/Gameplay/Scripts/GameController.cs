using System.Collections;
using System.Collections.Generic;
using Gameplay.Scripts.Level;
using ReuseSystem;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    private Cat cat;
    private Mouse mouse;
    private float currentCountDownTime;
    private Line line;
    [SerializeField] private List<LevelData> levelDatas;
    
    protected override void Awake()
    {
        base.Awake();
        cat = FindObjectOfType<Cat>();
        mouse = FindObjectOfType<Mouse>();
        line = FindObjectOfType<Line>();
    }

    public void LoadLevel(LevelData levelData)
    {
        cat.Tf.position = CameraController.Instance.GetRandomPositionInsideScreen();
        mouse.Tf.position = CameraController.Instance.GetRandomPositionInsideScreen();

        currentCountDownTime = levelData.timeToFinish;
        line.GenerateRandomLine(levelData.lineLength);
    }
}
