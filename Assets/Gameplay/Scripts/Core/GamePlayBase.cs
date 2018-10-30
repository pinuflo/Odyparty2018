using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayBase : MonoBehaviour {

    public static GamePlayBase Instance;
    public static GameState _currentStatus = GameState.ended;
    public static GameState CurrentStatus
    {
        get
        {
            return this._currentStatus;
        }
        set
        {
            this._currentStatus = value;
        }
    }

    void Awake()
    {
        if (Instance != null)
            GameObject.Destroy(Instance);
        else
            instance = this;
    }



}

public enum GameState { loading, welcome, countdown, playing, ending, gameover };