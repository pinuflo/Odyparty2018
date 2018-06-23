using UnityEngine;
using System.Collections;

public class gameNumber : MonoBehaviour {

    Animator anim;

	void Start () {
        anim = this.GetComponent<Animator>();
	}

    public void SetNumber(int i)
    {
        anim.SetInteger("Status", i);
    }
	

}
