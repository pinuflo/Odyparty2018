using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UI : MonoBehaviour {

    public gameNumber GN1, GN2;
    public Text scoreText, finalScreeScoreText, bonusScoreEndText, difficultyScore;
    public GameObject coundownPositionObject, endGameEffect, endGameMenu;
    public Animator timerAnimator, scoreAnimator;
    public ComboContainer comboContainer;
    public GameObject tutorial;

    void Awake()
    {
        /**
        GameplayManager.instance.timeUpdate += timeUpdate;
        GameplayManager.instance.bonusUpdate += bonusUpdate;
        GameplayManager.instance.updateScore += updateScore;
        GameplayManager.instance.gameChangeEvent += gameChangeEvent;
        GameplayManager.instance.countdownChange += countdownChangeEvent;
        GameplayManager.instance.finishGameEvent += finishGameEvent;
    **/
    }

    private void finishGameEvent()
    {
        /**
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/FinishText"), coundownPositionObject.transform.position, Quaternion.identity) as GameObject;
        obj.transform.parent = this.gameObject.transform;
        endGameEffect.SetActive(true);
        endGameMenu.SetActive(true);
		bonusScoreEndText.text = (GameplayManager.instance.maxComboHits * 25).ToString ();
		difficultyScore.text = (GameplayManager.instance.rank * 200).ToString();
     **/
    }

    private void countdownChangeEvent(int counter)
    {
        /**
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/CountdownNumber"), coundownPositionObject.transform.position, Quaternion.identity) as GameObject;
        obj.transform.parent = this.gameObject.transform;

        if (counter > 0)
            obj.GetComponent<CountdownText>().setText(counter.ToString());
        else
            obj.GetComponent<CountdownText>().setText("GO!");
        **/
    }

    /**
    private void gameChangeEvent(GameplayManager.GameStatus newGameStatus)
    {
        switch(newGameStatus)
        {
            case GameplayManager.GameStatus.loading:
                {
                    tutorial.SetActive(true);
                    tutorial.GetComponent<Animator>().SetInteger("Status", 1);
                    break;
                }
            case GameplayManager.GameStatus.countdown:
                {
                    tutorial.GetComponent<Animator>().SetInteger("Status", 0);
                    break;
                }
            case GameplayManager.GameStatus.playing:
                {
                    tutorial.SetActive(false);
                    timerAnimator.SetInteger("Status", 1);
                    scoreAnimator.SetInteger("Status", 1);
                    break;
                }
            case GameplayManager.GameStatus.finish:
                {
                    timerAnimator.SetInteger("Status", 0);
                    scoreAnimator.SetInteger("Status", 0);
                    finalScreeScoreText.text = GameplayManager.instance.score.ToString();
                    break;
                }


        }
    }


    void bonusUpdate(int bonus)
    {
        comboContainer.updateCombo(bonus);
    }

    void timeUpdate(float remainingTime)
    {
        int representation = (int)remainingTime;
        int aux1 = representation % 10;
        int aux2 = representation / 10;

        GN1.SetNumber(aux2);
        GN2.SetNumber(aux1);

    }
    **/

    void updateScore(int score)
    {
        scoreText.text = score.ToString();
    }
	


	
}
