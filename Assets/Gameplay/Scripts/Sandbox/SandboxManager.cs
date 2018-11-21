using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxManager : GamePlayBase
{
    override protected void OnLoadStart()
    {
        base.OnLoadStart();
        Debug.Log("=LOADING STARTS");
    }

    override protected void OnLoadFinish()
    {
        base.OnLoadFinish();
        Debug.Log("=LOADING FINISHES");
    }

    protected override void OnStateChange(GameState newGameStatus, GameState oldGameStatus)
    {
        base.OnStateChange(newGameStatus, oldGameStatus);
        Debug.Log("=Game status change: " + oldGameStatus.ToString() + " ->" + newGameStatus.ToString());

    }


}
