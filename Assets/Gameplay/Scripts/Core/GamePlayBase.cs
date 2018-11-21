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



    ///<summary>
    ///Triggers when the game status changes
    ///</summary>
    virtual protected void OnStateChange(GameState newGameStatus, GameState oldGameStatus)
    {

    }

    ///<summary>
    ///Triggers when the loading phase starts
    ///</summary>
    virtual protected void OnLoadStart()
    {

    }

    ///<summary>
    ///Triggers when the loading phase finishes
    ///</summary>
    virtual protected void OnLoadFinish()
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
                    OnLoadStart();
                    CurrentStatus = GameState.Welcome;
                    OnLoadFinish();
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
        OnStateChange(CurrentStatus, _old );
        if (gameStateChangeEvent!=null)
            gameStateChangeEvent(CurrentStatus, _old);
    }



    public delegate void GameChangeDelegate(GameState newGameStatus, GameState oldGameStatus);
    public GameChangeDelegate gameStateChangeEvent;

}

public enum GameState { Begin, Loading, Welcome, Countdown, Playing, Ending, Gameover };
public enum GameHeroType { Ody, Hero2, Hero3 };
