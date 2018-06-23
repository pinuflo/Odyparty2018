using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class EndScreen : MonoBehaviour {
	public Button LastRecordButton;

	// Use this for initialization
	void Start () {

		if (IntegrationManager.Instance.EveryPlayStatus () == 1) {
			LastRecordButton.interactable = false;
		} else {
			LastRecordButton.interactable = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
