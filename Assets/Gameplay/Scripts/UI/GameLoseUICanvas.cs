using System.Collections;
using System.Collections.Generic;
using ReuseSystem.AudioSystem;
using TMPro;
using UnityEngine;

public class GameLoseUICanvas : BasePopUp
{
    [SerializeField] private TextMeshProUGUI totalLevelTxt;
    public void OnInit(int level)
    {
        totalLevelTxt.text = "Total Level: " + level;
    }
    public void OnRestart()
    {
        AudioManager.Instance.PlayClip(SoundConstant.Button_Click);
        GameManager.Instance.PlayGame();
    }
}
