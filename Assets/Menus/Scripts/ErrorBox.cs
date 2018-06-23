using UnityEngine;
using System.Collections;

public class ErrorBox : MonoBehaviour {

    private Animator anim;

    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    public void show()
    {
        anim.SetInteger("Status", 1);
    }

    public void hide()
    {
        anim.SetInteger("Status", 0);
    }



}
