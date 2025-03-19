using System;
using System.Collections;
using System.Collections.Generic;
using ReuseSystem;
using TMPro;
using UnityEngine;

public class GamePlayUICanvas : UICanvas
{
    [SerializeField] private TextMeshProUGUI timeTxt;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private TextMeshProUGUI incorrectTimeTxt;
    
    public void OnInit(int level, int time,int incorrectTimes)
    {
        timeTxt.text = time.ToString() + "s";
        levelTxt.text = "Level " + level.ToString();
        this.incorrectTimeTxt.text = "So Lan Chon Sai Con Lai: " + incorrectTimes;
    }

    void Start()
    {
        GameController.Instance.OnTimeChange += UpdateTime;
    }

    void UpdateTime(int time)
    {
        timeTxt.text = time + "s";
    }

    public void UpdateInCorrectTimes(int incorrectTimes)
    {
        this.incorrectTimeTxt.text = "So Lan Chon Sai Con Lai: " + incorrectTimes;
    }
    private void OnDestroy()
    {
        GameController.Instance.OnTimeChange -= UpdateTime;
    }
}
