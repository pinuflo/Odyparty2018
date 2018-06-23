using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OdyBonus : MonoBehaviour {

    Animator anim;
    public Text text;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    IEnumerator reset()
    {
        yield return new WaitForSeconds(3.3F);
        anim.SetInteger("Status", 0);
    }

    public void trigger(int i)
    {
        
        anim.SetInteger("Status", 1);
        text.text = "+" + i.ToString();
        Debug.Log(text.text);
        StartCoroutine(reset());
    }
	
}
