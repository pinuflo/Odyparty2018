using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;


public class Opciones : MonoBehaviour {

	public static int value = 0;
	public Toggle toggle; //drag from inspector
	public Button FacebookLogout,GoogleLogout;


	void Awake(){
		this.LoadValue();
	}

	void Start(){
		refreshOption ();
	}

	void LoadValue()
	{
		value = PlayerPrefs.GetInt ("epStatus");
		if (value == 1) {
			toggle.isOn = false;
		} 
		else {
			toggle.isOn = true;
		}
		Debug.Log("LLoading... " + toggle.isOn);
	}

	public void eventValueChanged()
	{
		if (toggle.isOn == true) {
			PlayerPrefs.SetInt ("epStatus", 0);
		} else {
			PlayerPrefs.SetInt ("epStatus", 1);
		}
		PlayerPrefs.Save ();
		Debug.Log("Toggle is "+ toggle.isOn); //check isOn state
	}

	public  int getValue(){
		if (value != 1 && value != 0) {
			Debug.Log ("CARGANDO DATOOOOOOOS");
			this.LoadValue();
		}
		return value;
	}

	public void return_main(){
		Application.LoadLevel ("MainMenu");
	}

	public void Logout()
	{

		IntegrationManager.Instance.Logout ();
		refreshOption ();
	}

	public void GPLogout(){

		IntegrationManager.Instance.GPLogout ();
		refreshOption ();
	}

	public void refreshOption()
	{
		if(IntegrationManager.IsPlayerConenctedToFacebook == false)
		{
			FacebookLogout.interactable =  false;
		}
		else{
			FacebookLogout.interactable =  true;
		}
		
		if(IntegrationManager.isPlayerConnectedToGooglePlay == false)
		{
			
			GoogleLogout.interactable =  false;
		}
		else{
			GoogleLogout.interactable =  true;
		}
	}
}
