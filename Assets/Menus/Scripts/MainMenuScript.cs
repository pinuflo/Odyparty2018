using UnityEngine;
using System.Collections;
using System;

public class MainMenuScript : MonoBehaviour {

    public MainBox mainBox;
    public ErrorBox errorBox;
    public Animator socialButtonsAnimator;
    public FacebookReminder fReminder;

    void Awake()
    {
        
    }
    

    void Start()
    {
        IntegrationManager.Instance.connectToGooglePlayEvent += connectToGooglePlayEvent;

        if (!SoundManager.IsPlaying)
            SoundManager.playSong(0);
        else
        {
            SoundManager.fadeToSong(0);
            Debug.Log("fade a song");
        }

        mainBox.show();  //mostrar menú principal
        if (IntegrationManager.isFirstTimeLoad == true)
        {
            fReminder.openDialog();
        }
    }


    private void connectToGooglePlayEvent(bool success)
    {
        //if (success == true || OdyConstants.IsTest == true)
        //{
        //    mainBox.show();  //mostrar menú principal
        //}
        //else
        //{
        //    errorBox.show();  //mostrar error
        //}
    }

    public void showLeaderboard()
    {
		mainBox.displayleaderboardAnim ();
    }

	public void showFacebookLeaderBoard()
	{
		IntegrationManager.Instance.showFacebookLeaderboard ();
	}

	public void showGoogleLeaderBoard()
	{
		IntegrationManager.Instance.ShowGooglePlayLeaderboard();
	}

    public void playGame()
    {
        Application.LoadLevel("normal");
    }

    public void exit()
    {
        Application.Quit();
    }

	public void options(){
		Application.LoadLevel ("Opciones");
	}


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void toggleSocialOptions()
    {

        if ( socialButtonsAnimator.GetInteger("Status") == 0)
        {
            socialButtonsAnimator.SetInteger("Status", 1);
        }
        else
        {
            socialButtonsAnimator.SetInteger("Status", 0);
        }
        
    }

    public void connectToGooglePlay()
    {
        IntegrationManager.Instance.connectToGooglePlay();
        socialButtonsAnimator.SetInteger("Status", 0);
    }

	public void connectToFacebook()
	{
		IntegrationManager.Instance.connectToFacebook();
		socialButtonsAnimator.SetInteger("Status", 0);
	}

	public void EveryPlay(){
		IntegrationManager.Instance.ShowEveryplay ();
	}

	public void FBLeaderboard(){

	}


}
