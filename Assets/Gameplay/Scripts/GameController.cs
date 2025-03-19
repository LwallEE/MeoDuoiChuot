using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
    [SerializeField] private Transform goal;
    private bool isStartGame;
    private int currentLevelIndex;
    private int currentAttemp;
    private int currentIncorrectTimes;

    public Action<int> OnTimeChange;
    protected override void Awake()
    {
        base.Awake();
        cat = FindObjectOfType<Cat>();
        mouse = FindObjectOfType<Mouse>();
        line = FindObjectOfType<Line>();
    }

    public void LoadFirstLevel()
    {
        LoadLevel(0);
    }

    public void LoadNextLevel()
    {
        LoadLevel(currentLevelIndex+1);
    }

    public bool IsFullLevel()
    {
        return currentLevelIndex >= levelDatas.Count-1;
    }

    public void ChooseCorrect()
    {
        if (currentAttemp >= levelDatas[currentLevelIndex].attemptToWin)
        {
            goal.localScale = Vector3.zero;
            goal.gameObject.SetActive(true);
            goal.DOScale(1f, 0.5f);
        }
        else
        {
            currentAttemp += 1;
            line.GenerateRandomLine(levelDatas[currentLevelIndex].lineLength);
        }
    }
    
    public void LoadLevel(int index)
    {
        if (index < 0 || index >= levelDatas.Count) return;
        goal.gameObject.SetActive(false);
        currentLevelIndex = index;
        currentAttemp = 1;
        
        var levelData = levelDatas[index];
        currentIncorrectTimes = levelData.incorrectTimes;
        cat.Tf.position = CameraController.Instance.GetRandomPositionInsideScreen();
        mouse.Tf.position = CameraController.Instance.GetRandomPositionInsideScreen();

        cat.StartMove();
        cat.ResetSpeed();
        
        mouse.StartMove();
        mouse.SetIsChase(false);
        currentCountDownTime = levelData.timeToFinish;
        UIManager.Instance.GetUI<GamePlayUICanvas>().OnInit(currentLevelIndex+1, Mathf.RoundToInt(currentCountDownTime),currentIncorrectTimes);
        line.GenerateRandomLine(levelData.lineLength);
        isStartGame = true;
    }

    public void IncorrectChoose()
    {
       
        this.currentIncorrectTimes -= 1;
        if (currentIncorrectTimes >= 0)
        {
            UIManager.Instance.GetUI<GamePlayUICanvas>().UpdateInCorrectTimes(this.currentIncorrectTimes);

        }
        if (this.currentIncorrectTimes < 0)
        {
            mouse.SetIsChase(true);
            cat.IncreaseSpeed();
            isStartGame = false;
            return;
        }

    }
    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }
    void Update()
    {
        if (!isStartGame) return;
        if (currentCountDownTime > 0)
        {
            var previous = currentCountDownTime;
           
            currentCountDownTime -= Time.deltaTime;
            if (Mathf.RoundToInt(currentCountDownTime) < Mathf.RoundToInt(previous))
            {
                OnTimeChange?.Invoke(Mathf.RoundToInt(currentCountDownTime));
            }
        }
        else
        {
            mouse.SetIsChase(true);
            cat.IncreaseSpeed();
            isStartGame = false;
        }
    }
    

    public void StopMoveAll()
    {
        mouse.StopMove();
        cat.StopMove();
    }
    public void SetStartGame(bool value)
    {
        isStartGame = value;
    }
}
