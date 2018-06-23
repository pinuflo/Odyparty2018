using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainBox : MonoBehaviour {

	private Animator anim;
	public Animator	 leaderboardAnim;
	public Button facebookLeaderboardButton, googleLeaderboardButton, watchReplayButton;

	void facebookHasConnectedEvent (bool success)
	{
		refreshLeaderBoardMenu();
	}

    void Awake()
    {
        anim = this.GetComponent<Animator>();

    }

	void Start()
	{
		IntegrationManager.Instance.facebookHasConnectedEvent += facebookHasConnectedEvent;
		replayMenu ();
	}

    public void show()
    {
        anim.SetInteger("Status", 1);
    }

    public void hide()
    {
        anim.SetInteger("Status", 0);
    }

	public void refreshLeaderBoardMenu()
	{
		if(IntegrationManager.IsPlayerConenctedToFacebook == false)
		{
			facebookLeaderboardButton.interactable =  false;
		}
		else{
			facebookLeaderboardButton.interactable =  true;
		}

		if(IntegrationManager.isPlayerConnectedToGooglePlay == false)
		{

			googleLeaderboardButton.interactable =  false;
		}
		else{
			googleLeaderboardButton.interactable =  true;
		}

	}

	public void replayMenu()
	{
		Debug.Log ("EveryPlayStatus");
		if (IntegrationManager.Instance.EveryPlayStatus() == 1) {
			watchReplayButton.interactable = false;
		} else {
			watchReplayButton.interactable = true;
		}
	}

	public void displayleaderboardAnim()
	{
		if (leaderboardAnim.GetInteger ("Status") == 0) {

			refreshLeaderBoardMenu();

			leaderboardAnim.SetInteger ("Status", 1);
		}
		else
			leaderboardAnim.SetInteger ("Status", 0);
	}
	
}
