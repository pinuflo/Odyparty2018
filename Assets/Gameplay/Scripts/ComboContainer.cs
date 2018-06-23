using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboContainer : MonoBehaviour {

    public enum ComboContainerStatus { hidden, visible, shine };
    public ComboContainerStatus status = ComboContainerStatus.hidden;
    public Text amountText;
    private Animator anim;
    private int innercounter = 0;

    public void Awake()
    {
        anim = this.GetComponent<Animator>();
    }



    public void updateCombo(int amount)
    {

        if (amount == 0)
        {
            animateTo(ComboContainerStatus.hidden);
            this.status = ComboContainerStatus.hidden;
        }


        if (amount>1)
        {
            setText(amount);
            switch (status)
            {
                case ComboContainerStatus.hidden:
                    {

                        animateTo(ComboContainerStatus.visible);
                        this.status = ComboContainerStatus.visible;
                        break;
                    }
                case ComboContainerStatus.visible:
                    {
                        animateTo(ComboContainerStatus.shine);
                        this.status = ComboContainerStatus.visible;
                        break;
                    }
            }
        }


        
    }


    private void setText(int amount)
    {
        amountText.text = amount.ToString();
    }

    private void animateTo(ComboContainerStatus status)
    {
        switch (status)
        {
            case ComboContainerStatus.hidden:
                {
                    anim.SetInteger("Status", 0);
                    break;
                }
            case ComboContainerStatus.visible:
                {
                    anim.SetInteger("Status", 1);
                    break;
                }
            case ComboContainerStatus.shine:
                {
                    anim.SetTrigger("Shine");
                    StartCoroutine(comboExplotionienumerator());
                    break;
                }
        }
    }

    IEnumerator comboExplotionienumerator()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("ComboContainerFirework")) as GameObject;
        yield return new WaitForSeconds(2F);
        GameObject.Destroy(obj);
    }

}
