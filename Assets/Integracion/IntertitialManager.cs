using UnityEngine;
using System.Collections;
// Librerias de Google Ads
using GoogleMobileAds;
using GoogleMobileAds.Api;

public class IntertitialManager : MonoBehaviour {
	
	//public static IntertitialManager manager;    // Patron Singleton
	
	void Awake ()
	{
		  
	}
	
	void Start()
	{
		// Creacion del banner
		InterstitialAd interstitial = new InterstitialAd("ca-app-pub-2949896690548692/4176571568");
		// Creacion de la peticion de anuncio
		AdRequest request = new AdRequest.Builder().Build();
		// Carga del anuncio
		interstitial.LoadAd(request);

		interstitial.Show();
	}
}
