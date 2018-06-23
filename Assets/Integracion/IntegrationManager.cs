using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class IntegrationManager : MonoBehaviour {

    public const string MAIN_LEADERBOARDCODE = "CgkIipDV1KMVEAIQAQ";  //Código de la tabla de puntajes
    public const string EVENT_GAMES_PLAYED = "CgkIipDV1KMVEAIQAg";  //Evento cantidad de juegos en Google Play Games
    public const string EVENT_BONUS_HITS1 = "CgkIipDV1KMVEAIQAw";  //Evento 10 o menos bonus hits
    public const string EVENT_BONUS_HITS2 = "CgkIipDV1KMVEAIQBA";  //Evento 11 a 30 bonus hits
    public const string EVENT_BONUS_HITS3 = "CgkIipDV1KMVEAIQBQ";  //Evento 31 o mas bonus hits
    public const string EVENT_RANK1 = "CgkIipDV1KMVEAIQBg";  //Evento se termina el juego con rank 1
    public const string EVENT_RANK2 = "CgkIipDV1KMVEAIQCQ";  //Evento se termina el juego con rank 2
    public const string EVENT_RANK3 = "CgkIipDV1KMVEAIQCg";  //Evento se termina el juego con rank 3
    public const string EVENT_RANK4 = "CgkIipDV1KMVEAIQBw";  //Evento se termina el juego con rank 4
    public const string EVENT_RANK5 = "CgkIipDV1KMVEAIQCA";  //Evento se termina el juego con rank 4

	public FBscript facebookManager;
	public admobScript admob;
	public EveryplayTest everyplay;
	public Opciones opciones;
	
    private static IntegrationManager instance;
    public static IntegrationManager Instance
    {
        get
        {
            return instance;
        }
    }

	public static bool IsPlayerConenctedToFacebook {
		get
		{
			return Instance.facebookManager.getFacebookStatus();
		}
	}


    public static bool isFirstTimeLoad
    {
        get
        {
            int aux = PlayerPrefs.GetInt("isFirstTimeLoad",0);

            if(aux==0)
            {
                PlayerPrefs.SetInt("isFirstTimeLoad", 1);
                return true;
            }
            else
            {
                return false;
            }
        }
    }



    public static bool isPlayerConnectedToGooglePlay = false;

	void connectToFacebookEvent (bool success)
	{
		throw new System.NotImplementedException ();
	}

	public void scoresFacebook (bool success, System.Collections.Generic.List<object> dataList)
	{
		scoresFacebookEvent (success,dataList);
	}

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
        DontDestroyOnLoad(transform.gameObject);
		

        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();

		facebookManager.connectToFacebookEvent += connectToFacebook;
		facebookManager.scoresFacebookEvent += scoresFacebook;
	}


	void OnDestroy()
	{
		facebookManager.connectToFacebookEvent -= connectToFacebook;
		facebookManager.scoresFacebookEvent -= scoresFacebook;
	}


	void connectToFacebook (bool success)
	{
		facebookHasConnectedEvent (success);
	}

    public void ShowGooglePlayLeaderboard()
    {
        PlayGamesPlatform.Instance.ShowLeaderboardUI(MAIN_LEADERBOARDCODE);
    }

    public void SaveGooglePlayScore(int score)
    {
        Social.ReportScore(score, MAIN_LEADERBOARDCODE, (bool success) => {
            saveScoreEvent(success);
        });
    }

    public void increaseGooglePlayGameCounter()
    {
        PlayGamesPlatform.Instance.Events.IncrementEvent(EVENT_GAMES_PLAYED, 1);
    }

    public void increaseBonusHitsCounter(int amount)
    {
        if(amount <= 10)
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(EVENT_BONUS_HITS1, 1);
        }
        else if( amount >= 11 && amount <= 30 )
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(EVENT_BONUS_HITS2, 1);
        }
        else
        {
            PlayGamesPlatform.Instance.Events.IncrementEvent(EVENT_BONUS_HITS3, 1);
        }
    }

    public void increaseRankCounter(int amount)
    {
        switch(amount)
        {
            case 1:
                {
                    PlayGamesPlatform.Instance.Events.IncrementEvent(EVENT_RANK1, 1);
                    break;
                }
            case 2:
                {
                    PlayGamesPlatform.Instance.Events.IncrementEvent(EVENT_RANK2, 1);
                    break;
                }
            case 3:
                {
                    PlayGamesPlatform.Instance.Events.IncrementEvent(EVENT_RANK3, 1);
                    break;
                }
            case 4:
                {
                    PlayGamesPlatform.Instance.Events.IncrementEvent(EVENT_RANK4, 1);
                    break;
                }
            case 5:
                {
                    PlayGamesPlatform.Instance.Events.IncrementEvent(EVENT_RANK5, 1);
                    break;
                }


        }
    }
	
    public void connectToGooglePlay()
    {
        Social.localUser.Authenticate((bool success) => {
            if (success == true)
            {
                isPlayerConnectedToGooglePlay = true;
            }
            else
            {
                isPlayerConnectedToGooglePlay = false;
            }
            connectToGooglePlayEvent(success);
        });
    }

	public void connectToFacebook()
	{
		facebookManager.FBlogin ();
	}

	public void showFacebookLeaderboard(){

		Application.LoadLevel("fbleader");
	}

	public void LoadFacebookScores()
	{
		facebookManager.LoadScoreBoard ();
	}

	public string getUserID(){
		return facebookManager.getUserID ();
	}

	public void showADS()
	{
		admob.ShowInterstitial ();
	}

	public void hideADS(){
		admob.Request ();
	}

	public void ShareScore(int score){
		facebookManager.ShareWithFriends (score);
	}

	public void ShareLinkScore(int score){
		facebookManager.ShareLink (score);}

	public void SaveScoreFacebook(int score)
	{
		facebookManager.SetScore (score);
	}

	public void StartRecord()
	{
		everyplay.StartRecord ();
	}

	public void StopRecord()
	{
		everyplay.StopRecord ();
	}

	public void LastRecordGame()
	{
		everyplay.LastRecord ();
	}

	public void ShowEveryplay()
	{
		everyplay.ShowEveryplay ();
	}

	public void ShareModal()
	{
			everyplay.ShowSharingModal ();	
	} 

	public void EveryPlayAddScore(string s, object o)
	{
		everyplay.AddData (s,o);
	}
		
	public int EveryPlayStatus()
	{
		Debug.Log ("SSSSSSSStatus EVERYPLAY");
		Debug.Log (opciones.getValue ());
		//return 1;
		return opciones.getValue ();
	}

	public void Logout(){
		facebookManager.Logout ();
	}

	public void GPLogout()
	{
		PlayGamesPlatform.Instance.SignOut ();
	}

	public delegate void FacebookHasConnectedEvent(bool success);
	public FacebookHasConnectedEvent facebookHasConnectedEvent;

    public delegate void ConnectToGooglePlayEvent(bool success);
    public ConnectToGooglePlayEvent connectToGooglePlayEvent;

    public delegate void SaveGooglePlayScoreEvent(bool success);
    public SaveGooglePlayScoreEvent saveScoreEvent;

	public delegate void ScoresFacebookEvent(bool success, List<object> dataList);
	public ScoresFacebookEvent scoresFacebookEvent;
}
