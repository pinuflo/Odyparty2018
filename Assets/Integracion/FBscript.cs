using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;
using System;

public class FBscript : MonoBehaviour
{
	private List<object> scoresList = null;

	public GameObject ScoreEntryPanel;
	public GameObject ScoreScrollList;	

	private Dictionary<string, string> profile = null;

	public static string UserID;
	public int UserScore;

	public string getUserID()
	{
		Debug.Log ("PASA LA ID LAGI CULIADO");
		return UserID;
	}

	public int getUserScore()
	{
		Debug.Log ("PUNTAJE ACTUAL get :" + UserScore);
		return UserScore;
	}

    void Awake()
    {
		if (!FB.IsInitialized) {
			// Initialize the Facebook SDK
			Debug.Log ("Calling FB INIT");
			FB.Init(SetInit, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
    }

    void SetInit()
    {
        if(FB.IsLoggedIn)
        {
			FB.API("/me?fields=id", HttpMethod.GET, setUserID);
            Debug.Log("FB is logged in xxx");
        }
        else {
            Debug.Log("FB is not logged in");
        }
    }

    void OnHideUnity(bool isGameShown)
    {

        if (!isGameShown)
        {
            Time.timeScale = 0;
        }
        else {
            Time.timeScale = 1;
        }

    }

    public void FBlogin()
    {

		//var perms = new List<string>(){"public_profile", "email", "user_friends",};
		var perms2 = new List<string> (){"publish_actions"};
		//FB.LogInWithPublishPermissions(perms, AuthCallBack);
		FB.LogInWithPublishPermissions (perms2, AuthCallBack);


    }

	void AuthCallBack(ILoginResult result)
    {

        if (result.Error != null)
        {
            Debug.Log(result.Error);
        }
        else {
			if (FB.IsLoggedIn) {
				// AccessToken class will have session details
				var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
				// Print current access token's User ID
				Debug.Log(aToken.UserId);
				// Print current access token's granted permissions
				foreach (string perm in aToken.Permissions) {
					Debug.Log(perm);
				}
				FB.API("/me/scores", HttpMethod.GET, LoadScoreCallback);
				FB.API("/me?fields=id", HttpMethod.GET, setUserID);
				connectToFacebookEvent (true);
			} else {
				Debug.Log("User cancelled login");
				connectToFacebookEvent (false);
			}
        }
    }

	public bool getFacebookStatus()
	{
		return FB.IsLoggedIn;
	}

	void setUserID(IResult result)
	{
		UserID = result.ResultDictionary["id"].ToString();
	}

	public delegate void ConnectToFacebookEvent(bool success);
	public ConnectToFacebookEvent connectToFacebookEvent;

	public void ShareWithFriends(int score)
	{
		FB.FeedShare (
			string.Empty,
			null,
			"My New Score",
			"I got a great Score >>>"+score+" Try get me",
			"This is the description",
			null,
			string.Empty,
			ShareCallback
			);
	}
	
	void ShareCallback(IResult result)
	{
		if (result.Cancelled) {
			Debug.Log ("Share Cancelled");
		} else if (!string.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Error on share!");
		} else if (!string.IsNullOrEmpty (result.RawResult)) {
			Debug.Log("Sucess on share");}
	}

	public void ShareLink(int score){
		FB.ShareLink (
			new Uri("http://odygames.com/"),
			"My New Score",
			"I got " +score+"\n Can you beat me???",
			new Uri("https://imagizer.imageshack.us/v2/244x244q90/921/uwkfJ7.png"),
			callback: ShareLinkCallback
		);
	}

	void ShareLinkCallback(IResult result){
		if (result.Cancelled) {
			Debug.Log ("Share Cancelled");
		} else if (!string.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Error on share!");
		} else if (!string.IsNullOrEmpty (result.RawResult)) {
			Debug.Log("Sucess on share");}
	}
	
	public void Invite()
	{
		FB.Mobile.AppInvite
			(
				new Uri("https://www.google.cl/?gws_rd=ssl"),
				new Uri("https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcQzbJiRn-Ijj-3I0My-sm81ov_ZLW_MIQWi7b985rET0T9lN5Li"),
				InviteCallback
				);
	}
	
	void InviteCallback(IResult result)
	{
		if (result.Cancelled) {
			Debug.Log ("Invite Cancelled");
		} else if (!string.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Error on invite!");
		} else if (!string.IsNullOrEmpty (result.RawResult)) {
			Debug.Log("Sucess on Invite");}
	}
	
	public void LoadMyScore(){
		FB.API("/me/scores", HttpMethod.GET, LoadScoreCallback);
	}

	public void LoadScoreCallback(IResult result){
		
		var dataList = result.ResultDictionary["data"] as List<object>;
		if(dataList.Count == 0)
		{
			SetScore(0);
			Debug.Log ("PUNTAJE ACTUAL ----- :" + UserScore);
		}
		else{
			var dataDict = dataList[0] as Dictionary<string, object>;	
			string aux = dataDict["score"].ToString();
			UserScore = int.Parse(aux);
			Debug.Log ("PUNTAJE ACTUAL / :" + UserScore);
		}	
	}
	
	public void LoadScoreBoard(){
		FB.API("/app/scores?fields=score,user.limit(30)", HttpMethod.GET, LoadScoresCallback);
	}

	private static List<object> dataList;

	void LoadScoresCallback(IResult result)
	{
		if (result.Cancelled) {
			Debug.Log ("Invite Cancelled");
		} else if (!string.IsNullOrEmpty (result.Error)) {
			Debug.Log ("Error en el Score ql!");
			this.LoadScoreBoard();

		} else if (!string.IsNullOrEmpty (result.RawResult)) {

				Debug.Log("Sucess on Load Scoreboard");
				dataList = result.ResultDictionary ["data"] as List<object>;
				Debug.Log (dataList.Count);
				IntegrationManager.Instance.scoresFacebook (true, dataList);

			}
	}

	public delegate void ScoresFacebookEvent(bool success, List<object> dataList);
	public ScoresFacebookEvent scoresFacebookEvent;
	
	public void SetScore(int scoreToSave)
	{
		string scoreString = ((int)(scoreToSave)).ToString();
		var scoreData = new Dictionary<string, string>() {{"score", scoreString}};
		FB.API("me/scores", HttpMethod.POST, delegate(IGraphResult result){
		Debug.Log ("Score submit result: " + result.ToString() + "scoreData" + scoreData["score"]);	
		}, scoreData);
	}	

	public void Logout(){
		FB.LogOut ();
	}
}
