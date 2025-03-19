using System;
using System.Collections;
using System.Collections.Generic;
using ReuseSystem;
using ReuseSystem.AudioSystem;
using UnityEngine;

public enum EGameState
{
    GamePlay,
    GameWin,
    GameLose,
    GameMenu
}
public class GameManager : Singleton<GameManager>
{
    private EGameState currentGameState;

    private void Start()
    {
        Application.targetFrameRate = 60;
        SetSound();
        PlayGame();
    }

    void SetSound()
    {
        AudioManager.Instance.SetSoundVolume(1f);
        AudioManager.Instance.SetIsPlayMusic(true);
        AudioManager.Instance.SetIsPlaySfx(true);
    }
    public bool IsInState(EGameState state)
    {
        return currentGameState == state;
    }

    public void ChangeState(EGameState state)
    {
        currentGameState = state;
    }

    public void PlayGame()
    {
        AudioManager.Instance.PlayClip(SoundConstant.BG_MUSIC);
        ChangeState(EGameState.GamePlay);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<GamePlayUICanvas>();
        GameController.Instance.LoadFirstLevel();
    }

    public void PlayNextLevel()
    {
        AudioManager.Instance.PlayClip(SoundConstant.BG_MUSIC);
        ChangeState(EGameState.GamePlay);
        UIManager.Instance.CloseAll();
        UIManager.Instance.OpenUI<GamePlayUICanvas>();
        GameController.Instance.LoadNextLevel();
    }

    public void GameWin()
    {
        if (!IsInState(EGameState.GamePlay)) return;
        AudioManager.Instance.StopMusic(SoundConstant.BG_MUSIC);
        AudioManager.Instance.PlayClip(SoundConstant.Level_Win);
        ChangeState(EGameState.GameWin);
        if (GameController.Instance.IsFullLevel())
        {
            UIManager.Instance.OpenUI<GameCompleteAllLevelCanvas>();
        }
        else
        {
            UIManager.Instance.OpenUI<GameWinUICanvas>().OnInit(GameController.Instance.GetCurrentLevelIndex()+1);
        }
        GameController.Instance.SetStartGame(false);
        GameController.Instance.StopMoveAll();
        Debug.Log("Game win");
    }

    public void GameLose()
    {
        if (!IsInState(EGameState.GamePlay)) return;
        AudioManager.Instance.StopMusic(SoundConstant.BG_MUSIC);
        AudioManager.Instance.PlayClip(SoundConstant.Level_Lose);
        ChangeState(EGameState.GameLose);
        UIManager.Instance.OpenUI<GameLoseUICanvas>().OnInit(GameController.Instance.GetCurrentLevelIndex()+1);
        GameController.Instance.SetStartGame(false);
        GameController.Instance.StopMoveAll();
        Debug.Log("Game lose");
    }
}
