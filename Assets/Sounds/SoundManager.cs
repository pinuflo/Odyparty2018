using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    /**

    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }


    public static bool IsPlaying
    {
        get
        {
            return gAmp.isPlaying();
        }
    }

    void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        gAmp = this.gameObject.GetComponent<grumbleAMP>();
        DontDestroyOnLoad(transform.gameObject);
    }

    public static void playSong(int song)
    {
        if( IsPlaying  == false)
        {
            gAmp.PlaySong(song, 0, 0);
        }
        else
        {
            gAmp.CrossFadeToNewSong(song, 0, 3);
        }

       
    }

    public static void fadeToSong(int song)
    {
        try
        {
            gAmp.CrossFadeToNewSong(song, 0, 3);
        }
        catch
        {
            Debug.Log("fade to song falló");
        }
        
    }
    **/


}
