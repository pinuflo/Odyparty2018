using UnityEngine;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using Facebook.Unity;


public class LeaderBoardFacebookMenu : MonoBehaviour {

	private List<object> scoresList = null;
	private Dictionary<string, string> profile = null;
	public GameObject ScoreEntryPanel;
	public GameObject ScoreScrollList;
	public GameObject FriendAvatar;
	

	public void returnToMenu()
	{
		Application.LoadLevel ("MainMenu");
	}

	void scoresFacebookEvent (bool success, List<object> dataList)
	{
		foreach (Transform child in ScoreScrollList.transform) 
		{
			GameObject.Destroy (child.gameObject);
		}

		string ID = IntegrationManager.Instance.getUserID ();
		Debug.Log ("MY AI DI" + ID);
		int aux = 1;
		foreach (var dataListEntry in dataList) {

			var dataDict = dataListEntry as Dictionary<string, object>;
			long score = (long)dataDict ["score"];	
			var user = dataDict ["user"] as Dictionary<string, object>;	
			string userName = user ["name"] as string;
			string userId = user ["id"] as String;


			GameObject ScorePanel;
			ScorePanel = Instantiate (ScoreEntryPanel) as GameObject;
			ScorePanel.transform.SetParent (ScoreScrollList.transform,false);

			if(ID == userId)
			{
				ScorePanel.GetComponent<Image>().color = Color.green;
				Debug.Log ("dat xit works " + ID);
			}
			else{ScorePanel.GetComponent<Image>().color = Color.blue;}

			Transform TheUserAvatar = ScorePanel.transform.Find ("FriendAvatar");
			Image UserAvatar = TheUserAvatar.GetComponent<Image>();
			
			FB.API (Util.GetPictureURL(user["id"].ToString (), 128,128), HttpMethod.GET, delegate(IGraphResult pictureResult){
				
				if(pictureResult.Error != null) // if there was an error
				{
					Debug.Log (pictureResult.Error);
				}
				else // if everything was fine
				{
					UserAvatar.sprite = Sprite.Create (pictureResult.Texture, new Rect(0,0,128,128), new Vector2(0,0)); 
				}
				
			});

			Transform ThisScoreName = ScorePanel.transform.Find ("FriendName");
			Transform ThisScoreScore = ScorePanel.transform.Find ("FriendScore");
			Transform ThisScorePos = ScorePanel.transform.Find ("FriendPos");

			Text ScoreName = ThisScoreName.GetComponent<Text> ();
			Text ScoreScore = ThisScoreScore.GetComponent<Text> ();
			Text scorePos = ThisScorePos.GetComponent<Text> ();

			ScoreName.text = userName;
			ScoreScore.text = score.ToString ();
			scorePos.text = aux.ToString();

			aux = aux +1;

		}
	}

	
	public void Start()
	{
		IntegrationManager.Instance.LoadFacebookScores ();
		Debug.Log ("Load Facebook Scores ");
	}

	public void Awake()
	{
		IntegrationManager.Instance.scoresFacebookEvent -= scoresFacebookEvent;
		IntegrationManager.Instance.scoresFacebookEvent += scoresFacebookEvent;
	}

	public void OnDestroy()
	{
		IntegrationManager.Instance.scoresFacebookEvent -= scoresFacebookEvent;
	}





}
