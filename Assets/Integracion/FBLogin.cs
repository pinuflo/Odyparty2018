using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Facebook.Unity;




public class FBLogin : MonoBehaviour {
	
	public Counter counter;
	
	void Awake(){
		
		if(!FB.IsInitialized){
        // Initialize the Facebook SDK
			FB.Init(InitCallback, OnHideUnity);
		}
		else{
			// Already initialized, signal an app activation App Event
			FB.ActivateApp();
		}
		

    }	
    
    void Start(){
    
    }
    
    private void InitCallback(){
		if(FB.IsInitialized){
			// Signal an app activation App Event
			FB.ActivateApp();
			// Continue with Facebook SDK

			FBlogin();

		}
		else{
			Debug.Log("Failed to Initialize the Facebook SDK");
		}
	}
    
    private void OnHideUnity(bool isGameShown){
		if(!isGameShown){
			Time.timeScale = 0;
		}
		else{
			Time.timeScale = 1;
		}
	}
    
    
    public void FBlogin(){
		var perms = new List<string>(){"public_profile", "email", "user_friends","publish_actions"};
		FB.LogInWithPublishPermissions(perms, AuthCallback);
	}
    
    
    
    
    
    private void AuthCallback(ILoginResult result){

		if(FB.IsLoggedIn){
			LoadMyScore();
			LoadScoreBoard();
		}
		else{

		}
	}

    
    
	public void LoadMyScore(){
 		FB.API("/me/scores", HttpMethod.GET, LoadScoreCallback);
	}
	
	public void LoadScoreBoard(){
		FB.API("/app/scores?fields=score,user.limit(3)", HttpMethod.GET, LoadScoresCallback);
	}
	
	public void LoadScoreCallback(IGraphResult result){
		
		var dataList = result.ResultDictionary["data"] as List<object>;
		if(dataList.Count == 0){
			SetScore(0);
		}
		else{
			var dataDict = dataList[0] as Dictionary<string, object>;
		
			long score = (long)dataDict["score"];
			counter.clickCounter = (int)score;
    	}
    	
	}
	
	void LoadScoresCallback(IGraphResult result){

		counter.friendNames = new List<string>();
		counter.friendCounts = new List<int>();
		
		
		var dataList = result.ResultDictionary["data"] as List<object>;
		
		foreach(var dataListEntry in dataList){
			var dataDict = dataListEntry as Dictionary<string, object>;
			
			long score = (long)dataDict["score"];
			
			var user = dataDict["user"] as Dictionary<string, object>;
			
			string userName = user["name"] as string;
			
			counter.friendNames.Add(userName);
			counter.friendCounts.Add((int)score);
			
		}
	}
    
    
    public void SetScore(int scoreToSave){
		string scoreString = ((int)(scoreToSave)).ToString();
 		var scoreData = new Dictionary<string, string>() {{"score", scoreString}};
 		
 		FB.API("me/scores", HttpMethod.POST, delegate(IGraphResult result){
 			
 		}, scoreData);
		
	}
    
    
    
    
}
