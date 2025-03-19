using System.Collections;
using System.Collections.Generic;
using ReuseSystem.AudioSystem;
using UnityEngine;

public class GameCompleteAllLevelCanvas : BasePopUp
{
   
    public void OnRestart()
    {
        AudioManager.Instance.PlayClip(SoundConstant.Button_Click);
        GameManager.Instance.PlayGame();
    }
    
}
