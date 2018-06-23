using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameplayManager : MonoBehaviour {

    public int round = 0, score = 0, rank = 0;
    public enum GameState { starting, paused, playing, ended };
    public GameState currentStatus = GameState.playing;
    public float remainingSeconds = 0;
    public Text scoreText, remainingTimeText;
    public int bonuses = 0, turn = 0;
    public GroupColor currentColor = GroupColor.none;
    private BubbleContainer currentBubbleContainer=null;
    public int lastMorph = -1;
    public int maxComboHits = 0;
    public int gameDuration;

    public static GameplayManager instance;


    void Awake()
    {
        if (instance != null)
            GameObject.Destroy(instance);
        else
            instance = this;


        try
        {
            
        }
        catch
        {
            Debug.Log("Integration manager no encontrado");
        }



    }

    public void OnDestroy()
    {
        try
        {
           
        }
        catch
        {

        }
        
    }



    /// <summary>
    /// Cuando se intente guardar un puntaje sucederá algo
    /// </summary>
    /// <param name="success"></param>
    private void saveScoreEvent(bool success)
    {
        
    }

    void Start()
    {

        remainingSeconds = (float)gameDuration;
	
        gStatus = GameStatus.none;
        nextStatus();

        

    }



    public int getMorph(int a, int b)
    {
        int aux = 1;

        if (lastMorph == a)
        {
            aux = UnityEngine.Random.Range(a + 1, b);
        }
        else if (lastMorph > a && lastMorph < b)
        {
            int rand = UnityEngine.Random.Range(1, 4);
            if (rand < 2)
            {
                aux = UnityEngine.Random.Range(a, lastMorph);
            }
            else
            {
                aux = UnityEngine.Random.Range(lastMorph + 1, b);
            }

        }
        else if (lastMorph == b)
        {
            aux = UnityEngine.Random.Range(a, b - 1);
        }




        return aux;
    }


    void beginNextRound()
    {
        int morph = 0;
        int aux = getCurrentLevel();

        

        switch (aux)
        {
            case 1:
                {
                    morph = getMorph(1, 6);
                    rank = 1;
                    break;
                }
            case 2:
                {
                    morph = getMorph(1, 13);
                    rank = 2;
                    break;
                }
            case 3:
                {
                    morph = getMorph(1, 12);
                    rank = 3;
                    break;
                }
            case 4:
                {
                    morph = getMorph(1, 11);
                    rank = 4;
                    break;
                }
            case 5:
                {
                    morph = getMorph(1, 11);
                    rank = 4;
                    break;
                }
        }


        Debug.Log("morph = " + morph);

        round = round + 1;
        nextRound(morph, rank);
        lastMorph = morph;
    }

    IEnumerator gameTimeIenumerator()
    {
        do
        {
            yield return new WaitForSeconds(0.5F);
            remainingSeconds = remainingSeconds - 0.5F;
            timeUpdate(remainingSeconds);

            if(remainingSeconds == 20)
            {
                //SoundManager.fadeToSong(2);
            }

        } while (remainingSeconds > 0);


        nextStatus();
        nextRound(1, 1);

    }

	void gameEnd ()
	{
	
	}


    IEnumerator changeLoadingToCountdown()
    {
        gStatus = GameStatus.loading;
        yield return new WaitForSeconds(5F);
        nextStatus();
    }
    

    void nextStatus()
    {

        switch (gStatus)
        {

            case GameStatus.none:
                {
                    StartCoroutine(changeLoadingToCountdown());
                    break;
                }

            case GameStatus.loading:
                {
					
                    gStatus = GameStatus.countdown;
                    countdown();
                    break;
                }
            case GameStatus.playing:
                {
                    gStatus = GameStatus.finish;
                    finish();
                    
                    break;
                }
            case GameStatus.countdown:
                {
                    gStatus = GameStatus.playing;
                    playing();
                    
                    break;
                }
            case GameStatus.finish:
                {
                    gStatus = GameStatus.gameEnded;
					gameEnd();
                    break;
                }

        }
        gameChangeEvent(gStatus);

    }


    IEnumerator finishIenumerator()
    {
        yield return new WaitForSeconds(6F);
        nextStatus();
    }


	IEnumerator mostrarPublicidadIenumerator()
	{
		yield return new WaitForSeconds (3.5F);
		
	}

    public void finish()
    {
        grantBonus();
        finishGameEvent();


        StartCoroutine(finishIenumerator());
		StartCoroutine (mostrarPublicidadIenumerator ());
	
		if (PlayerPrefs.GetInt ("score") < score) {
			PlayerPrefs.SetInt ("Score", score);
			PlayerPrefs.Save();
		}

    }

    public void playing()
    {
        StartCoroutine(gameTimeIenumerator());
        beginNextRound();
    }

	public void removeRemainingTime ()
	{
		this.remainingSeconds = 0;
	}

    IEnumerator countdownIenumerator()
    {
        int counter = 3;

        do
        {
            countdownChange(counter);
            counter = counter - 1;
            yield return new WaitForSeconds(1F);



        } while (counter >= 0);

        yield return new WaitForSeconds(1F);
        nextStatus();

    }


    public void countdown()
    {


        StartCoroutine(countdownIenumerator());
    }




    


    void nextRound(int morph, int rank)
    {
        GameObject aux = null;
        BubbleContainer auxBubbleContainer;
        string objectName = "";

        switch (rank)
        {
            case (1):
                {
                    objectName = "Normal/Rank1/BubbleContainer" + (morph).ToString();
                    break;
                }
            case (2):
                {
                    objectName = "Normal/Rank2/BubbleContainer" + (morph).ToString();
                    break;
                }
            case (3):
                {
                    objectName = "Normal/Rank3/BubbleContainer" + (morph).ToString();
                    break;
                }
            case (4):
                {
                    objectName = "Normal/Rank4/BubbleContainer" + (morph).ToString();
                    break;
                }


        }


        if(gStatus == GameStatus.playing)
        {
            aux = Instantiate((GameObject)Resources.Load<GameObject>(objectName) as GameObject);

            aux.transform.parent = this.gameObject.transform;
            auxBubbleContainer = aux.GetComponent<BubbleContainer>();
            auxBubbleContainer.RoundEnd += roundEnd;
            currentBubbleContainer = aux.GetComponent<BubbleContainer>();
        }
        else
        {
            if(currentBubbleContainer != null)
            {
                currentBubbleContainer.expired = true;
                currentBubbleContainer.runBubbleActions();
                Debug.Log("no se deben colocar burbujas");
            }

        }




    }



    void roundEnd(BubbleContainer bubbleContainer)
    {
        bubbleContainer.RoundEnd -= roundEnd;
        if(gStatus == GameStatus.playing)
            beginNextRound();
        turn = turn + 1;


        if (bubbleContainer.expired == false)
        {
            if (bubbleContainer.giveBonus == true)
            {
                bonuses = bonuses + 1;
                maxComboHits = Math.Max(maxComboHits, bonuses);
            }
            else
            {
                bonuses = 0;
            }
            score = score + 10;
            updateScore(score);
        }
        else
        {
            if (bonuses == 0)
            {
                decreaseTime(3F);
            }
            else
            {
                bonuses = 0;
            }
        }
        bonusUpdate(bonuses);
        destroyCurrentContainer(bubbleContainer.gameObject);

    }

    void grantBonus()
    {
        score = score + maxComboHits * 25 + rank*200;
    }


    public void decreaseTime(float amount)
    {
        remainingSeconds = remainingSeconds - amount;
        if (remainingSeconds <= 0)
        {
            remainingSeconds = 0.25F;
        }

    }

    int getCurrentLevel()
    {
        if (turn >= 0 && turn <= 6)
        {
            return 1;
        }
        if (turn >= 7 && turn <= 12)
        {
            return 2;
        }
        if (turn >= 13 && turn <= 30)
        {
            return 3;
        }
        if (turn >= 31 && turn <= 50)
        {
            return 4;
        }
        if (turn >= 51)
        {
            return 4;
        }

        return 4;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    private void destroyCurrentContainer(GameObject obj)
    {
           GameObject.Destroy(obj);
    }

    public void restartGame()
    {
        Application.LoadLevel("normal");
    }

    public void exit()
    {
        
        Application.LoadLevel("MainMenu");
        Destroy(this.gameObject);
    }

	public void replay()
	{

	}

	public void share(){

	}


    public delegate void scoreupdateScoreDelegate(int newScore);
    public scoreupdateScoreDelegate updateScore;

    public delegate void timeUpdateDelegate(float remainingTime);
    public timeUpdateDelegate timeUpdate;

    public delegate void bonusUpdateDelegate(int bonus);
    public bonusUpdateDelegate bonusUpdate;


    public enum GameStatus { none, loading, countdown, playing, finish, gameEnded }
    public GameStatus gStatus;

    public delegate void GameChangeDelegate(GameStatus newGameStatus);
    public GameChangeDelegate gameChangeEvent;

    public delegate void CountdownChangeDelegate(int counter);
    public CountdownChangeDelegate countdownChange;

    public delegate void FinishGameDelegate();
    public FinishGameDelegate finishGameEvent;

}
