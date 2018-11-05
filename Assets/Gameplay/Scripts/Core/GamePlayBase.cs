using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayBase : MonoBehaviour {

    public static GamePlayBase Instance;
    public GameState _currentStatus = GameState.Begin;
    public static GameState CurrentStatus
    {
        get
        {
            return Instance._currentStatus;
        }
        set
        {
            Instance._currentStatus = value;
        }
    }

    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            Instance = this;
    }

    void Start()
    {
        NextStatus();
    }

    virtual public void LoadStart()
    {

    }

    virtual public void LoadFinish()
    {

    }

    virtual public void NextStatus()
    {
        GameState _old = GamePlayBase.CurrentStatus;
        switch (CurrentStatus)
        {
            case GameState.Begin:
                {
                    CurrentStatus = GameState.Loading;
                    break;
                }

            case GameState.Loading:
                {
                    LoadStart();
                    CurrentStatus = GameState.Welcome;
                    LoadFinish();
                    break;
                }
            case GameState.Welcome:
                {
                    CurrentStatus = GameState.Countdown;
                    break;
                }
            case GameState.Countdown:
                {
                    CurrentStatus = GameState.Playing;
                    break;
                }
            case GameState.Playing:
                {
                    CurrentStatus = GameState.Ending;
                    break;
                }
            case GameState.Ending:
                {
                    CurrentStatus = GameState.Gameover;
                    break;
                }

        }

        if(gameChangeEvent!=null)
            gameChangeEvent(CurrentStatus, _old);
    }



    public delegate void GameChangeDelegate(GameState newGameStatus, GameState oldGameStatus);
    public GameChangeDelegate gameChangeEvent;

}

public enum GameState { Begin, Loading, Welcome, Countdown, Playing, Ending, Gameover };
public enum GameHeroType { Ody, Hero2, Hero3 };
