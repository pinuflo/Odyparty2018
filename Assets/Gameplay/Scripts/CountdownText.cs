using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CountdownText : MonoBehaviour {

    public Text text;

    public void kill()
    {
        GameObject.Destroy(this.gameObject);
    }

    public void setText(string textInput)
    {
        text.text = textInput;
    }

}
