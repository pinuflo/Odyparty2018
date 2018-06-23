using UnityEngine;
using System.Collections;

public class Splat : MonoBehaviour
{

    IEnumerator selfDestroy()
    {
        yield return new WaitForSeconds(3F);
        kill();
    }


    void Start()
    {
            StartCoroutine(selfDestroy());
    }

    public void kill()
    {
        GameObject.Destroy(this.gameObject);
    }

}