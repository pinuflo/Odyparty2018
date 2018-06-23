using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Counter : MonoBehaviour {
	
	public int clickCounter = 0;
	
	[HideInInspector] public List<string> friendNames;
	[HideInInspector] public List<int> friendCounts;
	
	
	public FBLogin fBLogin;
	
	void Start () {
		
	}
	
	void Update () {
		if ( Input.GetKeyDown("a") ){
			clickCounter++;
			fBLogin.SetScore(clickCounter);
		}
	}
	
	void OnGUI(){
		//GUI.Label(new Rect(Screen.width * 0.05f, Screen.height * 0.7f, 500f, 20f), "Number of 'a' key presses: "+clickCounter);
		
		for(int i=0;i<friendNames.Count;i++){
			GUI.Label(new Rect(Screen.width * 0.5f, Screen.height * (0.6f+0.05f*i), 500f, 20f), friendNames[i]+" "+friendCounts[i]);
		}
	}
}
