using UnityEngine;


public class OdyConstants : MonoBehaviour {

    public static OdyConstants OC;
    public const int MaxHP = 14;
    public static int MaxColors
    {
        get
        {
            return System.Enum.GetValues(typeof(GroupColor)).Length;
        }
    }


    public static bool IsTest
    {
        get
        {
            if (Application.isEditor)
                return true;
            else
                return false;
        }

    }

    void Awake()
    {
        if (OC != null)
            GameObject.Destroy(OC);
        else
            OC = this;
    }





}

public enum GroupColor { none, red, purple, blue, black, green, yellow };