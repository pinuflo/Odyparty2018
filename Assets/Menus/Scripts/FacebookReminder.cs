using UnityEngine;
using System.Collections;

public class FacebookReminder : MonoBehaviour {

    private Animator anim;

	public void closeDialog()
    {
        this.gameObject.SetActive(false);
    }

    public void openDialog()
    {
        this.gameObject.SetActive(true);
    }

    public void connectButton()
    {
        IntegrationManager.Instance.connectToFacebook();
        closeDialog();
    }

    public void connectButtonGoogle()
    {
        IntegrationManager.Instance.connectToGooglePlay();
        closeDialog();
    }


}
